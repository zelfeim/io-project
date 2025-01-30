import VisitType from '@/enums/visit-type.ts';
import VisitStatus from '@/enums/visit-status.ts';

const VISIT_TYPE_TRANSLATIONS: Record<VisitType, string> = {
    [VisitType.EXAMINATION]: 'Badanie',
    [VisitType.OPERATION]: 'Operacja',
};

const VISIT_STATUS_TRANSLATIONS: Record<VisitStatus, string> = {
    [VisitStatus.CANCELLED]: 'Anulowana',
    [VisitStatus.COMPLETED]: 'Ukończona',
    [VisitStatus.PLANNED]: 'Zaplanowana',
};

export { VISIT_STATUS_TRANSLATIONS, VISIT_TYPE_TRANSLATIONS };
