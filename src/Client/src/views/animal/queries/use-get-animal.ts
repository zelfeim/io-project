import { useQuery } from '@tanstack/vue-query';
import type { AxiosResponse } from 'axios';
import type { Animal } from '@/types/animal.ts';
import httpClient from '@/http-client';

const useGetAnimal = (id: number) => {
    return useQuery({
        queryKey: ['useGetAnimal', id],
        queryFn: async (): Promise<Animal> => {
            const response: AxiosResponse<Animal> = await httpClient.get(`/api/animal/${id}`);

            return response.data;
        },
    });
};

export default useGetAnimal;
