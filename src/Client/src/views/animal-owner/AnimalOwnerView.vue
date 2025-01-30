<script setup lang="ts">
import { RouteLocationNormalizedLoaded, useRoute, useRouter } from 'vue-router';
import useGetAnimalOwner from '@/views/animal-owner/queries/use-get-animal-owner.ts';
import useGetOwnerAnimals from '@/views/animal-owner/queries/use-get-owner-animals.ts';
import useDeleteAnimal from '@/views/animals/queries/use-delete-animal.ts';
import { computed, ComputedRef, ref, Ref } from 'vue';
import { Animal } from '@/types/animal.ts';
import { ElNotification } from 'element-plus';
import useDeleteAnimalOwner from '@/views/animal-owners/queries/use-delete-animal-owner.ts';
import RoutePath from '@/enums/route-path.ts';
import useHasAnyRole from '@/composables/use-has-any-role.ts';
import Role from '@/enums/role.ts';

const route: RouteLocationNormalizedLoaded = useRoute();
const { hasAnyRole } = useHasAnyRole();
const { push } = useRouter();
const { data: owner } = useGetAnimalOwner(+route.params.id);
const { data: animals, refetch } = useGetOwnerAnimals(+route.params.id);
const { mutateAsync: deleteAnimalOwner } = useDeleteAnimalOwner();
const { mutateAsync: deleteAnimal } = useDeleteAnimal();

const animalToRemove: Ref<Animal | null> = ref(null);
const isRemoveAnimalDialogVisible: ComputedRef<boolean> = computed({
    get: (): boolean => !!animalToRemove.value,
    set: (newValue: boolean): void => {
        if (!newValue) {
            animalToRemove.value = null;
        }
    },
});

const isRemoveOwnerDialogVisible: Ref<boolean> = ref(false);

const handleDeleteAnimalOwner = async (): Promise<void> => {
    await deleteAnimalOwner(owner.value.id);
    await push({ path: RoutePath.ANIMAL_OWNERS });

    ElNotification({
        title: 'Usunięto właściciela',
    });
};

const handleDeleteAnimal = async (id: string): Promise<void> => {
    await deleteAnimal(+id);
    await refetch();
    animalToRemove.value = null;

    ElNotification({
        title: `Usunięto zwierzę`,
    });
};
</script>

<template>
    <el-dialog v-model="isRemoveOwnerDialogVisible">
        <template #header>Uwaga</template>

        <p>Czy na pewno chcesz usunąć właściciela?</p>

        <template #footer>
            <el-button @click="isRemoveOwnerDialogVisible = false" type="warning">Anuluj</el-button>
            <el-button @click="handleDeleteAnimalOwner()" type="primary">Usuń</el-button>
        </template>
    </el-dialog>

    <el-dialog v-model="isRemoveAnimalDialogVisible">
        <template #header>Uwaga</template>

        <p>Czy na pewno chcesz usunąć zwierzę {{ animalToRemove?.name }}?</p>

        <template #footer>
            <el-button @click="animalToRemove = null" type="warning">Anuluj</el-button>
            <el-button @click="handleDeleteAnimal(animalToRemove.id)" type="primary">Usuń</el-button>
        </template>
    </el-dialog>

    <div class="content-width">
        <div v-if="owner" class="owner-details">
            <h3>Informacje o właścicielu</h3>
            <el-descriptions size="large" direction="vertical" border>
                <el-descriptions-item label="Imię">{{ owner.name }}</el-descriptions-item>
                <el-descriptions-item label="Nazwisko">{{ owner.surname }}</el-descriptions-item>
                <el-descriptions-item label="E-mail">{{ owner.email.email }}</el-descriptions-item>
                <el-descriptions-item label="Telefon">{{ owner.telephone }}</el-descriptions-item>
                <el-descriptions-item label="Adres">{{ owner.address }}</el-descriptions-item>
            </el-descriptions>
        </div>

        <div class="owner-animals">
            <h3>Zwierzęta</h3>
            <el-table border stripe :data="animals" style="width: 100%">
                <el-table-column prop="name" label="Imię" />
                <el-table-column prop="age" label="Wiek" />
                <el-table-column prop="race" label="Rasa" />
                <el-table-column prop="species" label="Gatunek" />
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
        </div>

        <div class="buttons-wrapper">
            <el-button link type="primary" @click="push({ path: RoutePath.ANIMAL_OWNERS })"> Wróć </el-button>
            <el-button v-if="hasAnyRole([Role.RECEPTIONIST])" type="danger" @click="isRemoveOwnerDialogVisible = true">
                Usuń właściciela
            </el-button>
        </div>
    </div>
</template>

<style scoped lang="scss">
h3 {
    margin: 20px 0 10px;
}

.el-table:deep(tr) {
    cursor: pointer;
}
</style>
