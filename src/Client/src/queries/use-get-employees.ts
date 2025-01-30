import { useQuery } from '@tanstack/vue-query';
import type { Employee } from '@/types/employee.ts';
import type { AxiosResponse } from 'axios';
import httpClient from '@/http-client';

const useGetEmployees = () => {
    return useQuery({
        queryKey: ['useGetEmployees'],
        queryFn: async (): Promise<Employee[]> => {
            const response: AxiosResponse<Employee[]> = await httpClient.get('/api/employee');

            return response.data;
        },
        placeholderData: [],
        initialData: [],
    });
};

export default useGetEmployees;
