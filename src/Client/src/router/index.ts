import { createRouter, createWebHistory, type Router } from 'vue-router';
import HomeView from '@/views/home/HomeView.vue';
import RoutePath from '@/enums/route-path.ts';
import LoginView from '@/views/login/LoginView.vue';
import AnimalOwnerCreateView from '@/views/animal-owner-create/AnimalOwnerCreateView.vue';
import AnimalOwnersView from '@/views/animal-owners/AnimalOwnersView.vue';
import AnimalsView from '@/views/animals/AnimalsView.vue';
import ResourcesView from '@/views/resources/ResourcesView.vue';
import VisitsView from '@/views/visits/VisitsView.vue';
import AnimalCreateView from '@/views/animal-create/AnimalCreateView.vue';
import VisitCreateView from '@/views/visit-create/VisitCreateView.vue';
import AnimalOwnerView from '@/views/animal-owner/AnimalOwnerView.vue';
import AnimalView from '@/views/animal/AnimalView.vue';
import EmployeesView from '@/views/employees/EmployeesView.vue';
import EmployeeCreateView from '@/views/employee-create/EmployeeCreateView.vue';
import ResourcesCreateView from '@/views/resources-create/ResourcesCreateView.vue';
import EmployeeVisitsView from '@/views/employee-visits/EmployeeVisitsView.vue';
import VisitView from '@/views/visit/VisitView.vue';

const router: Router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: RoutePath.LOGIN,
            name: 'login',
            component: LoginView,
            meta: {
                public: true,
                breadcrumbs: [],
            },
        },
        {
            path: RoutePath.HOME,
            name: 'home',
            component: HomeView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME],
            },
        },
        {
            path: RoutePath.ANIMAL_OWNER_CREATE,
            name: 'animalOwnerCreate',
            component: AnimalOwnerCreateView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.ANIMAL_OWNERS, RoutePath.ANIMAL_OWNER_CREATE],
            },
        },
        {
            path: RoutePath.ANIMAL_OWNERS,
            name: 'animalOwners',
            component: AnimalOwnersView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.ANIMAL_OWNERS],
            },
        },
        {
            path: `${RoutePath.ANIMAL_OWNER}/:id`,
            name: 'animalOwner',
            component: AnimalOwnerView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.ANIMAL_OWNERS],
            },
        },
        {
            path: `${RoutePath.ANIMAL}/:id`,
            name: 'animal',
            component: AnimalView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.ANIMALS],
            },
        },
        {
            path: RoutePath.ANIMALS,
            name: 'animals',
            component: AnimalsView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.ANIMALS],
            },
        },
        {
            path: RoutePath.ANIMAL_CREATE,
            name: 'animalCreate',
            component: AnimalCreateView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.ANIMALS, RoutePath.ANIMAL_CREATE],
            },
        },
        {
            path: RoutePath.RESOURCES,
            name: 'resources',
            component: ResourcesView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.RESOURCES],
            },
        },
        {
            path: RoutePath.RESOURCE_CREATE,
            name: 'resourceCreate',
            component: ResourcesCreateView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.RESOURCES, RoutePath.RESOURCE_CREATE],
            },
        },
        {
            path: RoutePath.EMPLOYEES,
            name: 'employees',
            component: EmployeesView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.EMPLOYEES],
            },
        },
        {
            path: RoutePath.EMPLOYEE_CREATE,
            name: 'createEmployee',
            component: EmployeeCreateView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.EMPLOYEES, RoutePath.EMPLOYEE_CREATE],
            },
        },
        {
            path: `${RoutePath.VISIT}/:id`,
            name: 'visit',
            component: VisitView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.VISITS],
            },
        },
        {
            path: RoutePath.VISITS,
            name: 'visits',
            component: VisitsView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.VISITS],
            },
        },
        {
            path: RoutePath.EMPLOYEE_VISITS,
            name: 'employeeVisits',
            component: EmployeeVisitsView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.EMPLOYEE_VISITS],
            },
        },
        {
            path: RoutePath.VISIT_CREATE,
            name: 'visitCreate',
            component: VisitCreateView,
            meta: {
                public: false,
                breadcrumbs: [RoutePath.HOME, RoutePath.VISITS, RoutePath.VISIT_CREATE],
            },
        },
        {
            path: '/:catchAll(.*)',
            redirect: RoutePath.HOME,
        },
    ],
});

export default router;
