<script setup lang="ts">
import useGetVisits from '@/views/visits/queries/use-get-visits.ts';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import { VISIT_STATUS_TRANSLATIONS, VISIT_TYPE_TRANSLATIONS } from '@/translations/visit.ts';
import useCancelVisit from '@/views/visits/queries/use-cancel-visit.ts';
import { ElNotification } from 'element-plus';
import type { ComputedRef, Ref } from 'vue';
import { computed, ref } from 'vue';
import type { Visit } from '@/types/visit.ts';
import useGetAnimals from '@/views/animals/queries/use-get-animals.ts';
import useGetEmployees from '@/queries/use-get-employees.ts';
import { type Animal } from '@/types/animal.ts';
import { type Employee } from '@/types/employee.ts';
import useHasAnyRole from '@/composables/use-has-any-role.ts';
import Role from '@/enums/role.ts';
import VisitStatus from '@/enums/visit-status.ts';
import useRescheduleVisitValidation from '@/views/visits/composables/use-reschedule-visit-validation.ts';
import useRescheduleVisit from '@/views/visits/queries/use-reschedule-visit.ts';
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import VisitType from '../../enums/visit-type.ts';

const { mutateAsync: rescheduleVisit } = useRescheduleVisit();
const { v$ } = useRescheduleVisitValidation();
const { data: animals } = useGetAnimals();
const { data: employees } = useGetEmployees();
const { data, refetch: refetchVisits } = useGetVisits();
const { push } = useRouter();
const { mutateAsync: cancelVisit } = useCancelVisit();
const { hasAnyRole } = useHasAnyRole();

const handleCancelVisit = async (id: number): Promise<void> => {
    await cancelVisit(id);
    visitToCancel.value = null;
    await refetchVisits();

    ElNotification({
        title: 'Anulowano wizytę',
    });
};

const handleRescheduleVisit = async (id: number): Promise<void> => {
    await rescheduleVisit({
        id,
        request: {
            date: v$.value.date.$model,
            visitLength: v$.value.visitLength.$model,
        },
    });

    visitToReschedule.value = null;
    await refetchVisits();

    ElNotification({
        title: 'Przeniesiono wizytę',
    });
};

const visitToCancel: Ref<Visit | null> = ref(null);
const isCancelDialogVisible: ComputedRef<boolean> = computed({
    get: (): boolean => !!visitToCancel.value,
    set: (value: boolean): void => {
        if (!value) {
            visitToCancel.value = null;
        }
    },
});

const visitToReschedule: Ref<Visit | null> = ref(null);
const isRescheduleDialogVisible: ComputedRef<boolean> = computed({
    get: (): boolean => !!visitToReschedule.value,
    set: (value: boolean): void => {
        if (!value) {
            visitToReschedule.value = null;
        }
    },
});

const getEmployeeName = (id: number): string => {
    if (!employees.value) {
        return '-';
    }

    const employee: Employee | undefined = employees.value.find((employee: Employee): boolean => employee.id === id);

    if (!employee) {
        return '-';
    }

    return `${employee.name} ${employee.surname}`;
};
</script>

<template>
    <el-dialog v-model="isCancelDialogVisible">
        <template #header>Uwaga</template>
        <template #default> Czy na pewno chcesz anulować wizytę </template>
        <template #footer>
            <el-button @click="visitToCancel = null" type="warning">Anuluj</el-button>
            <el-button @click="handleCancelVisit(visitToCancel!.id)" type="primary">Zatwierdź</el-button>
        </template>
    </el-dialog>

    <el-dialog v-model="isRescheduleDialogVisible">
        <template #header>Przeniesienie wizyty</template>
        <template #default>
            <div class="control-row">
                <div>Data</div>

                <el-date-picker
                    v-model="v$.date.$model"
                    @blur="v$.date.$touch()"
                    type="datetime"
                    :disabled-date="(date: Date) => date.getTime() < new Date().getTime()"
                ></el-date-picker>

                <control-error-component :v$="v$.date" />
            </div>

            <div class="control-row">
                <div>Czas trwania</div>

                <el-input-number
                    v-model="v$.visitLength.$model"
                    @blur="v$.visitLength.$touch()"
                    :step="30"
                    :min="30"
                ></el-input-number>

                <control-error-component :v$="v$.visitLength" />
            </div>
        </template>
        <template #footer>
            <el-button @click="visitToReschedule = null" type="warning">Anuluj</el-button>
            <el-button @click="handleRescheduleVisit(visitToReschedule!.id)" type="primary">Zatwierdź</el-button>
        </template>
    </el-dialog>

    <div class="content-width">
        <el-table border v-if="data" :data="data" style="width: 100%" stripe>
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
            <el-table-column label="Lekarz">
                <template #default="scope"> {{ getEmployeeName(scope.row.employeeId) }} </template>
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
            <el-table-column width="80">
                <template #default="scope">
                    <el-button
                        v-if="scope.row.visitStatus == VisitStatus.PLANNED && hasAnyRole([Role.RECEPTIONIST])"
                        link
                        type="danger"
                        @click="visitToCancel = scope.row"
                    >
                        Anuluj
                    </el-button>
                </template>
            </el-table-column>
            <el-table-column width="90">
                <template #default="scope">
                    <el-button
                        v-if="scope.row.visitStatus == VisitStatus.PLANNED && hasAnyRole([Role.RECEPTIONIST])"
                        link
                        type="primary"
                        @click="visitToReschedule = scope.row"
                    >
                        Przenieś
                    </el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="buttons-wrapper">
            <el-button link type="primary" @click="push({ path: RoutePath.HOME })"> Wróć </el-button>

            <el-button
                v-if="hasAnyRole([Role.RECEPTIONIST])"
                type="primary"
                @click="push({ path: RoutePath.VISIT_CREATE })"
            >
                Dodaj
            </el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
