import { defineStore } from 'pinia';
import type { Ref } from 'vue';
import { ref } from 'vue';
import type Role from '@/enums/role.ts';

const mainStore = defineStore(
    'mainStore',
    () => {
        const sessionStart: Ref<Date | null> = ref(null);
        const role: Ref<Role | null> = ref(null);

        return {
            sessionStart,
            role,
        };
    },
    {
        persist: true,
    },
);

export default mainStore;
