import { IPosition } from "../../../models/IPosition"
import { IPositionFormError } from "../../../models/IPositionFormError"
import { PositionAction, PositionActionEnum, PositionState } from "./types"


const initialState: PositionState = {
    error: '',
    isLoading: false,
    position: {} as IPosition,
    positionError: {} as IPositionFormError
}

export default function positionReducer(state = initialState, action: PositionAction): PositionState {
    switch (action.type) {
        case PositionActionEnum.SET_POSITION:
            return { ...state, position: action.payload }
        case PositionActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case PositionActionEnum.SET_POSITION_ERROR:
            return { ...state, positionError: action.payload }
        case PositionActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}