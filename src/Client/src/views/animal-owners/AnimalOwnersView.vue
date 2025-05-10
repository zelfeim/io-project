<script setup lang="ts">
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import useGetAnimalOwners from '@/views/animal-owners/queries/use-get-animal-owners.ts';
import type { ComputedRef, Ref } from 'vue';
import { computed, ref } from 'vue';
import type { AnimalOwner } from '@/types/animal-owner.ts';
import useDeleteAnimalOwner from '@/views/animal-owners/queries/use-delete-animal-owner.ts';
import { ElNotification } from 'element-plus';
import useHasAnyRole from '@/composables/use-has-any-role.ts';
import Role from '@/enums/role.ts';

const { hasAnyRole } = useHasAnyRole();
const { push } = useRouter();
const { mutateAsync: deleteAnimalOwner } = useDeleteAnimalOwner();
const { data, refetch } = useGetAnimalOwners();
const ownerToDelete: Ref<AnimalOwner | null> = ref(null);
const isDialogVisible: ComputedRef<boolean> = computed({
    get: (): boolean => !!ownerToDelete.value,
    set: (newValue: boolean): void => {
        if (!newValue) {
            ownerToDelete.value = null;
        }
    },
});

const handleDelete = async (id: string) => {
    await deleteAnimalOwner(+id);
    await refetch();
    ownerToDelete.value = null;

    ElNotification({
        title: `Usunięto właściciela`,
    });
};
</script>

<template>
    <div class="content-width">
        <el-dialog v-model="isDialogVisible">
            <template #header>Uwaga</template>

            <p>Czy na pewno chcesz usunąć właściciela {{ ownerToDelete?.name }} {{ ownerToDelete?.surname }}</p>

            <template #footer>
                <el-button @click="ownerToDelete = null" type="warning">Anuluj</el-button>
                <el-button @click="handleDelete(ownerToDelete!.id.toString())" type="primary">Usuń</el-button>
            </template>
        </el-dialog>

        <el-table stripe :data="data">
            <el-table-column prop="name" label="Imię" width="180" />
            <el-table-column prop="surname" label="Nazwisko" width="180" />
            <el-table-column prop="email.email" label="Adres e-mail" />
            <el-table-column prop="telephone" label="Tel." />
            <el-table-column prop="address" label="Adres" />
            <el-table-column>
                <template #default="scope">
                    <el-button @click="push({ name: 'animalOwner', params: { id: scope.row.id } })" link type="primary">
                        Szczegóły
                    </el-button>
                    <el-button
                        v-if="hasAnyRole([Role.RECEPTIONIST])"
                        @click="ownerToDelete = scope.row"
                        link
                        type="danger"
                    >
                        Usuń
                    </el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.HOME })" type="primary" link>Wróć</el-button>
            <el-button
                v-if="hasAnyRole([Role.RECEPTIONIST])"
                @click="push({ path: RoutePath.ANIMAL_OWNER_CREATE })"
                type="primary"
            >
                Dodaj
            </el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
