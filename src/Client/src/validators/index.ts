import type { ValidatorFn, ValidatorResponse } from '@vuelidate/core';

const requiredValidator: ValidatorFn = <T>(value: T): ValidatorResponse | boolean => {
    if (!value) {
        return {
            $valid: false,
            msg: 'Te pole jest wymagane.',
        };
    }

    return true;
};

const usePatternValidator = (pattern: RegExp, msg: string): ValidatorFn<string> => {
    return (value: string): ValidatorResponse | boolean => {
        if (!pattern.test(value)) {
            return {
                $valid: false,
                msg,
            };
        }

        return true;
    };
};

export { requiredValidator, usePatternValidator };
