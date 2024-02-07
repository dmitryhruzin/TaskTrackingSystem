import { IRole } from "../../../models/IRole"
import { IRoleFormError } from "../../../models/IRoleFormError"
import { RoleAction, RoleActionEnum, RoleState } from "./types"


const initialState: RoleState = {
    error: '',
    isLoading: false,
    role: {} as IRole,
    roleError: {} as IRoleFormError
}

export default function roleReducer(state = initialState, action: RoleAction): RoleState {
    switch (action.type) {
        case RoleActionEnum.SET_ROLE:
            return { ...state, role: action.payload }
        case RoleActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case RoleActionEnum.SET_ROLE_ERROR:
            return { ...state, roleError: action.payload }
        case RoleActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}