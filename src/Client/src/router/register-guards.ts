import { type RouteLocationNormalized, type Router } from 'vue-router'
import RoutePath from '@/enums/route-path.ts'

const registerGuards = (router: Router): void => {
    router.beforeEach((to: RouteLocationNormalized): RoutePath | boolean => {
        // TODO
        const isAuthenticated: boolean = true

        if (!isAuthenticated) {
            return to.meta.public || RoutePath.LOGIN
        }

        return !to.meta.public || RoutePath.HOME
    })
}

export default registerGuards
