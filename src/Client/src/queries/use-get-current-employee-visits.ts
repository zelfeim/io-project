import { useQuery } from '@tanstack/vue-query';
import type { Visit } from '@/types/visit.ts';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';

const useGetCurrentEmployeeVisits = () => {
    return useQuery({
        queryKey: ['useGetCurrentEmployeeVisits'],
        queryFn: async (): Promise<Visit[]> => {
            const response: AxiosResponse<Visit[]> = await httpClient.get('/api/employee/visits');

            return response.data;
        },
    });
};

export default useGetCurrentEmployeeVisits;
