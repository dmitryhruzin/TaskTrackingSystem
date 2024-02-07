import { IRegisterFormError } from "../../../models/IRegisterFormError"
import { IRole } from "../../../models/IRole"
import { IUpdateUserFormError } from "../../../models/IUpdateUserFormError"
import { IUser } from "../../../models/IUser"
import { UserAction, UserActionEnum, UserState } from "./types"


const initialState: UserState = {
    error: '',
    isLoading: false,
    user: {} as IUser,
    roles: [] as IRole[],
    registerError: {} as IRegisterFormError,
    updateError: {} as IUpdateUserFormError,
}

export default function userReducer(state = initialState, action: UserAction): UserState {
    switch (action.type) {
        case UserActionEnum.SET_USER:
            return { ...state, user: action.payload }
        case UserActionEnum.SET_USER_ROLE:
            return { ...state, roles: action.payload }
        case UserActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case UserActionEnum.SET_REGISTER_ERROR:
            return { ...state, registerError: action.payload }
        case UserActionEnum.SET_UPDATE_ERROR:
            return { ...state, updateError: action.payload }
        case UserActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}