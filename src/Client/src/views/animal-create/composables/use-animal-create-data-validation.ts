import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import type { AnimalCreateFormData } from '@/views/animal-create/types';
import { requiredValidator } from '@/validators';
import { ref, type Ref } from 'vue';

const useAnimalCreateDataValidation = () => {
    const data: Ref<AnimalCreateFormData> = ref({
        age: 0,
        name: '',
        animalOwnerId: null,
        race: '',
        species: '',
    });

    const rules: ValidationArgs<AnimalCreateFormData> = {
        age: {
            required: requiredValidator,
        },
        name: {
            required: requiredValidator,
        },
        animalOwnerId: {
            required: requiredValidator,
        },
        race: {
            required: requiredValidator,
        },
        species: {
            required: requiredValidator,
        },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useAnimalCreateDataValidation;
