import 'vue-router';
import type RoutePath from '@/enums/route-path.ts';

export {};

declare module 'vue-router' {
    interface RouteMeta {
        public: boolean;
        breadcrumbs: RoutePath[];
    }
}
