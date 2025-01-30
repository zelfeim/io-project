import { useQuery } from '@tanstack/vue-query';
import type { AxiosResponse } from 'axios';
import type { AnimalOwner } from '@/types/animal-owner.ts';
import httpClient from '@/http-client';

const useGetAnimalOwners = () => {
    return useQuery({
        queryKey: ['useGetAnimalOwners'],
        queryFn: async (): Promise<AnimalOwner[]> => {
            const response: AxiosResponse<AnimalOwner[]> = await httpClient.get('/api/animal-owner');

            return response.data;
        },
        placeholderData: [],
        initialData: [],
    });
};

export default useGetAnimalOwners;
