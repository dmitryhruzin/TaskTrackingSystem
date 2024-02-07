import { IStatus } from "../../../models/IStatus"
import { IStatusFormError } from "../../../models/IStatusFormError"
import { StatusAction, StatusActionEnum, StatusState } from "./types"


const initialState: StatusState = {
    error: '',
    isLoading: false,
    status: {} as IStatus,
    statusError: {} as IStatusFormError
}

export default function statusReducer(state = initialState, action: StatusAction): StatusState {
    switch (action.type) {
        case StatusActionEnum.SET_STATUS:
            return { ...state, status: action.payload }
        case StatusActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case StatusActionEnum.SET_STATUS_ERROR:
            return { ...state, statusError: action.payload }
        case StatusActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}