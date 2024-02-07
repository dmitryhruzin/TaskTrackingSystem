import { IStatus } from "../../../models/IStatus"
import { IStatusFormError } from "../../../models/IStatusFormError"


export interface StatusState {
    status: IStatus
    isLoading: boolean
    error: string
    statusError: IStatusFormError
}

export enum StatusActionEnum {
    SET_ERROR = 'SET_STATUS_ERROR',
    SET_STATUS_ERROR = 'SET_STATUS_FORM_ERROR',
    SET_STATUS = 'SET_STATUS',
    SET_IS_LOADING = "SET_STATUS_IS_LOADING"
}

export interface SetErrorAction {
    type: StatusActionEnum.SET_ERROR
    payload: string
}
export interface SetStatusErrorAction {
    type: StatusActionEnum.SET_STATUS_ERROR
    payload: IStatusFormError
}
export interface SetStatusAction {
    type: StatusActionEnum.SET_STATUS
    payload: IStatus
}
export interface SetIsLoadingAction {
    type: StatusActionEnum.SET_IS_LOADING
    payload: boolean
}
export type StatusAction =
    SetErrorAction |
    SetStatusAction |
    SetIsLoadingAction |
    SetStatusErrorAction