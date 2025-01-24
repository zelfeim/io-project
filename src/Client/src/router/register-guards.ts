import {type RouteLocationNormalized, type Router} from "vue-router";

const registerGuards = (router: Router): void => {
    router.beforeEach((to: RouteLocationNormalized): boolean => {
        if (to.meta.requireAuth) {
            // todo auth
            return true;
        }

        return true;
    });
}

export default registerGuards;