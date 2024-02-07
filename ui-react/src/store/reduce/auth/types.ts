import { IRole } from "../../../models/IRole"
import { IToken } from "../../../models/IToken"
import { IUser } from "../../../models/IUser"
import { ILoginFormError } from "../../../models/ILoginFormError"
import { IForgotFormError } from "../../../models/IForgotFormError"
import { IResetFormError } from "../../../models/IResetFormError"

export interface AuthState {
    isAuth: boolean
    user: IUser
    roles: IRole[]
    token: IToken
    isLoading: boolean
    isAuthLoading: boolean
    error: ILoginFormError
    pushError: string
    forgotError: IForgotFormError
    resetError: IResetFormError
}

export enum AuthActionEnum {
    SET_AUTH = 'SET_AUTH',
    SET_ERROR = 'SET_AUTH_ERROR',
    SET_FORGOT_ERROR = 'SET_AUTH_FORGOT_ERROR',
    SET_RESET_ERROR = 'SET_AUTH_RESET_ERROR',
    SET_PUSH_ERROR = 'SET_AUTH_PUSH_ERROR',
    SET_USER = 'SET_AUTH_USER',
    SET_ROLES = 'SET_AUTH_ROLES',
    SET_TOKEN = 'SET_AUTH_TOKEN',
    SET_IS_LOADING = 'SET_AUTH_IS_LOADING',
    SET_AUTH_IS_LOADING = 'SET_AUTH_AUTH_IS_LOADING'
}

export interface SetAuthAction {
    type: AuthActionEnum.SET_AUTH
    payload: boolean
}
export interface SetErrorAction {
    type: AuthActionEnum.SET_ERROR
    payload: ILoginFormError
}
export interface SetForgotErrorAction {
    type: AuthActionEnum.SET_FORGOT_ERROR
    payload: IForgotFormError
}
export interface SetResetErrorAction {
    type: AuthActionEnum.SET_RESET_ERROR
    payload: IResetFormError
}
export interface SetPushErrorAction {
    type: AuthActionEnum.SET_PUSH_ERROR
    payload: string
}
export interface SetAuthUserAction {
    type: AuthActionEnum.SET_USER
    payload: IUser
}
export interface SetAuthRolesAction {
    type: AuthActionEnum.SET_ROLES
    payload: IRole[]
}
export interface SetAuthTokenAction {
    type: AuthActionEnum.SET_TOKEN
    payload: IToken
}
export interface SetIsLoadingAction {
    type: AuthActionEnum.SET_IS_LOADING
    payload: boolean
}
export interface SetAuthIsLoadingAction {
    type: AuthActionEnum.SET_AUTH_IS_LOADING
    payload: boolean
}

export type AuthAction =
    SetAuthAction |
    SetErrorAction |
    SetIsLoadingAction |
    SetAuthUserAction |
    SetAuthTokenAction |
    SetAuthRolesAction |
    SetPushErrorAction |
    SetAuthIsLoadingAction |
    SetForgotErrorAction |
    SetResetErrorAction