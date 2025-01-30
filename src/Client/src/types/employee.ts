import type Role from '@/enums/role.ts';

type Employee = {
    id: number;
    name: string;
    surname: string;
    email: string;
    role: Role;
    address: string;
};

type EmployeeCreateRequest = {
    name: string;
    surname: string;
    role: Role | null;
    address: string;
    email: string;
    password: string;
};

export type { Employee, EmployeeCreateRequest };
