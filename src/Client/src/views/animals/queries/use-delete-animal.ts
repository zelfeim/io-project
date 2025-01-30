import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';

const useDeleteAnimal = () => {
    return useMutation({
        mutationKey: ['useDeleteAnimal'],
        mutationFn: async (id: number): Promise<void> => {
            await httpClient.delete(`/api/animal/${id}`);
        },
    });
};

export default useDeleteAnimal;
