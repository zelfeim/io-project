import { createRouter, createWebHistory, type Router } from 'vue-router'
import HomeView from '@/views/home/HomeView.vue'
import RoutePath from '@/enums/route-path.ts'
import registerGuards from '@/router/register-guards.ts'
import LoginView from '@/views/login/LoginView.vue'

const router: Router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: RoutePath.LOGIN,
            name: 'login',
            component: LoginView,
            meta: {
                public: true,
            },
        },
        {
            path: RoutePath.HOME,
            name: 'home',
            component: HomeView,
            meta: {
                public: false,
            },
        },
    ],
})

registerGuards(router)

export default router
