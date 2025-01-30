import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';

const useDeleteResource = () => {
    return useMutation({
        mutationKey: ['useDeleteResource'],
        mutationFn: async (id: number): Promise<void> => {
            await httpClient.delete(`/api/resource/${id}`);
        },
    });
};

export default useDeleteResource;
