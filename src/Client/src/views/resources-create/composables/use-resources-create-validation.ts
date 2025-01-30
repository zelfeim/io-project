import type { Ref } from 'vue';
import { ref } from 'vue';
import type { CreateResourceRequest } from '@/types/resource.ts';
import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import { requiredValidator } from '@/validators';

const useResourcesCreateValidation = () => {
    const data: Ref<CreateResourceRequest> = ref({
        type: null,
        name: '',
        amount: 1,
        shelfLife: new Date().toString(),
    });

    const rules: ValidationArgs<CreateResourceRequest> = {
        type: {
            required: requiredValidator,
        },
        name: {
            required: requiredValidator,
        },
        amount: {
            required: requiredValidator,
        },
        shelfLife: {
            required: requiredValidator,
        },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useResourcesCreateValidation;
