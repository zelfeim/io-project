import { storeToRefs } from 'pinia';
import createVisitFormDataStore from '@/views/visit-create/stores/create-visit-form-data-store.ts';
import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import type { CreateVisitFormData } from '@/views/visit-create/types';
import { requiredValidator } from '@/validators';

const useCreateVisitFormDataValidation = () => {
    const { data } = storeToRefs(createVisitFormDataStore());

    const rules: ValidationArgs<CreateVisitFormData> = {
        animalId: {
            required: requiredValidator,
        },
        visitLength: {
            required: requiredValidator,
        },
        type: {
            required: requiredValidator,
        },
        visitInformation: {},
        employeeId: {
            required: requiredValidator,
        },
        date: {
            required: requiredValidator,
        },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useCreateVisitFormDataValidation;
