import { type RouteLocationNormalizedLoaded, useRoute } from 'vue-router';
import { computed, type ComputedRef } from 'vue';
import RoutePath from '@/enums/route-path.ts';

const useCurrentRoute = () => {
    const route: RouteLocationNormalizedLoaded = useRoute();

    const isLogin: ComputedRef<boolean> = computed((): boolean => {
        return route.path === RoutePath.LOGIN;
    });

    return {
        isLogin,
    };
};

export default useCurrentRoute;
