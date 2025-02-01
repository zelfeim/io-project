import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';
import type { CreateAnimalOwner } from '@/types/animal-owner.ts';

const useCreateAnimalOwner = () => {
    return useMutation({
        mutationKey: ['useCreateAnimalOwner'],
        mutationFn: async (request: CreateAnimalOwner): Promise<void> => {
            await httpClient.post('/api/animal-owner', request);
        },
    });
};

export default useCreateAnimalOwner;
