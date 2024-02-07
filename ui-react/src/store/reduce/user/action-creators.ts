import { AxiosError } from "axios"
import { AppDispatch } from "../.."
import RoleService from "../../../api/RoleService"
import UserService from "../../../api/UserService"
import { IRegisterFormError } from "../../../models/IRegisterFormError"
import { IRegisterUser } from "../../../models/IRegisterUser"
import { IRole } from "../../../models/IRole"
import { IUpdateUser } from "../../../models/IUpdateUser"
import { IUpdateUserFormError } from "../../../models/IUpdateUserFormError"
import { IUser } from "../../../models/IUser"
import { SetErrorAction, SetIsLoadingAction, SetRegisterErrorAction, SetUpdateErrorAction, SetUserAction, SetUserRoleAction, UserActionEnum } from "./types"


export const UserActionCreators = {
    setUser: (user: IUser): SetUserAction => ({ type: UserActionEnum.SET_USER, payload: user }),
    setRoles: (payload: IRole[]): SetUserRoleAction => ({ type: UserActionEnum.SET_USER_ROLE, payload }),
    setError: (payload: string): SetErrorAction => ({ type: UserActionEnum.SET_ERROR, payload }),
    setRegisterError: (payload: IRegisterFormError): SetRegisterErrorAction => ({ type: UserActionEnum.SET_REGISTER_ERROR, payload }),
    setUpdateError: (payload: IUpdateUserFormError): SetUpdateErrorAction => ({ type: UserActionEnum.SET_UPDATE_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: UserActionEnum.SET_IS_LOADING, payload }),
    loadUser: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(UserActionCreators.setUser({} as IUser))
            dispatch(UserActionCreators.setError(""))
            dispatch(UserActionCreators.setIsLoading(true))
            const response = await UserService.getUser(id)
            const user = response.data
            if (user) {
                dispatch(UserActionCreators.setUser(user))
            }
            else {
                dispatch(UserActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
        }
    },
    loadRoles: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(UserActionCreators.setRoles([] as IRole[]))
            dispatch(UserActionCreators.setError(""))
            dispatch(UserActionCreators.setIsLoading(true))
            const response = await RoleService.getRolesByUserId(id)
            const roles = response.data
            if (roles.length !== 0) {
                dispatch(UserActionCreators.setRoles(roles))
            }
            else {
                dispatch(UserActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
        }
    },
    addUser: (user: IRegisterUser) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setRegisterError({} as IRegisterFormError))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            await UserService.addUser(user)
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const firstName = ae.FirstName
            const lastName = ae.LastName
            const userName = ae.userName
            const password = ae.Password
            const confirmPassword = ae.ConfirmPassword
            const email = ae.Email
            dispatch(UserActionCreators.setRegisterError({ firstName, lastName, userName, password, confirmPassword, email } as IRegisterFormError))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    },
    addUserToRole: (id: number, role: IRole) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            console.log(role)
            await RoleService.addToRole(id, role)
        }
        catch (e) {
            console.log(e)
            dispatch(UserActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    },
    addUserToRoles: (id: number, roles: IRole[]) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            await RoleService.addToRoles(id, roles)
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    },
    updateUser: (user: IUpdateUser) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setUpdateError({} as IUpdateUserFormError))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            await UserService.updateUser(user)
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const firstName = ae.FirstName
            const lastName = ae.LastName
            const userName = ae.userName
            const email = ae.Email
            dispatch(UserActionCreators.setUpdateError({ firstName, lastName, userName, email } as IUpdateUserFormError))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteUser: (id: number) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            await UserService.deleteUser(id)
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteUserToRole: (id: number, name: string) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            await RoleService.deleteToRole(id, name)
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteUserToRoles: (id: number, names: string[]) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserActionCreators.setError(""))
        dispatch(UserActionCreators.setIsLoading(true))
        try {
            await RoleService.deleteToRoles(id, names)
        }
        catch (e) {
            dispatch(UserActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserActionCreators.setIsLoading(false))
            return result
        }
    }
}