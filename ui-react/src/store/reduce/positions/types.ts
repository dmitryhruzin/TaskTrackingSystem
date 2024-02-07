import { IPosition } from "../../../models/IPosition"


export interface PositionsState {
    positions: IPosition[]
    isLoading: boolean
    error: string
}

export enum PositionsActionEnum {
    SET_ERROR = 'SET_POSITIONS_ERROR',
    SET_POSITIONS = 'SET_POSITIONS',
    SET_IS_LOADING = "SET_POSITIONS_IS_LOADING"
}

export interface SetErrorAction {
    type: PositionsActionEnum.SET_ERROR
    payload: string
}
export interface SetPositionsAction {
    type: PositionsActionEnum.SET_POSITIONS
    payload: IPosition[]
}
export interface SetIsLoadingAction {
    type: PositionsActionEnum.SET_IS_LOADING
    payload: boolean
}
export type PositionsAction =
    SetErrorAction |
    SetPositionsAction |
    SetIsLoadingAction 