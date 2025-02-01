<script setup lang="ts">
import useGetAnimals from '@/views/animals/queries/use-get-animals.ts';
import type { Ref } from 'vue';
import { computed, type ComputedRef, ref } from 'vue';
import type { Animal } from '@/types/animal.ts';
import useDeleteAnimal from '@/views/animals/queries/use-delete-animal.ts';
import { ElNotification } from 'element-plus';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import useGetAnimalOwners from '@/views/animal-owners/queries/use-get-animal-owners.ts';
import { type AnimalOwner } from '@/types/animal-owner.ts';
import useHasAnyRole from '@/composables/use-has-any-role.ts';
import Role from '@/enums/role.ts';

const { push } = useRouter();
const { data, refetch } = useGetAnimals();
const { data: animalOwners } = useGetAnimalOwners();
const { mutateAsync: deleteAnimal } = useDeleteAnimal();
const { hasAnyRole } = useHasAnyRole();

const animalToRemove: Ref<Animal | null> = ref(null);
const isDialogVisible: ComputedRef<boolean> = computed({
    get: (): boolean => !!animalToRemove.value,
    set: (newValue: boolean): void => {
        if (!newValue) {
            animalToRemove.value = null;
        }
    },
});

const handleDelete = async (id: string): Promise<void> => {
    await deleteAnimal(+id);
    await refetch();
    animalToRemove.value = null;

    ElNotification({
        title: `Usunięto zwierzę`,
    });
};

const getAnimalOwnerName = (id: number): string => {
    const owner: AnimalOwner | undefined = animalOwners.value.find((owner: AnimalOwner): boolean => owner.id === id);

    if (!owner) {
        return '';
    }

    return `${owner.name} ${owner.surname}`;
};
</script>

<template>
    <div class="content-width">
        <el-dialog v-model="isDialogVisible">
            <template #header>Uwaga</template>

            <p>Czy na pewno chcesz usunąć zwierzę {{ animalToRemove?.name }}?</p>

            <template #footer>
                <el-button @click="animalToRemove = null" type="warning">Anuluj</el-button>
                <el-button @click="handleDelete(animalToRemove!.id.toString())" type="primary">Usuń</el-button>
            </template>
        </el-dialog>

        <el-table border stripe :data="data" style="width: 100%">
            <el-table-column prop="name" label="Imię" />
            <el-table-column prop="age" label="Wiek" />
            <el-table-column prop="race" label="Rasa" />
            <el-table-column prop="species" label="Gatunek" />
            <el-table-column prop="animalOwnerId" label="Właściciel">
                <template #default="scope">
                    {{ getAnimalOwnerName(scope.row.animalOwnerId) }}
                </template>
            </el-table-column>
            <el-table-column>
                <template #default="scope">
                    <el-button link type="primary" @click="push({ name: 'animal', params: { id: scope.row.id } })">
                        Szczegóły
                    </el-button>
                    <el-button
                        v-if="hasAnyRole([Role.RECEPTIONIST])"
                        link
                        type="danger"
                        @click="animalToRemove = scope.row"
                    >
                        Usuń
                    </el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="buttons-wrapper">
            <el-button link type="primary" @click="push({ path: RoutePath.HOME })">Wróć</el-button>
            <el-button
                v-if="hasAnyRole([Role.RECEPTIONIST])"
                type="primary"
                @click="push({ path: RoutePath.ANIMAL_CREATE })"
                >Dodaj</el-button
            >
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
