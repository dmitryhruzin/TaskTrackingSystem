import { IStatus } from "../../../models/IStatus"


export interface StatusesState {
    statuses: IStatus[]
    isLoading: boolean
    error: string
}

export enum StatusesActionEnum {
    SET_ERROR = 'SET_STATUSES_ERROR',
    SET_STATUSES = 'SET_STATUSES',
    SET_IS_LOADING = "SET_STATUSES_IS_LOADING"
}

export interface SetErrorAction {
    type: StatusesActionEnum.SET_ERROR
    payload: string
}
export interface SetStatusesAction {
    type: StatusesActionEnum.SET_STATUSES
    payload: IStatus[]
}
export interface SetIsLoadingAction {
    type: StatusesActionEnum.SET_IS_LOADING
    payload: boolean
}
export type StatusesAction =
    SetErrorAction |
    SetStatusesAction |
    SetIsLoadingAction 