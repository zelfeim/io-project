import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';
import type { CreateVisit } from '@/types/visit.ts';

const useCreateVisit = () => {
    return useMutation({
        mutationKey: ['useCreateVisit'],
        mutationFn: async (body: CreateVisit): Promise<void> => {
            await httpClient.post('/api/visit', body);
        },
    });
};

export default useCreateVisit;
