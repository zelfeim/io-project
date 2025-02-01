<script setup lang="ts">
import { type RouteLocationNormalizedLoaded, useRoute } from 'vue-router';
import type { ComputedRef } from 'vue';
import { computed } from 'vue';
import RoutePath from '@/enums/route-path.ts';

const route: RouteLocationNormalizedLoaded = useRoute();

const breadcrumbs: ComputedRef<RoutePath[]> = computed((): RoutePath[] => {
    return route.meta.breadcrumbs;
});

const ROUTE_NAME_BY_PATH: Record<RoutePath, string> = {
    [RoutePath.HOME]: 'Strona główna',
    [RoutePath.LOGIN]: '',
    [RoutePath.ANIMAL_OWNER_CREATE]: 'Dodaj właściciela',
    [RoutePath.ANIMAL_OWNERS]: 'Właściciele',
    [RoutePath.ANIMAL_OWNER]: 'Podgląd właściciela',
    [RoutePath.ANIMAL_CREATE]: 'Dodaj zwierzę',
    [RoutePath.ANIMAL]: '',
    [RoutePath.ANIMALS]: 'Zwierzęta',
    [RoutePath.VISITS]: 'Wizyty',
    [RoutePath.RESOURCES]: 'Zasoby',
    [RoutePath.RESOURCE_CREATE]: 'Stwórz zasób',
    [RoutePath.EMPLOYEES]: 'Pracownicy',
    [RoutePath.EMPLOYEE_VISITS]: 'Wizyty',
    [RoutePath.EMPLOYEE_CREATE]: 'Dodaj pracownika',
    [RoutePath.VISIT_CREATE]: 'Dodaj wizytę',
    [RoutePath.VISIT]: '',
};
</script>

<template>
    <div class="breadcrumbs-wrapper">
        <div class="content-width breadcrumbs">
            <div v-for="breadcrumb in breadcrumbs" :key="breadcrumb" class="breadcrumb-wrapper">
                <router-link :to="breadcrumb">
                    {{ ROUTE_NAME_BY_PATH[breadcrumb] }}
                </router-link>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.breadcrumbs-wrapper {
    padding: 8px 0;
    box-shadow: #beb3b3 2px 2px 5px;
    color: var(--text);

    .breadcrumbs {
        display: flex;
        align-items: center;
        gap: 4px;

        .breadcrumb-wrapper {
            a {
                color: inherit;
                text-decoration: none;

                &:hover {
                    text-decoration: underline;
                }
            }

            &:not(.breadcrumb-wrapper:first-child) {
                position: relative;
                padding-left: 30px;

                &::before {
                    content: '>';
                    position: absolute;
                    left: 0;
                    top: 0;
                    width: 30px;
                    text-align: center;
                }
            }
        }
    }
}
</style>
