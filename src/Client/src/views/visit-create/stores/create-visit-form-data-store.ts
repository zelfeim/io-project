import { defineStore } from 'pinia';
import type { Ref } from 'vue';
import { ref } from 'vue';
import type { CreateVisitFormData } from '@/views/visit-create/types';

const createVisitFormDataStore = defineStore('createVisitFormDataStore', () => {
    const defaultDate: Date = new Date();
    defaultDate.setDate(defaultDate.getDate() + 1);

    const data: Ref<CreateVisitFormData> = ref({
        type: null,
        date: defaultDate,
        animalId: null,
        visitInformation: '',
        visitLength: 0,
        employeeId: null,
    });

    return {
        data,
    };
});

export default createVisitFormDataStore;
