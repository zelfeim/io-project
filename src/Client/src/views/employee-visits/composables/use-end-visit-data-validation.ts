import type { Ref } from 'vue';
import { ref } from 'vue';
import type { EndVisitRequest } from '@/types/visit.ts';
import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import { requiredValidator } from '@/validators';

const useEndVisitDataValidation = () => {
    const data: Ref<EndVisitRequest> = ref({
        prescribedMeds: '',
        suggestedTreatment: '',
    });

    const rules: ValidationArgs<EndVisitRequest> = {
        prescribedMeds: {
            required: requiredValidator,
        },
        suggestedTreatment: {
            required: requiredValidator,
        },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useEndVisitDataValidation;
