<script setup lang="ts">
import useLoginDataValidation from '@/views/login/composables/use-login-data-validation.ts';
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import { storeToRefs } from 'pinia';
import mainStore from '@/stores/main-store.ts';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import httpClient from '@/http-client';
import { ElNotification } from 'element-plus';

const { v$ } = useLoginDataValidation();
const { push } = useRouter();
const { sessionStart } = storeToRefs(mainStore());

const handleLogin = async (): Promise<void> => {
    try {
        await httpClient.post('/login', {
            email: v$.value.login.$model,
            password: v$.value.password.$model,
        });

        sessionStart.value = new Date();
        await push(RoutePath.HOME);
        v$.value.login.$model = '';
        v$.value.password.$model = '';
        v$.value.$reset();
    } catch (e) {
        ElNotification({
            title: 'Adres email lub hasło jest nieprawidłowe',
        });
    }
};
</script>

<template>
    <div class="content-width login-wrapper">
        <div class="login-form-wrapper">
            <div class="login-logo-wrapper">
                <img class="login-logo" src="@/assets/logo.png" alt="Logo" />
            </div>

            <el-form @keydown.enter.prevent="handleLogin" class="login-form">
                <div class="control-wrapper">
                    <label>Adres e-mail</label>

                    <el-input @blur="v$.login.$touch()" size="large" v-model="v$.login.$model" />

                    <control-error-component :v$="v$.login" />
                </div>

                <div class="control-wrapper">
                    <label for="">Hasło</label>

                    <el-input @blur="v$.password.$touch()" size="large" v-model="v$.password.$model" type="password" />

                    <control-error-component :v$="v$.password" />
                </div>

                <el-button @click="handleLogin" size="large" type="primary">Login</el-button>
            </el-form>
        </div>
    </div>
</template>

<style scoped lang="scss">
.login-logo-wrapper {
    text-align: center;

    .login-logo {
        width: 300px;
    }
}
</style>
