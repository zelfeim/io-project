import ResourceType from '@/enums/resource-type.ts';

const RESOURCE_TYPE_TRANSLATIONS: Record<ResourceType, string> = {
    [ResourceType.EQUIPMENT]: 'Wyposażenie',
    [ResourceType.MEDICINE]: 'Lek',
};

export { RESOURCE_TYPE_TRANSLATIONS };
