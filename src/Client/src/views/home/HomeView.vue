<script setup lang="ts">
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import useHasAnyRole from '@/composables/use-has-any-role.ts';
import Role from '@/enums/role.ts';
import useGetRoles from '@/queries/use-get-roles.ts';
import { watch } from 'vue';
import { storeToRefs } from 'pinia';
import mainStore from '@/stores/main-store.ts';

const { push } = useRouter();
const { role } = storeToRefs(mainStore());
const { hasAnyRole } = useHasAnyRole();
const { data, isLoading } = useGetRoles();

watch(isLoading, (value: boolean): void => {
    if (!value) {
        role.value = data.value;
    }
});
</script>

<template>
    <div v-if="role" class="content-width">
        <div class="feature-list">
            <div
                v-if="hasAnyRole([Role.VET, Role.RECEPTIONIST])"
                @click="push({ path: RoutePath.ANIMAL_OWNERS })"
                class="feature"
            >
                <h4>Właściciele</h4>
                <p>Przeglądaj, dodaj oraz usuwaj właścicieli</p>
            </div>

            <div
                v-if="hasAnyRole([Role.VET, Role.RECEPTIONIST])"
                @click="push({ path: RoutePath.ANIMALS })"
                class="feature"
            >
                <h4>Zwierzęta (pacjenci)</h4>
                <p>Przeglądaj, dodaj oraz usuwaj pacjentów</p>
            </div>

            <div v-if="hasAnyRole([Role.RECEPTIONIST])" @click="push({ path: RoutePath.VISITS })" class="feature">
                <h4>Wizyty</h4>
                <p>Przeglądaj, dodawaj oraz anuluj wizyty</p>
            </div>

            <div v-if="hasAnyRole([Role.VET])" @click="push({ path: RoutePath.EMPLOYEE_VISITS })" class="feature">
                <h4>Wizyty</h4>
                <p>Przeglądaj wizyty</p>
            </div>

            <div v-if="hasAnyRole([Role.VET])" @click="push({ path: RoutePath.RESOURCES })" class="feature">
                <h4>Zasoby</h4>
                <p>Zarządzaj zasobami</p>
            </div>

            <div v-if="hasAnyRole([Role.ADMIN])" @click="push({ path: RoutePath.EMPLOYEES })" class="feature">
                <h4>Pracownicy</h4>
                <p>Zarządzaj pracownikami</p>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.feature-list {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 16px;

    .feature {
        cursor: pointer;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 4px;
        background: var(--surface);
        border-radius: 12px;
        padding: 20px 0;

        .material-symbols-outlined {
            font-size: 76px;
        }
    }
}
</style>
