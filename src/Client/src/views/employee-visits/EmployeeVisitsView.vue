<script setup lang="ts">
import useGetCurrentEmployeeVisits from '@/views/employee-visits/queries/use-get-current-employee-visits.ts';
import useGetAnimals from '@/views/animals/queries/use-get-animals.ts';
import type { ComputedRef, Ref } from 'vue';
import { computed, ref } from 'vue';
import useEndVisitDataValidation from '@/views/employee-visits/composables/use-end-visit-data-validation.ts';
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import useEndVisit from '@/views/employee-visits/queries/use-end-visit.ts';
import type { Visit } from '@/types/visit.ts';
import { VISIT_STATUS_TRANSLATIONS, VISIT_TYPE_TRANSLATIONS } from '@/translations/visit.ts';
import { type Animal } from '@/types/animal.ts';
import { ElNotification } from 'element-plus';
import VisitStatus from '@/enums/visit-status.ts';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import VisitType from '../../enums/visit-type.ts';

const { push } = useRouter();
const { v$ } = useEndVisitDataValidation();
const { data: animals } = useGetAnimals();
const { data: visits, refetch } = useGetCurrentEmployeeVisits();
const { mutateAsync: endVisit } = useEndVisit();

const visitToEnd: Ref<Visit | null> = ref(null);

const isEndVisitDialogOpened: ComputedRef<boolean> = computed({
    get: (): boolean => !!visitToEnd.value,
    set: (value: boolean): void => {
        if (!value) {
            visitToEnd.value = null;
        }
    },
});

const handleEndVisit = async (): Promise<void> => {
    await endVisit({
        id: visitToEnd.value?.id as number,
        request: {
            suggestedTreatment: v$.value.suggestedTreatment.$model,
            prescribedMeds: v$.value.prescribedMeds.$model,
        },
    });

    await refetch();
    visitToEnd.value = null;

    ElNotification({
        title: 'Zakończono wizytę',
    });
};
</script>

<template>
    <el-dialog v-model="isEndVisitDialogOpened">
        <template #header>Zakończ wizytę</template>
        <template #default>
            <div class="control-wrapper">
                <label>Przypisane leki *</label>

                <el-input
                    @blur="v$.prescribedMeds.$touch()"
                    size="large"
                    v-model="v$.prescribedMeds.$model"
                    placeholder="Przypisane leki"
                />

                <control-error-component :v$="v$.prescribedMeds" />
            </div>

            <div class="control-wrapper">
                <label>Zalecenia *</label>

                <el-input
                    @blur="v$.suggestedTreatment.$touch()"
                    size="large"
                    v-model="v$.suggestedTreatment.$model"
                    placeholder="Zalecenia"
                />

                <control-error-component :v$="v$.suggestedTreatment" />
            </div>

            <p class="form-disclaimer">* Pole wymagane</p>

            <div class="buttons-wrapper">
                <el-button @click="isEndVisitDialogOpened = false" link type="warning">Anuluj</el-button>
                <el-button @click="handleEndVisit()" type="primary">Zakończ</el-button>
            </div>
        </template>
        <template #footer></template>
    </el-dialog>

    <div class="content-width">
        <el-table border v-if="visits" :data="visits" style="width: 100%" stripe>
            <el-table-column prop="date" label="Data Wizyty">
                <template #default="scope">
                    {{ new Date(scope.row.date).toLocaleDateString() }}
                </template>
            </el-table-column>
            <el-table-column label="Pacjent">
                <template #default="scope">
                    {{ animals?.find((animal: Animal): boolean => animal.id === scope.row.animalId)?.name }}
                </template>
            </el-table-column>
            <el-table-column prop="visitType" label="Typ Wizyty">
                <template #default="scope">
                    {{ VISIT_TYPE_TRANSLATIONS[scope.row.visitType as VisitType] }}
                </template>
            </el-table-column>
            <el-table-column prop="visitStatus" label="Status">
                <template #default="scope">
                    {{ VISIT_STATUS_TRANSLATIONS[scope.row.visitStatus as VisitStatus] }}
                </template>
            </el-table-column>
            <el-table-column prop="visitInformation" label="Informacje o Wizycie">
                <template #default="scope">
                    {{ scope.row.visitInformation?.trim() || '-' }}
                </template>
            </el-table-column>

            <el-table-column label="Akcje" width="120">
                <template #default="scope">
                    <el-button @click="push({ name: 'visit', params: { id: scope.row.id } })" link type="primary"
                        >Szczegóły</el-button
                    >

                    <el-button
                        v-if="scope.row.visitStatus === VisitStatus.PLANNED"
                        @click="visitToEnd = scope.row"
                        type="primary"
                    >
                        Zakończ
                    </el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.HOME })" type="primary" link>Wróć</el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
