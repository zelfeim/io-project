import type VisitType from '@/enums/visit-type.ts';

type CreateVisitFormData = {
    animalId: number | null;
    date: Date | null;
    employeeId: number | null;
    type: VisitType | null;
    visitInformation: string;
    visitLength: number;
};

export type { CreateVisitFormData };
