import { useQuery } from '@tanstack/vue-query';
import type { AnimalOwner } from '@/types/animal-owner.ts';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';

const useGetAnimalOwner = (id: number) => {
    return useQuery({
        queryKey: ['useGetAnimalOwner'],
        queryFn: async (): Promise<AnimalOwner> => {
            const response: AxiosResponse<AnimalOwner> = await httpClient.get(`/api/animal-owner/${id}`);

            return response.data;
        },
    });
};

export default useGetAnimalOwner;
