import { IPosition } from "../../../models/IPosition"
import { IPositionFormError } from "../../../models/IPositionFormError"


export interface PositionState {
    position: IPosition
    isLoading: boolean
    error: string
    positionError: IPositionFormError
}

export enum PositionActionEnum {
    SET_ERROR = 'SET_POSITION_ERROR',
    SET_POSITION_ERROR = 'SET_POSITION_FORM_ERROR',
    SET_POSITION = 'SET_POSITION',
    SET_IS_LOADING = "SET_POSITION_IS_LOADING"
}

export interface SetErrorAction {
    type: PositionActionEnum.SET_ERROR
    payload: string
}
export interface SetPositionErrorAction {
    type: PositionActionEnum.SET_POSITION_ERROR
    payload: IPositionFormError
}
export interface SetPositionAction {
    type: PositionActionEnum.SET_POSITION
    payload: IPosition
}
export interface SetIsLoadingAction {
    type: PositionActionEnum.SET_IS_LOADING
    payload: boolean
}
export type PositionAction =
    SetErrorAction |
    SetPositionAction |
    SetIsLoadingAction |
    SetPositionErrorAction