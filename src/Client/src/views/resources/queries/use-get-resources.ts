import { useQuery } from '@tanstack/vue-query';
import type { Resource } from '@/types/resource.ts';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';

const useGetResources = () => {
    return useQuery({
        queryKey: ['useGetResources'],
        queryFn: async (): Promise<Resource[]> => {
            const response: AxiosResponse = await httpClient.get('/api/resource');

            return response.data;
        },
    });
};

export default useGetResources;
