import { IRole } from "../../../models/IRole"


export interface RolesState {
    roles: IRole[]
    isLoading: boolean
    error: string
}

export enum RolesActionEnum {
    SET_ERROR = 'SET_ROLES_ERROR',
    SET_ROLES = 'SET_ROLES',
    SET_IS_LOADING = "SET_ROLES_IS_LOADING"
}

export interface SetErrorAction {
    type: RolesActionEnum.SET_ERROR
    payload: string
}
export interface SetRolesAction {
    type: RolesActionEnum.SET_ROLES
    payload: IRole[]
}
export interface SetIsLoadingAction {
    type: RolesActionEnum.SET_IS_LOADING
    payload: boolean
}
export type RolesAction =
    SetErrorAction |
    SetRolesAction |
    SetIsLoadingAction 