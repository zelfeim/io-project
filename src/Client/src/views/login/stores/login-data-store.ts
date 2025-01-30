import { defineStore } from 'pinia';
import type { Ref } from 'vue';
import { ref } from 'vue';
import type { LoginData } from '@/views/login/types/login-data.ts';

const loginDataStore = defineStore('loginDataStore', () => {
    const data: Ref<LoginData> = ref({
        login: '',
        password: '',
    });

    return {
        data,
    };
});

export default loginDataStore;
