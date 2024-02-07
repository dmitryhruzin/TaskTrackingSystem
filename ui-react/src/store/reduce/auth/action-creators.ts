import { AxiosError } from "axios"
import { AppDispatch } from "../.."
import LoginService from "../../../api/LoginService"
import RoleService from "../../../api/RoleService"
import TokenService from "../../../api/TokenService"
import UserService from "../../../api/UserService"
import { IForgotFormError } from "../../../models/IForgotFormError"
import { IForgotPassword } from "../../../models/IForgotPassword"
import { ILoginFormError } from "../../../models/ILoginFormError"
import { IResetFormError } from "../../../models/IResetFormError"
import { IResetPassword } from "../../../models/IResetPassword"
import { IRole } from "../../../models/IRole"
import { IToken } from "../../../models/IToken"
import { IUser } from "../../../models/IUser"
import { AuthActionEnum, SetAuthAction, SetAuthIsLoadingAction, SetAuthRolesAction, SetAuthTokenAction, SetAuthUserAction, SetErrorAction, SetForgotErrorAction, SetIsLoadingAction, SetPushErrorAction, SetResetErrorAction } from "./types"


export const AuthActionCreators = {
    setUser: (user: IUser): SetAuthUserAction => ({ type: AuthActionEnum.SET_USER, payload: user }),
    setIsAuth: (auth: boolean): SetAuthAction => ({ type: AuthActionEnum.SET_AUTH, payload: auth }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: AuthActionEnum.SET_IS_LOADING, payload }),
    setIsAuthLoading: (payload: boolean): SetAuthIsLoadingAction => ({ type: AuthActionEnum.SET_AUTH_IS_LOADING, payload }),
    setError: (payload: ILoginFormError): SetErrorAction => ({ type: AuthActionEnum.SET_ERROR, payload }),
    setForgotError: (payload: IForgotFormError): SetForgotErrorAction => ({ type: AuthActionEnum.SET_FORGOT_ERROR, payload }),
    setResetError: (payload: IResetFormError): SetResetErrorAction => ({ type: AuthActionEnum.SET_RESET_ERROR, payload }),
    setPushError: (payload: string): SetPushErrorAction => ({ type: AuthActionEnum.SET_PUSH_ERROR, payload }),
    setToken: (payload: IToken): SetAuthTokenAction => ({ type: AuthActionEnum.SET_TOKEN, payload }),
    setRoles: (payload: IRole[]): SetAuthRolesAction => ({ type: AuthActionEnum.SET_ROLES, payload }),
    login: (login: string, password: string) => async (dispatch: AppDispatch) => {
        try {
            dispatch(AuthActionCreators.setPushError(""))
            dispatch(AuthActionCreators.setError({} as ILoginFormError))
            dispatch(AuthActionCreators.setIsLoading(true))
            const response = await LoginService.login({ login: login, password: password })
            const token = response.data
            if (token) {
                localStorage.setItem('auth', 'true')
                localStorage.setItem('login', login)
                localStorage.setItem('accessToken', token.accessToken)
                localStorage.setItem('refreshToken', token.refreshToken)
                const user = (await UserService.getUserByEmail(login)).data
                if (user) {
                    localStorage.setItem('id', user.id.toString())
                    dispatch(AuthActionCreators.setUser(user))
                    const roles = (await RoleService.getRolesByUserId(user.id)).data
                    dispatch(AuthActionCreators.setRoles(roles))
                }
                dispatch(AuthActionCreators.setIsAuth(true))
                dispatch(AuthActionCreators.setToken(token))
            }
            else {
                dispatch(AuthActionCreators.setPushError("Not Found."))
                dispatch(AuthActionCreators.setError({ error: "Not Found.", password: [], email: [] } as ILoginFormError))
            }
        }
        catch (e) {
            dispatch(AuthActionCreators.setPushError((e as Error).message))
            const ae = (e as AxiosError).response!.data
            if (ae) {
                const password = (ae as any).errors?.Password
                const email = (ae as any).errors?.Login
                dispatch(AuthActionCreators.setError({ error: (e as Error).message, password, email } as ILoginFormError))
            }
        }
        finally {
            dispatch(AuthActionCreators.setIsLoading(false))
        }
    },
    logout: () => async (dispatch: AppDispatch) => {
        try {
            dispatch(AuthActionCreators.setPushError(""))
            dispatch(AuthActionCreators.setError({} as ILoginFormError))
            dispatch(AuthActionCreators.setIsLoading(true))
            const refreshToken = localStorage.getItem('refreshToken')
            const accessToken = localStorage.getItem('accessToken')
            await TokenService.revokeToken({ accessToken, refreshToken } as IToken)
        }
        catch (e) {
            dispatch(AuthActionCreators.setPushError((e as Error).message))
            dispatch(AuthActionCreators.setError({ error: (e as Error).message, password: [], email: [] } as ILoginFormError))
        }
        finally {
            localStorage.removeItem('auth')
            localStorage.removeItem('login')
            localStorage.removeItem('accessToken')
            localStorage.removeItem('refreshToken')
            localStorage.removeItem('id')
            dispatch(AuthActionCreators.setUser({} as IUser))
            dispatch(AuthActionCreators.setRoles([] as IRole[]))
            dispatch(AuthActionCreators.setIsAuth(false))
            dispatch(AuthActionCreators.setToken({} as IToken))
            dispatch(AuthActionCreators.setIsLoading(false))
        }
    },
    auth: () => async (dispatch: AppDispatch) => {
        try {
            dispatch(AuthActionCreators.setPushError(""))
            dispatch(AuthActionCreators.setIsAuthLoading(true))
            const auth = localStorage.getItem('auth')
            const id = localStorage.getItem('id')
            const accessToken = localStorage.getItem('accessToken')
            const refreshToken = localStorage.getItem('refreshToken')
            if (auth && id && accessToken && refreshToken) {
                const user = (await UserService.getUser(Number(id))).data
                dispatch(AuthActionCreators.setUser(user))
                const roles = (await RoleService.getRolesByUserId(user.id)).data
                dispatch(AuthActionCreators.setRoles(roles))
                dispatch(AuthActionCreators.setIsAuth(true))
                dispatch(AuthActionCreators.setToken({ accessToken, refreshToken } as IToken))
            }
            else {
                throw "Not found user"
            }
        }
        catch (e) {
            dispatch(AuthActionCreators.setPushError((e as Error).message))
            localStorage.removeItem('auth')
            localStorage.removeItem('login')
            localStorage.removeItem('accessToken')
            localStorage.removeItem('refreshToken')
            localStorage.removeItem('id')
            dispatch(AuthActionCreators.setUser({} as IUser))
            dispatch(AuthActionCreators.setRoles([] as IRole[]))
            dispatch(AuthActionCreators.setIsAuth(false))
            dispatch(AuthActionCreators.setToken({} as IToken))
        }
        finally {
            dispatch(AuthActionCreators.setIsAuthLoading(false))
        }
    },
    forgot: (model: IForgotPassword) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(AuthActionCreators.setPushError(""))
        dispatch(AuthActionCreators.setForgotError({} as IForgotFormError))
        dispatch(AuthActionCreators.setIsLoading(true))
        try {
            await LoginService.forgotPassword(model)
        }
        catch (e) {
            dispatch(AuthActionCreators.setPushError((e as Error).message))
            const ae = (e as AxiosError).response!.data
            if (ae) {
                const clientURI = (ae as any).errors?.ClientURI
                const email = (ae as any).errors?.Email
                dispatch(AuthActionCreators.setForgotError({ clientURI, email } as IForgotFormError))
            }
            result = false
        }
        finally {
            dispatch(AuthActionCreators.setIsLoading(false))
            return result
        }
    },
    reset: (model: IResetPassword) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(AuthActionCreators.setPushError(""))
        dispatch(AuthActionCreators.setResetError({} as IResetFormError))
        dispatch(AuthActionCreators.setIsLoading(true))
        try {
            await LoginService.resetPassword(model)
        }
        catch (e) {
            dispatch(AuthActionCreators.setPushError((e as Error).message))
            const ae = (e as AxiosError).response!.data
            if (ae) {
                const confirmPassword = (ae as any).errors?.ConfirmPassword
                const password = (ae as any).errors?.Password
                const token = (ae as any).errors?.Token
                const email = (ae as any).errors?.Email
                dispatch(AuthActionCreators.setResetError({ token, email, password, confirmPassword } as IResetFormError))
            }
            result = false
        }
        finally {
            dispatch(AuthActionCreators.setIsLoading(false))
            return result
        }
    },
}