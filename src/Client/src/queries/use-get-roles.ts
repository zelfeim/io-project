import { useQuery } from '@tanstack/vue-query';
import type Role from '@/enums/role.ts';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';
import mainStore from '@/stores/main-store.ts';

const useGetRoles = () => {
    const { sessionStart } = mainStore();

    return useQuery({
        queryKey: ['useGetRoles', sessionStart],
        queryFn: async (): Promise<Role> => {
            const response: AxiosResponse<Role> = await httpClient.get('/roles');

            return response.data;
        },
    });
};

export default useGetRoles;
