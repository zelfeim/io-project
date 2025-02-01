<script setup lang="ts">
import useGetEmployees from '@/queries/use-get-employees.ts';
import RoutePath from '@/enums/route-path.ts';
import { useRouter } from 'vue-router';
import { ROLE_TRANSLATIONS } from '@/translations/employee.ts';
import type { ComputedRef, Ref } from 'vue';
import { computed, ref } from 'vue';
import type { Employee } from '@/types/employee.ts';
import useDeleteEmployee from '@/views/employees/queries/use-delete-employee.ts';
import { ElNotification } from 'element-plus';
import useHasAnyRole from '@/composables/use-has-any-role.ts';
import Role from '@/enums/role.ts';

const { hasAnyRole } = useHasAnyRole();
const { push } = useRouter();
const { data, refetch } = useGetEmployees();
const { mutateAsync: deleteEmployee } = useDeleteEmployee();
const employeeToDelete: Ref<Employee | null> = ref(null);
const isDeleteEmployeeDialogVisible: ComputedRef<boolean> = computed({
    get: (): boolean => !!employeeToDelete.value,
    set: (value: boolean): void => {
        if (!value) {
            employeeToDelete.value = null;
        }
    },
});

const handleDeleteEmployee = async (): Promise<void> => {
    await deleteEmployee(employeeToDelete.value?.id as number);
    await refetch();

    ElNotification({
        title: `Usunięto pracownika ${employeeToDelete.value?.name} ${employeeToDelete.value?.surname}`,
    });

    employeeToDelete.value = null;
};
</script>

<template>
    <div class="content-width">
        <el-dialog v-model="isDeleteEmployeeDialogVisible">
            <template #header>Uwaga</template>
            <template #default>
                Czy na pewno chcesz usunąć pracownika {{ employeeToDelete?.name }} {{ employeeToDelete?.surname }}?
            </template>
            <template #footer>
                <el-button @click="isDeleteEmployeeDialogVisible = false" type="warning">Anuluj</el-button>
                <el-button @click="handleDeleteEmployee()" type="primary">Usuń</el-button>
            </template>
        </el-dialog>

        <el-table stripe :data="data" border style="width: 100%">
            <el-table-column prop="name" label="Imię" />
            <el-table-column prop="surname" label="Nazwisko" />
            <el-table-column prop="emailAddress.email" label="E-mail" />
            <el-table-column prop="role" label="Rola">
                <template #default="scope">
                    {{ ROLE_TRANSLATIONS[scope.row.role as Role] }}
                </template>
            </el-table-column>
            <el-table-column prop="address" label="Adres" />
            <el-table-column>
                <template #default="scope">
                    <el-button v-if="hasAnyRole([Role.ADMIN])" link type="danger" @click="employeeToDelete = scope.row">
                        Usuń
                    </el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.HOME })" type="text">Wróć</el-button>

            <el-button
                v-if="hasAnyRole([Role.ADMIN])"
                @click="push({ path: RoutePath.EMPLOYEE_CREATE })"
                type="primary"
            >
                Dodaj pracownika
            </el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
