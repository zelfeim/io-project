import type { Ref } from 'vue';
import { ref } from 'vue';
import type { EmployeeCreateRequest } from '@/types/employee.ts';
import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import { requiredValidator } from '@/validators';

const useCreateEmployeeValidation = () => {
    const data: Ref<EmployeeCreateRequest> = ref({
        name: '',
        surname: '',
        email: '',
        address: '',
        role: null,
        password: '',
    });

    const rules: ValidationArgs<EmployeeCreateRequest> = {
        name: {
            required: requiredValidator,
        },
        surname: {
            required: requiredValidator,
        },
        email: {
            required: requiredValidator,
        },
        address: {
            required: requiredValidator,
        },
        role: {
            required: requiredValidator,
        },
        password: {
            required: requiredValidator,
        },
    };

    const v$ = useVuelidate(rules, data);

    return {
        v$,
    };
};

export default useCreateEmployeeValidation;
