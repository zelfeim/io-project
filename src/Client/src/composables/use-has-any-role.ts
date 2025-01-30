import mainStore from '@/stores/main-store.ts';
import type Role from '@/enums/role.ts';
import { storeToRefs } from 'pinia';

const useHasAnyRole = () => {
    const { role } = storeToRefs(mainStore());

    const hasAnyRole = (roles: Role[]): boolean => {
        return !!role.value && roles.includes(role.value);
    };

    return {
        hasAnyRole,
    };
};

export default useHasAnyRole;
