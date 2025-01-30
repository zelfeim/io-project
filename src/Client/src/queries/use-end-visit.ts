import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';
import type { EndVisitRequest } from '@/types/visit.ts';

type Config = {
    id: number;
    request: EndVisitRequest;
};

const useEndVisit = () => {
    return useMutation({
        mutationKey: ['useEndVisit'],
        mutationFn: async (config: Config): Promise<void> => {
            await httpClient.post(`/api/visit/${config.id}/end`, config.request);
        },
    });
};

export default useEndVisit;
