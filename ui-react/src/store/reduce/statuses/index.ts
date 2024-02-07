import { IStatus } from "../../../models/IStatus"
import { StatusesAction, StatusesActionEnum, StatusesState } from "./types"


const initialState: StatusesState = {
    error: '',
    isLoading: false,
    statuses: [] as IStatus[]
}

export default function statusesReducer(state = initialState, action: StatusesAction): StatusesState {
    switch (action.type) {
        case StatusesActionEnum.SET_STATUSES:
            return { ...state, statuses: action.payload }
        case StatusesActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case StatusesActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}