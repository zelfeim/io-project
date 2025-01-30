import { useMutation } from '@tanstack/vue-query';
import type { CreateResourceRequest } from '@/types/resource.ts';
import httpClient from '@/http-client';

const useCreateResource = () => {
    return useMutation({
        mutationKey: ['useCreateResource'],
        mutationFn: async (resource: CreateResourceRequest): Promise<void> => {
            await httpClient.post('/api/resource', resource);
        },
    });
};

export default useCreateResource;
