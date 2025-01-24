import { createRouter, createWebHistory, type Router } from 'vue-router';
import HomeView from '@/views/HomeView.vue';
import RoutePath from '@/enums/RoutePath.ts';
import registerGuards from '@/router/register-guards.ts';

const router: Router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: RoutePath.HOME,
            name: 'home',
            component: HomeView,
            meta: {
                requireAuth: true,
            },
        },
    ],
});

registerGuards(router);

export default router;
