import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';

const useDeleteAnimalOwner = () => {
    return useMutation({
        mutationKey: ['useDeleteAnimalOwner'],
        mutationFn: async (id: number): Promise<void> => {
            return httpClient.delete(`/api/animal-owner/${id}`);
        },
    });
};

export default useDeleteAnimalOwner;
