import { useMutation } from '@tanstack/vue-query';
import type { EmployeeCreateRequest } from '@/types/employee.ts';
import httpClient from '@/http-client';

const useCreateEmployee = () => {
    return useMutation({
        mutationKey: ['useCreateEmployee'],
        mutationFn: async (employee: EmployeeCreateRequest): Promise<void> => {
            await httpClient.post('/api/employee', employee);
        },
    });
};

export default useCreateEmployee;
