import { useQuery } from '@tanstack/vue-query';
import type { Animal } from '@/types/animal.ts';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';

const useGetOwnerAnimals = (id: number) => {
    return useQuery({
        queryKey: ['useGetOwnerAnimals'],
        queryFn: async (): Promise<Animal[]> => {
            const response: AxiosResponse<Animal[]> = await httpClient.get(`/api/animal-owner/${id}/animals`);

            return response.data;
        },
    });
};

export default useGetOwnerAnimals;
