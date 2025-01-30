type Animal = {
    id: number;
    age: number;
    name: string;
    race: string;
    species: string;
    animalOwnerId: number;
};

type AnimalCreate = {
    name: string;
    age: number;
    race: string;
    species: string;
    animalOwnerId: number;
};

export type { Animal, AnimalCreate };
