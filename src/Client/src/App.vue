<script lang="ts" setup>
import { RouterView } from 'vue-router';
import FooterComponent from '@/components/footer/FooterComponent.vue';
import HeaderComponent from '@/components/header/HeaderComponent.vue';
import BreadcrumbsComponent from '@/components/breadcrumbs/BreadcrumbsComponent.vue';
import useCurrentRoute from '@/composables/use-current-route.ts';
import { storeToRefs } from 'pinia';
import mainStore from '@/stores/main-store.ts';

const { role } = storeToRefs(mainStore());
const { isLogin } = useCurrentRoute();
</script>

<template>
    <div class="wrapper">
        <header-component />
        <breadcrumbs-component v-if="!isLogin" />

        <div class="view-wrapper">
            <router-view />
        </div>

        <footer-component />
    </div>
</template>

<style lang="scss" scoped>
.wrapper {
    display: flex;
    flex-direction: column;
    min-height: 100vh;

    .view-wrapper {
        padding: 40px 0;
        flex: 1;
        display: flex;
        align-items: center;
    }
}
</style>
