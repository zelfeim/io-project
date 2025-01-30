import { useQuery } from '@tanstack/vue-query';
import type { AxiosResponse } from 'axios';
import type { Visit } from '@/types/visit.ts';
import httpClient from '@/http-client';

const useGetVisit = (id: number) => {
    return useQuery({
        queryKey: ['useGetVisit', id],
        queryFn: async (): Promise<Visit> => {
            const response: AxiosResponse<Visit> = await httpClient.get(`/api/visit/${id}`);

            return response.data;
        },
    });
};

export default useGetVisit;
