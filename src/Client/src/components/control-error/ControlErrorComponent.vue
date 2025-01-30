<script setup lang="ts">
import type { BaseValidation } from '@vuelidate/core';
import type { ComputedRef } from 'vue';
import { computed } from 'vue';

type Props = {
    v$: BaseValidation;
};

const props: Props = defineProps<Props>();

const error: ComputedRef<string | null> = computed((): string | null => {
    if (!props.v$.$errors.length) {
        return null;
    }

    return props.v$.$errors[0].$response.msg;
});
</script>

<template>
    <div class="control-error">{{ error }}</div>
</template>

<style scoped lang="scss">
.control-error {
    height: 30px;
    color: red;
}
</style>
