import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';

const useDeleteEmployee = () => {
    return useMutation({
        mutationKey: ['useDeleteEmployee'],
        mutationFn: async (id: number): Promise<void> => {
            await httpClient.delete(`/api/employee/${id}`);
        },
    });
};

export default useDeleteEmployee;
