<script setup lang="ts">
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import useCurrentRoute from '@/composables/use-current-route.ts';
import { storeToRefs } from 'pinia';
import mainStore from '@/stores/main-store.ts';
import httpClient from '@/http-client';

const { push } = useRouter();
const { sessionStart } = storeToRefs(mainStore());
const { isLogin } = useCurrentRoute();

const handleLogout = async (): Promise<void> => {
    await httpClient.post('/logout');
    await push(RoutePath.LOGIN);
    sessionStart.value = null;
};
</script>

<template>
    <header>
        <div class="header-content content-width">
            <h1>VetMedica</h1>

            <div v-if="!isLogin" class="logout">
                <el-button type="warning" @click="handleLogout">Wyloguj</el-button>
            </div>
        </div>
    </header>
</template>

<style scoped lang="scss">
header {
    background: var(--secondary);
    color: var(--light-text);
    padding: 10px 0;

    .header-content {
        display: flex;
        align-items: center;

        h1 {
            flex: 1;
        }
    }
}
</style>
