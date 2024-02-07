import { IRole } from "../../../models/IRole"
import { RolesAction, RolesActionEnum, RolesState } from "./types"


const initialState: RolesState = {
    error: '',
    isLoading: false,
    roles: [] as IRole[]
}

export default function rolesReducer(state = initialState, action: RolesAction): RolesState {
    switch (action.type) {
        case RolesActionEnum.SET_ROLES:
            return { ...state, roles: action.payload }
        case RolesActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case RolesActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}