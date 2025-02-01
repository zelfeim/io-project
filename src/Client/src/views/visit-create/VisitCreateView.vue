<script setup lang="ts">
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import useCreateVisitFormDataValidation from '@/views/visit-create/composables/use-create-visit-form-data-validation.ts';
import useGetAnimals from '@/views/animals/queries/use-get-animals.ts';
import useGetEmployees from '@/queries/use-get-employees.ts';
import type { ComputedRef } from 'vue';
import { computed } from 'vue';
import type { Employee } from '@/types/employee.ts';
import useCreateVisit from '@/views/visit-create/queries/use-create-visit.ts';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import { ElNotification } from 'element-plus';
import VisitType from '@/enums/visit-type.ts';
import { VISIT_TYPE_TRANSLATIONS } from '@/translations/visit.ts';

const { push } = useRouter();
const { v$ } = useCreateVisitFormDataValidation();
const { mutateAsync: createVisit } = useCreateVisit();
const { data: animals } = useGetAnimals();
const { data: employees } = useGetEmployees();

const vets: ComputedRef<Employee[]> = computed((): Employee[] => {
    if (!employees.value) {
        return [];
    }

    return employees.value.filter((employee: Employee): boolean => employee.role === 'Vet');
});

const handleCreateVisit = async (): Promise<void> => {
    await createVisit({
        date: v$.value.date.$model!.toISOString(),
        animalId: v$.value.animalId.$model as number,
        visitInformation: v$.value.visitInformation.$model,
        visitLength: v$.value.visitLength.$model,
        type: v$.value.type.$model as VisitType,
        employeeId: v$.value.employeeId.$model as number,
    });

    await push({ path: RoutePath.VISITS });

    ElNotification({
        title: 'Zaplanowano wizytę',
    });
};
</script>

<template>
    <div class="content-width">
        <div class="form">
            <div class="control-wrapper">
                <label>Zwierzę *</label>

                <el-select
                    @blur="v$.animalId.$touch()"
                    v-model="v$.animalId.$model"
                    placeholder="Wybierz pacjenta"
                    value-key="id"
                    size="large"
                >
                    <el-option v-for="animal in animals" :label="animal.name" :key="animal.id" :value="animal.id" />
                </el-select>

                <control-error-component :v$="v$.animalId" />
            </div>

            <div class="control-wrapper">
                <div>Data Wizyty *</div>

                <el-date-picker
                    @blur="v$.date.$touch()"
                    v-model="v$.date.$model"
                    :disabled-date="(date: Date) => date.getTime() < Date.now()"
                    size="large"
                    type="datetime"
                    placeholder="Wybierz datę i godzinę"
                    format="YYYY-MM-DD HH:mm"
                />

                <control-error-component :v$="v$.date" />
            </div>

            <div class="control-wrapper">
                <label>Lekarz *</label>

                <el-select
                    @blur="v$.employeeId.$touch()"
                    v-model="v$.employeeId.$model"
                    placeholder="Wybierz lekarza"
                    value-key="id"
                    size="large"
                >
                    <el-option
                        v-for="employee in vets"
                        :label="`${employee.name} ${employee.surname}`"
                        :key="employee.id"
                        :value="employee.id"
                    />
                </el-select>

                <control-error-component :v$="v$.employeeId" />
            </div>

            <div class="control-wrapper">
                <label>Typ Wizyty *</label>
                <el-select
                    @blur="v$.type.$touch()"
                    size="large"
                    v-model="v$.type.$model"
                    placeholder="Wybierz typ wizyty"
                >
                    <el-option
                        v-for="type in Object.values(VisitType)"
                        :key="type"
                        :label="VISIT_TYPE_TRANSLATIONS[type]"
                        :value="type"
                    ></el-option>
                </el-select>
                <control-error-component :v$="v$.type" />
            </div>

            <div class="control-wrapper">
                <label>Informacje o Wizycie</label>

                <el-input
                    type="textarea"
                    @blur="v$.visitInformation.$touch()"
                    resize="vertical"
                    size="large"
                    v-model="v$.visitInformation.$model"
                    placeholder="Dodaj szczegóły dotyczące wizyty"
                />

                <control-error-component :v$="v$.visitInformation" />
            </div>

            <div class="control-wrapper">
                <div>Czas Trwania (minuty) *</div>

                <el-input-number
                    @blur="v$.visitLength.$touch()"
                    size="large"
                    v-model="v$.visitLength.$model"
                    :min="30"
                    :step="30"
                    placeholder="Podaj czas trwania wizyty w minutach"
                />
                <control-error-component :v$="v$.visitLength" />
            </div>

            <p class="form-disclaimer">* Pole wymagane</p>

            <div class="buttons-wrapper">
                <el-button @click="push({ path: RoutePath.VISITS })" type="text">Wróć</el-button>

                <el-button @click="handleCreateVisit" :disabled="v$.$invalid" type="primary">Dodaj</el-button>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
