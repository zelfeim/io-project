<script setup lang="ts">
import useGetVisit from '@/queries/use-get-visit.ts';
import { useRoute, useRouter } from 'vue-router';
import { VISIT_STATUS_TRANSLATIONS, VISIT_TYPE_TRANSLATIONS } from '../../translations/visit.ts';
import useGetAnimals from '@/views/animals/queries/use-get-animals.ts';
import RoutePath from '@/enums/route-path.ts';

const route = useRoute();
const { push } = useRouter();
const { data: animals } = useGetAnimals();
const { data: visit } = useGetVisit(+route.params.id);
</script>

<template>
    <div class="content-width">
        <el-descriptions v-if="visit" title="Szczegóły wizyty" border>
            <el-descriptions-item label="ID Wizyty">{{ visit.id }}</el-descriptions-item>
            <el-descriptions-item label="ID Zwierzęcia">{{
                animals?.find((animal) => animal.id === visit.animalId).name
            }}</el-descriptions-item>

            <el-descriptions-item label="Data wizyty">
                {{ new Date(visit.date).toLocaleDateString() }}
            </el-descriptions-item>
            <el-descriptions-item label="Długość wizyty">{{ visit.visitLength }} min</el-descriptions-item>
            <el-descriptions-item label="Typ wizyty">
                {{ VISIT_TYPE_TRANSLATIONS[visit.visitType] }}
            </el-descriptions-item>
            <el-descriptions-item label="Status wizyty">
                {{ VISIT_STATUS_TRANSLATIONS[visit.visitStatus] }}
            </el-descriptions-item>
            <el-descriptions-item label="Informacje">
                {{ visit.visitInformation.trim() || 'Brak informacji' }}
            </el-descriptions-item>
            <el-descriptions-item label="Zalecenia">
                {{ visit.suggestedTreatment || 'Brak zaleceń' }}
            </el-descriptions-item>
            <el-descriptions-item label="Przypisane leki">
                {{ visit.prescription?.prescribedMeds || 'Brak recepty' }}
            </el-descriptions-item>
        </el-descriptions>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.EMPLOYEE_VISITS })" type="primary" link>Wróć</el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
