<script setup lang="ts">
import useGetResources from '@/views/resources/queries/use-get-resources.ts';
import RoutePath from '@/enums/route-path.ts';
import { useRouter } from 'vue-router';
import useDeleteResource from '@/views/resources/queries/use-delete-resource.ts';
import type { ComputedRef, Ref } from 'vue';
import { computed, ref } from 'vue';
import type { Resource } from '@/types/resource.ts';
import useUpdateResourceAmount from '@/views/resources/queries/use-update-resource-amount.ts';
import { ElNotification } from 'element-plus';

const { push } = useRouter();
const { mutateAsync: updateResourceAmount } = useUpdateResourceAmount();
const { mutateAsync: deleteResource } = useDeleteResource();
const { data: resources, refetch } = useGetResources();
const amount: Ref<number> = ref(1);

const resourceToUpdate: Ref<Resource | null> = ref(null);
const isUpdateResourceDialogOpened: ComputedRef<boolean> = computed({
    get: (): boolean => !!resourceToUpdate.value,
    set: (value: boolean): void => {
        if (!value) {
            resourceToUpdate.value = null;
        }
    },
});

const resourceToDelete: Ref<Resource | null> = ref(null);
const isDeleteResourceDialogOpened: ComputedRef<boolean> = computed({
    get: (): boolean => !!resourceToDelete.value,
    set: (value: boolean): void => {
        if (!value) {
            resourceToDelete.value = null;
        }
    },
});

const handleUpdateResourceAmount = async (): Promise<void> => {
    await updateResourceAmount({ id: resourceToUpdate.value?.id as number, amount: amount.value });
    await refetch();

    ElNotification({
        title: `Zaktualizowano stan zasobu ${resourceToUpdate.value?.name}`,
    });

    resourceToUpdate.value = null;
};

const handleDeleteResource = async (): Promise<void> => {
    await deleteResource(resourceToDelete.value?.id as number);
    await refetch();

    ElNotification({
        title: `Usunięto zasób ${resourceToDelete.value?.name}`,
    });

    resourceToDelete.value = null;
};
</script>

<template>
    <el-dialog v-model="isUpdateResourceDialogOpened">
        <template #header>Aktualizacja stanu</template>

        <div>Nowa ilość</div>
        <el-input-number v-model="amount" :min="1" />

        <template #footer>
            <el-button @click="isUpdateResourceDialogOpened = false" type="warning">Anuluj</el-button>
            <el-button @click="handleUpdateResourceAmount()" type="primary">Potwierdź</el-button>
        </template>
    </el-dialog>

    <el-dialog v-model="isDeleteResourceDialogOpened">
        <template #header>Uwaga</template>

        <p>Czy na pewno chcesz usunąć zasób {{ resourceToDelete?.name }}?</p>

        <template #footer>
            <el-button @click="isDeleteResourceDialogOpened = false" type="warning">Anuluj</el-button>
            <el-button @click="handleDeleteResource()" type="primary">Potwierdź</el-button>
        </template>
    </el-dialog>

    <div class="content-width">
        <el-table :data="resources" border style="width: 100%">
            <el-table-column prop="name" label="Nazwa" />
            <el-table-column prop="type" label="Typ" />
            <el-table-column prop="amount" label="Ilość" />
            <el-table-column prop="shelfLive" label="Data ważności">
                <template #default="scope">
                    {{ new Date(scope.row.shelfLive).toLocaleDateString() }}
                </template>
            </el-table-column>
            <el-table-column>
                <template #default="scope">
                    <el-button @click="resourceToDelete = scope.row" type="danger" link>Usuń</el-button>

                    <el-button @click="resourceToUpdate = scope.row" type="primary" link>Aktualizuj ilość</el-button>
                </template>
            </el-table-column>
        </el-table>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.HOME })" type="text">Wróć</el-button>

            <el-button @click="push({ path: RoutePath.RESOURCE_CREATE })" type="primary">Dodaj zasób</el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
