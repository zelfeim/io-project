<script setup lang="ts">
import useGetAnimalVisits from '@/views/animal/queries/use-get-animal-visits.ts';
import { useRoute, useRouter } from 'vue-router';
import useGetAnimal from '@/views/animal/queries/use-get-animal.ts';
import useGetAnimalOwners from '@/views/animal-owners/queries/use-get-animal-owners.ts';
import type { AnimalOwner } from '@/types/animal-owner.ts';
import RoutePath from '@/enums/route-path.ts';
import { VISIT_STATUS_TRANSLATIONS, VISIT_TYPE_TRANSLATIONS } from '@/translations/visit.ts';
import VisitType from '../../enums/visit-type.ts';
import VisitStatus from '../../enums/visit-status.ts';

const route = useRoute();
const { push } = useRouter();
const { data: owners } = useGetAnimalOwners();
const { data: visits } = useGetAnimalVisits(+route.params.id);
const { data: animal } = useGetAnimal(+route.params.id);

const getAnimalOwnerName = (id: number): string => {
    const owner: AnimalOwner | undefined = owners.value.find((owner: AnimalOwner): boolean => owner.id === id);

    return `${owner?.name} ${owner?.surname}`;
};
</script>

<template>
    <div class="content-width">
        <div v-if="animal" class="owner-details">
            <h3>Informacje o zwierzęciu</h3>
            <el-descriptions size="large" direction="vertical" border>
                <el-descriptions-item label="Imię">{{ animal.name }}</el-descriptions-item>
                <el-descriptions-item label="Rasa">{{ animal.race }}</el-descriptions-item>
                <el-descriptions-item label="Gatunek">{{ animal.species }}</el-descriptions-item>
                <el-descriptions-item label="Wiek">{{ animal.age }}</el-descriptions-item>
                <el-descriptions-item label="Właściciel">{{
                    getAnimalOwnerName(animal.animalOwnerId)
                }}</el-descriptions-item>
            </el-descriptions>
        </div>

        <div v-if="visits">
            <h3>Wizyty</h3>

            <el-table :data="visits" border style="width: 100%">
                <el-table-column prop="date" label="Data wizyty">
                    <template #default="scope">
                        {{ new Date(scope.row.date).toLocaleDateString() }}
                    </template>
                </el-table-column>
                <el-table-column prop="visitLength" label="Długość wizyty (min)" />
                <el-table-column prop="visitType" label="Typ wizyty">
                    <template #default="scope">
                        {{ VISIT_TYPE_TRANSLATIONS[scope.row.visitType as VisitType] }}
                    </template>
                </el-table-column>
                <el-table-column prop="visitStatus" label="Status wizyty">
                    <template #default="scope">
                        {{ VISIT_STATUS_TRANSLATIONS[scope.row.visitStatus as VisitStatus] }}
                    </template>
                </el-table-column>
                <el-table-column prop="visitInformation" label="Informacje" />
                <el-table-column prop="suggestedTreatment" label="Zalecenia" />
                <el-table-column prop="prescription.prescribedMeds" label="Recepta" />
            </el-table>
        </div>

        <div class="buttons-wrapper">
            <el-button type="primary" @click="push({ path: RoutePath.ANIMALS })" link> Wróć </el-button>

            <el-button type="primary" @click="push({ name: 'animalOwner', params: { id: animal!.animalOwnerId } })">
                Zobacz właściciela
            </el-button>
        </div>
    </div>
</template>

<style scoped lang="scss">
h3 {
    margin: 20px 0 10px;
}
</style>
