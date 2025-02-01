<script setup lang="ts">
import useAnimalCreateDataValidation from '@/views/animal-create/composables/use-animal-create-data-validation.ts';
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import useGetAnimalOwners from '@/views/animal-owners/queries/use-get-animal-owners.ts';
import RoutePath from '@/enums/route-path.ts';
import { useRouter } from 'vue-router';
import useCreateAnimal from '@/views/animals/queries/use-create-animal.ts';
import { ElNotification } from 'element-plus';

const { push } = useRouter();
const { data: owners } = useGetAnimalOwners();
const { v$ } = useAnimalCreateDataValidation();
const { mutateAsync: createAnimal } = useCreateAnimal();

const handleAdd = async () => {
    await createAnimal({
        name: v$.value.name.$model,
        age: v$.value.age.$model,
        race: v$.value.race.$model,
        species: v$.value.species.$model,
        animalOwnerId: v$.value.animalOwnerId.$model as number,
    });

    await push({ path: RoutePath.ANIMALS });

    ElNotification({
        title: 'Dodano zwierzę',
    });
};
</script>

<template>
    <div class="content-width">
        <div class="control-wrapper">
            <label>Imię *</label>
            <el-input @blur="v$.name.$touch()" size="large" v-model="v$.name.$model" placeholder="Imię" />
            <control-error-component :v$="v$.name" />
        </div>

        <div class="control-wrapper">
            <label>Wiek *</label>
            <el-slider show-input @blur="v$.age.$touch()" size="large" v-model="v$.age.$model" :min="0" :max="100" />
            <control-error-component :v$="v$.age" />
        </div>

        <div class="control-wrapper">
            <label>Rasa *</label>
            <el-input @blur="v$.race.$touch()" size="large" v-model="v$.race.$model" placeholder="Rasa" />
            <control-error-component :v$="v$.race" />
        </div>

        <div class="control-wrapper">
            <label>Gatunek *</label>
            <el-input @blur="v$.species.$touch()" size="large" v-model="v$.species.$model" placeholder="Gatunek" />
            <control-error-component :v$="v$.species" />
        </div>

        <div class="control-wrapper">
            <label>Właściciel *</label>

            <el-select
                @blur="v$.animalOwnerId.$touch()"
                v-model="v$.animalOwnerId.$model"
                value-key="id"
                size="large"
                placeholder="Właściciel"
            >
                <el-option
                    v-for="owner in owners"
                    :label="owner.name + ' ' + owner.surname"
                    :key="owner.id"
                    :value="owner.id"
                />
            </el-select>

            <control-error-component :v$="v$.animalOwnerId" />
        </div>

        <p class="form-disclaimer">* Pole wymagane</p>

        <div class="buttons-wrapper">
            <el-button @click="push({ path: RoutePath.ANIMALS })" type="text">Wróć</el-button>

            <el-button @click="handleAdd" :disabled="v$.$invalid" type="primary">Dodaj</el-button>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
