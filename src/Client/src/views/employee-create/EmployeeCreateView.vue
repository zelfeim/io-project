<script setup lang="ts">
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import useCreateEmployeeValidation from '@/views/employee-create/composables/use-create-employee-validation.ts';
import Role from '@/enums/role.ts';
import { ROLE_TRANSLATIONS } from '@/translations/employee.ts';
import useCreateEmployee from '@/views/employee-create/queries/use-create-employee.ts';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import { ElNotification } from 'element-plus';

const { v$ } = useCreateEmployeeValidation();
const { push } = useRouter();
const { mutateAsync: createEmployee } = useCreateEmployee();

const handleCreate = async (): Promise<void> => {
    await createEmployee({
        name: v$.value.name.$model,
        surname: v$.value.surname.$model,
        role: v$.value.role.$model,
        address: v$.value.address.$model,
        email: v$.value.email.$model,
        password: v$.value.password.$model,
    });

    await push({ path: RoutePath.EMPLOYEES });

    ElNotification({
        title: 'Utworzono pracownika',
    });
};
</script>

<template>
    <div class="content-width">
        <div class="form">
            <div class="control-wrapper">
                <label>Imię *</label>

                <el-input @blur="v$.name.$touch()" size="large" v-model="v$.name.$model" placeholder="Imię" />

                <control-error-component :v$="v$.name" />
            </div>

            <div class="control-wrapper">
                <label>Nazwisko *</label>

                <el-input @blur="v$.surname.$touch()" size="large" v-model="v$.surname.$model" placeholder="Nazwisko" />

                <control-error-component :v$="v$.surname" />
            </div>

            <div class="control-wrapper">
                <label>Email *</label>

                <el-input @blur="v$.email.$touch()" size="large" v-model="v$.email.$model" placeholder="Email" />

                <control-error-component :v$="v$.email" />
            </div>

            <div class="control-wrapper">
                <label>Adres *</label>

                <el-input
                    type="textarea"
                    @blur="v$.address.$touch()"
                    size="large"
                    v-model="v$.address.$model"
                    placeholder="Adres"
                />

                <control-error-component :v$="v$.address" />
            </div>

            <div class="control-wrapper">
                <label>Rola *</label>

                <el-select @blur="v$.role.$touch()" size="large" v-model="v$.role.$model" placeholder="Wybierz rolę">
                    <el-option
                        v-for="role in Object.values(Role)"
                        :key="role"
                        :value="role"
                        :label="ROLE_TRANSLATIONS[role]"
                    ></el-option>
                </el-select>

                <control-error-component :v$="v$.address" />
            </div>

            <div class="control-wrapper">
                <label>Hasło *</label>

                <el-input
                    placeholder="Hasło"
                    type="password"
                    @blur="v$.password.$touch()"
                    size="large"
                    v-model="v$.password.$model"
                />

                <control-error-component :v$="v$.password" />
            </div>

            <p class="form-disclaimer">* Pole wymagane</p>
        </div>

        <div class="buttons-wrapper">
            <el-button link type="primary" @click="push({ path: RoutePath.EMPLOYEES })">Wróć</el-button>
            <el-button :disabled="v$.$invalid" type="primary" @click="handleCreate()">Dodaj</el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
