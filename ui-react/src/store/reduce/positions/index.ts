import { IPosition } from "../../../models/IPosition"
import { PositionsAction, PositionsActionEnum, PositionsState } from "./types"


const initialState: PositionsState = {
    error: '',
    isLoading: false,
    positions: [] as IPosition[]
}

export default function positionsReducer(state = initialState, action: PositionsAction): PositionsState {
    switch (action.type) {
        case PositionsActionEnum.SET_POSITIONS:
            return { ...state, positions: action.payload }
        case PositionsActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case PositionsActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}