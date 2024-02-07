import { IRole } from "../../../models/IRole"
import { IRoleFormError } from "../../../models/IRoleFormError"


export interface RoleState {
    role: IRole
    isLoading: boolean
    error: string
    roleError: IRoleFormError
}

export enum RoleActionEnum {
    SET_ERROR = 'SET_ROLE_ERROR',
    SET_ROLE_ERROR = 'SET_ROLE_FORM_ERROR',
    SET_ROLE = 'SET_ROLE',
    SET_IS_LOADING = "SET_ROLE_IS_LOADING"
}

export interface SetErrorAction {
    type: RoleActionEnum.SET_ERROR
    payload: string
}
export interface SetRoleErrorAction {
    type: RoleActionEnum.SET_ROLE_ERROR
    payload: IRoleFormError
}
export interface SetRoleAction {
    type: RoleActionEnum.SET_ROLE
    payload: IRole
}
export interface SetIsLoadingAction {
    type: RoleActionEnum.SET_IS_LOADING
    payload: boolean
}
export type RoleAction =
    SetErrorAction |
    SetRoleAction |
    SetIsLoadingAction |
    SetRoleErrorAction