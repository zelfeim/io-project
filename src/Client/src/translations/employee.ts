import Role from '@/enums/role.ts';

const ROLE_TRANSLATIONS: Record<Role, string> = {
    [Role.VET]: 'Lekarz',
    [Role.RECEPTIONIST]: 'Rejestrator',
    [Role.ADMIN]: 'Administrator',
};

export { ROLE_TRANSLATIONS };
