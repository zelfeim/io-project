import type { Ref } from 'vue';
import { ref } from 'vue';
import type { RescheduleVisitRequest } from '@/types/visit.ts';
import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import { requiredValidator } from '@/validators';

const useRescheduleVisitValidation = () => {
    const defaultDate: Date = new Date();
    defaultDate.setDate(defaultDate.getDate() + 1);

    const data: Ref<RescheduleVisitRequest> = ref({
        date: defaultDate.toString(),
        visitLength: 30,
    });

    const rules: ValidationArgs<RescheduleVisitRequest> = {
        date: {
            required: requiredValidator,
        },
        visitLength: {
            required: requiredValidator,
        },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useRescheduleVisitValidation;
