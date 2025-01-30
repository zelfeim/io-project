<script setup lang="ts">
import ControlErrorComponent from '@/components/control-error/ControlErrorComponent.vue';
import useResourcesCreateValidation from '@/views/resources-create/composables/use-resources-create-validation.ts';
import RoutePath from '@/enums/route-path.ts';
import { useRouter } from 'vue-router';
import useCreateResource from '@/views/resources-create/queries/use-create-resource.ts';
import { ElNotification } from 'element-plus';
import ResourceType from '@/enums/resource-type.ts';
import { RESOURCE_TYPE_TRANSLATIONS } from '@/translations/resource.ts';

const { push } = useRouter();
const { v$ } = useResourcesCreateValidation();
const { mutateAsync: createResource } = useCreateResource();

const handleCreate = async (): Promise<void> => {
    await createResource({
        type: v$.value.type.$model,
        amount: v$.value.amount.$model,
        name: v$.value.name.$model,
        shelfLife: new Date(v$.value.shelfLife.$model).toISOString(),
    });

    await push({ path: RoutePath.RESOURCES });

    ElNotification({
        title: 'Stworzono zasób',
    });
};
</script>

<template>
    <div class="content-width">
        <div class="form">
            <div class="control-wrapper">
                <label>Nazwa *</label>

                <el-input @blur="v$.name.$touch()" size="large" v-model="v$.name.$model" placeholder="Nazwa" />

                <control-error-component :v$="v$.name" />
            </div>

            <div class="control-wrapper">
                <div>Typ *</div>

                <el-select @blur="v$.type.$touch()" size="large" v-model="v$.type.$model" placeholder="Typ">
                    <el-option
                        v-for="type in Object.values(ResourceType)"
                        :key="type"
                        :label="RESOURCE_TYPE_TRANSLATIONS[type]"
                        :value="type"
                    />
                </el-select>

                <control-error-component :v$="v$.type" />
            </div>

            <div class="control-wrapper">
                <div>Czas przydatności *</div>

                <el-date-picker
                    type="datetime"
                    @blur="v$.shelfLife.$touch()"
                    size="large"
                    v-model="v$.shelfLife.$model"
                />

                <control-error-component :v$="v$.shelfLife" />
            </div>

            <div class="control-wrapper">
                <div>Ilość *</div>

                <el-input-number @blur="v$.amount.$touch()" size="large" :min="1" v-model="v$.amount.$model" />

                <control-error-component :v$="v$.amount" />
            </div>

            <p class="form-disclaimer">* Pole wymagane</p>

            <div class="buttons-wrapper">
                <el-button @click="push({ path: RoutePath.RESOURCES })" type="text">Wróć</el-button>

                <el-button @click="handleCreate" :disabled="v$.$invalid" type="primary">Dodaj</el-button>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
