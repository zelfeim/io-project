import { useQuery } from '@tanstack/vue-query';
import type { AxiosResponse } from 'axios';
import type { Visit } from '@/types/visit.ts';
import httpClient from '@/http-client';

const useGetAnimalVisits = (id: number) => {
    return useQuery({
        queryKey: ['useGetAnimalVisits', id],
        queryFn: async (): Promise<Visit[]> => {
            const response: AxiosResponse<Visit[]> = await httpClient.get(`/api/animal/${id}/visits`);

            return response.data;
        },
    });
};

export default useGetAnimalVisits;
