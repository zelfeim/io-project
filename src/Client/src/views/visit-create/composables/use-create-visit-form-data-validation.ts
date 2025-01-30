import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import type { CreateVisitFormData } from '@/views/visit-create/types';
import { requiredValidator } from '@/validators';
import { ref, type Ref } from 'vue';

const useCreateVisitFormDataValidation = () => {
    const defaultDate: Date = new Date();
    defaultDate.setDate(defaultDate.getDate() + 1);

    const data: Ref<CreateVisitFormData> = ref({
        type: null,
        date: defaultDate,
        animalId: null,
        visitInformation: '',
        visitLength: 0,
        employeeId: null,
    });

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
