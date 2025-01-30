import type VisitType from '@/enums/visit-type.ts';
import type VisitStatus from '@/enums/visit-status.ts';

type Prescription = {
    prescribedMeds: string;
};

type Visit = {
    id: number;
    animalId: number;
    employeeId: number;
    date: string;
    visitLength: number;
    visitType: VisitType;
    visitStatus: VisitStatus;
    visitInformation: string;
    suggestedTreatment: string;
    prescription: Prescription;
};

type CreateVisit = {
    animalId: number;
    date: string;
    employeeId: number;
    type: VisitType;
    visitInformation: string;
    visitLength: number;
};

type EndVisitRequest = {
    suggestedTreatment: string;
    prescribedMeds: string;
};

type RescheduleVisitRequest = {
    date: string;
    visitLength: number;
};

export type { Prescription, Visit, CreateVisit, EndVisitRequest, RescheduleVisitRequest };
