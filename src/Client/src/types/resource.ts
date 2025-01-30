type Resource = {
    name: string;
    type: Resource;
    amount: number;
    shelfLife: string;
    id: number;
};

type CreateResourceRequest = {
    name: string;
    type: Resource | null;
    amount: number;
    shelfLife: string;
};

type UpdateResourceRequest = {
    amount: number;
};

export type { Resource, CreateResourceRequest, UpdateResourceRequest };
