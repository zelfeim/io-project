import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';

type Config = {
    id: number;
    amount: number;
};

const useUpdateResourceAmount = () => {
    return useMutation({
        mutationKey: ['useUpdateResourceAmount'],
        mutationFn: async (config: Config): Promise<void> => {
            await httpClient.put(`/api/resource/${config.id}`, { amount: config.amount });
        },
    });
};

export default useUpdateResourceAmount;
