<script setup lang="ts">
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import useAnimalOwnerCreateDataValidation from '@/views/animal-owner-create/composables/use-animal-owner-create-data-validation.ts';
import useCreateAnimalOwner from '@/views/animal-owner-create/queries/use-create-animal-owner.ts';
import { useRouter } from 'vue-router';
import RoutePath from '@/enums/route-path.ts';
import { ElNotification } from 'element-plus';

const { v$ } = useAnimalOwnerCreateDataValidation();
const { mutateAsync: createAnimalOwner } = useCreateAnimalOwner();
const { push } = useRouter();

const handleAdd = async (): Promise<void> => {
    await createAnimalOwner({
        name: v$.value.name.$model,
        surname: v$.value.surname.$model,
        email: v$.value.email.$model,
        telephone: v$.value.telephone.$model,
        address: v$.value.address.$model,
    });

    await push({ path: RoutePath.ANIMAL_OWNERS });

    ElNotification({
        title: `Utworzono właściciela ${v$.value.name.$model} ${v$.value.surname.$model}`,
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
                <label>Adres email *</label>

                <el-input @blur="v$.email.$touch()" size="large" v-model="v$.email.$model" placeholder="Adres email" />

                <control-error-component :v$="v$.email" />
            </div>

            <div class="control-wrapper">
                <label>Telefon *</label>

                <el-input
                    @blur="v$.telephone.$touch()"
                    size="large"
                    v-model="v$.telephone.$model"
                    placeholder="Telefon"
                />

                <control-error-component :v$="v$.telephone" />
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

            <p class="form-disclaimer">* Pole wymagane</p>
        </div>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.ANIMAL_OWNERS })" type="text">Wróć</el-button>
            <el-button :disabled="v$.$invalid" @click="handleAdd" type="primary">Dodaj</el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
