import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import type { CreateAnimalOwner } from '@/types/animal-owner.ts';
import { requiredValidator } from '@/validators';
import { ref, type Ref } from 'vue';

const useAnimalOwnerCreateDataValidation = () => {
    const data: Ref<CreateAnimalOwner> = ref({
        name: '',
        surname: '',
        email: '',
        telephone: '',
        address: '',
    });

    const rules: ValidationArgs<CreateAnimalOwner> = {
        name: { required: requiredValidator },
        surname: { required: requiredValidator },
        email: { required: requiredValidator },
        telephone: { required: requiredValidator },
        address: { required: requiredValidator },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useAnimalOwnerCreateDataValidation;
