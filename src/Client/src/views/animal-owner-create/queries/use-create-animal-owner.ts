import { useMutation } from '@tanstack/vue-query';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';
import type { CreateAnimalOwner } from '@/types/animal-owner.ts';

const useCreateAnimalOwner = () => {
    return useMutation({
        mutationKey: ['useCreateAnimalOwner'],
        mutationFn: async (request: CreateAnimalOwner) => {
            const res: AxiosResponse<number> = httpClient.post('/api/animal-owner', request);

            return res;
        },
    });
};

export default useCreateAnimalOwner;
