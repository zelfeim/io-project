type CreateAnimalOwner = {
    name: string;
    surname: string;
    email: string;
    telephone: string;
    address: string;
};

type AnimalOwner = {
    id: number;
    name: string;
    surname: string;
    email: {
        email: string;
    };
    telephone: string;
    address: string;
};

export { type AnimalOwner, type CreateAnimalOwner };
