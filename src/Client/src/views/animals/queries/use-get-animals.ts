import { useQuery } from '@tanstack/vue-query';
import type { Animal } from '@/types/animal.ts';
import httpClient from '@/http-client';
import type { AxiosResponse } from 'axios';

const useGetAnimals = () => {
    return useQuery({
        queryKey: ['useGetAnimals'],
        queryFn: async (): Promise<Animal[]> => {
            const response: AxiosResponse<Animal[]> = await httpClient.get('/api/animal');

            return response.data;
        },
    });
};

export default useGetAnimals;
