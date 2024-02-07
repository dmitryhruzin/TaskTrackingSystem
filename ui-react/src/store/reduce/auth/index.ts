import { IForgotFormError } from "../../../models/IForgotFormError"
import { ILoginFormError } from "../../../models/ILoginFormError"
import { IResetFormError } from "../../../models/IResetFormError"
import { IRole } from "../../../models/IRole"
import { IToken } from "../../../models/IToken"
import { IUser } from "../../../models/IUser"
import { AuthAction, AuthActionEnum, AuthState } from "./types"


const initialState: AuthState = {
    isAuth: false,
    error: {} as ILoginFormError,
    forgotError: {} as IForgotFormError,
    resetError: {} as IResetFormError,
    isLoading: false,
    isAuthLoading: true,
    user: {} as IUser,
    token: {} as IToken,
    roles: [] as IRole[],
    pushError: ""
}

export default function authReducer(state = initialState, action: AuthAction): AuthState {
    switch (action.type) {
        case AuthActionEnum.SET_AUTH:
            return { ...state, isAuth: action.payload }
        case AuthActionEnum.SET_USER:
            return { ...state, user: action.payload }
        case AuthActionEnum.SET_ROLES:
            return { ...state, roles: action.payload }
        case AuthActionEnum.SET_TOKEN:
            return { ...state, token: action.payload }
        case AuthActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case AuthActionEnum.SET_FORGOT_ERROR:
            return { ...state, forgotError: action.payload }
        case AuthActionEnum.SET_RESET_ERROR:
            return { ...state, resetError: action.payload }
        case AuthActionEnum.SET_PUSH_ERROR:
            return { ...state, pushError: action.payload }
        case AuthActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        case AuthActionEnum.SET_AUTH_IS_LOADING:
            return { ...state, isAuthLoading: action.payload }
        default:
            return state
    }
}