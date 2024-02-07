export interface IRole {
    id: number
    name: string
}

export enum RoleNames {
    USER = "User",
    MANAGER = "Manager",
    ADMINISTRATOR = "Administrator"
}

export const arrayRole : string[] = [
    RoleNames.ADMINISTRATOR,
    RoleNames.MANAGER,
    RoleNames.USER
]