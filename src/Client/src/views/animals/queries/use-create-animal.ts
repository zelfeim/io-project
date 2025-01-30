import { useMutation } from '@tanstack/vue-query';
import type { AnimalCreate } from '@/types/animal.ts';
import httpClient from '@/http-client';

const useCreateAnimal = () => {
    return useMutation({
        mutationKey: ['useCreateAnimal'],
        mutationFn: async (data: AnimalCreate): Promise<void> => {
            await httpClient.post('/api/animal', data);
        },
    });
};

export default useCreateAnimal;
