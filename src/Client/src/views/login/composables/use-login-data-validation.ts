import { storeToRefs } from 'pinia';
import loginDataStore from '@/views/login/stores/login-data-store.ts';
import type { ValidationArgs } from '@vuelidate/core';
import { useVuelidate } from '@vuelidate/core';
import type { LoginData } from '@/views/login/types/login-data.ts';
import { requiredValidator, usePatternValidator } from '@/validators';
import { EMAIL_PATTERN } from '@/patterns';

const useLoginDataValidation = () => {
    const { data } = storeToRefs(loginDataStore());

    const rules: ValidationArgs<LoginData> = {
        login: {
            required: requiredValidator,
            pattern: usePatternValidator(EMAIL_PATTERN, 'Niepoprawny adres e-mail'),
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

export default useLoginDataValidation;
