import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';

const useCancelVisit = () => {
    return useMutation({
        mutationKey: ['useCancelVisit'],
        mutationFn: async (id: number): Promise<void> => {
            await httpClient.put(`/api/visit/${id}/cancel`);
        },
    });
};

export default useCancelVisit;
