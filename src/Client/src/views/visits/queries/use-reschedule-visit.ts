import { useMutation } from '@tanstack/vue-query';
import httpClient from '@/http-client';
import { type RescheduleVisitRequest } from '@/types/visit.ts';

type Config = {
    id: number;
    request: RescheduleVisitRequest;
};

const useRescheduleVisit = () => {
    return useMutation({
        mutationKey: ['useRescheduleVisit'],
        mutationFn: async (config: Config): Promise<void> => {
            await httpClient.put(`/visit/${config.id}/reschedule`, config.request);
        },
    });
};

export default useRescheduleVisit;
