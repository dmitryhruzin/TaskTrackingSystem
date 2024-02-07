import { IUser } from "../../../models/IUser"


export interface UsersState {
    users: IUser[]
    isLoading: boolean
    error: string
}

export enum UsersActionEnum {
    SET_ERROR = 'SET_USERS_ERROR',
    SET_USERS = 'SET_USERS',
    SET_IS_LOADING = "SET_USERS_IS_LOADING"
}

export interface SetErrorAction {
    type: UsersActionEnum.SET_ERROR
    payload: string
}
export interface SetUsersAction {
    type: UsersActionEnum.SET_USERS
    payload: IUser[]
}
export interface SetIsLoadingAction {
    type: UsersActionEnum.SET_IS_LOADING
    payload: boolean
}
export type UsersAction =
    SetErrorAction |
    SetUsersAction |
    SetIsLoadingAction 