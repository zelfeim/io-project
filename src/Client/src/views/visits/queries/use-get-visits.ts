import { useQuery } from '@tanstack/vue-query';
import httpClient from '@/http-client';
import type { AxiosResponse } from 'axios';
import type { Visit } from '@/types/visit.ts';

const useGetVisits = () => {
    return useQuery({
        queryKey: ['useGetVisits'],
        queryFn: async (): Promise<Visit[]> => {
            const response: AxiosResponse<Visit[]> = await httpClient.get('/api/visit');

            return response.data;
        },
    });
};

export default useGetVisits;
