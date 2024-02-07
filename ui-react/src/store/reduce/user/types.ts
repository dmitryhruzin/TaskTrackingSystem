import { IRegisterFormError } from "../../../models/IRegisterFormError"
import { IRole } from "../../../models/IRole"
import { IUpdateUserFormError } from "../../../models/IUpdateUserFormError"
import { IUser } from "../../../models/IUser"


export interface UserState {
    user: IUser
    roles: IRole[]
    isLoading: boolean
    error: string
    registerError: IRegisterFormError
    updateError: IUpdateUserFormError
}

export enum UserActionEnum {
    SET_ERROR = 'SET_USER_ERROR',
    SET_REGISTER_ERROR = 'SET_USER_REGISTER_ERROR',
    SET_UPDATE_ERROR = 'SET_USER_UPDATE_ERROR',
    SET_USER = 'SET_USER',
    SET_USER_ROLE = 'SET_USER_ROLE',
    SET_IS_LOADING = "SET_USER_IS_LOADING"
}

export interface SetErrorAction {
    type: UserActionEnum.SET_ERROR
    payload: string
}
export interface SetRegisterErrorAction {
    type: UserActionEnum.SET_REGISTER_ERROR
    payload: IRegisterFormError
}
export interface SetUpdateErrorAction {
    type: UserActionEnum.SET_UPDATE_ERROR
    payload: IUpdateUserFormError
}
export interface SetUserAction {
    type: UserActionEnum.SET_USER
    payload: IUser
}
export interface SetUserRoleAction {
    type: UserActionEnum.SET_USER_ROLE
    payload: IRole[]
}
export interface SetIsLoadingAction {
    type: UserActionEnum.SET_IS_LOADING
    payload: boolean
}
export type UserAction =
    SetErrorAction |
    SetUserAction |
    SetIsLoadingAction |
    SetRegisterErrorAction |
    SetUpdateErrorAction |
    SetUserRoleAction