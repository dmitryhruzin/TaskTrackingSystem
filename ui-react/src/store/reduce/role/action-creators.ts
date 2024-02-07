import { AxiosError } from "axios"
import { AppDispatch } from "../.."
import RoleService from "../../../api/RoleService"
import { IRole } from "../../../models/IRole"
import { IRoleFormError } from "../../../models/IRoleFormError"
import { RoleActionEnum, SetErrorAction, SetIsLoadingAction, SetRoleAction, SetRoleErrorAction } from "./types"


export const RoleActionCreators = {
    setRole: (role: IRole): SetRoleAction => ({ type: RoleActionEnum.SET_ROLE, payload: role }),
    setError: (payload: string): SetErrorAction => ({ type: RoleActionEnum.SET_ERROR, payload }),
    setRoleError: (payload: IRoleFormError): SetRoleErrorAction => ({ type: RoleActionEnum.SET_ROLE_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: RoleActionEnum.SET_IS_LOADING, payload }),
    loadRole: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(RoleActionCreators.setRole({} as IRole))
            dispatch(RoleActionCreators.setError(""))
            dispatch(RoleActionCreators.setIsLoading(true))
            const response = await RoleService.getRole(id)
            const role = response.data
            if (role) {
                dispatch(RoleActionCreators.setRole(role))
            }
            else {
                dispatch(RoleActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(RoleActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(RoleActionCreators.setIsLoading(false))
        }
    },
    addRole: (role: IRole) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(RoleActionCreators.setRoleError({} as IRoleFormError))
        dispatch(RoleActionCreators.setError(""))
        dispatch(RoleActionCreators.setIsLoading(true))
        try {
            await RoleService.addRole(role)
        }
        catch (e) {
            dispatch(RoleActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            dispatch(RoleActionCreators.setRoleError({ name } as IRoleFormError))
            result = false
        }
        finally {
            dispatch(RoleActionCreators.setIsLoading(false))
            return result
        }
    },
    updateRole: (role: IRole) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(RoleActionCreators.setRoleError({} as IRoleFormError))
        dispatch(RoleActionCreators.setError(""))
        dispatch(RoleActionCreators.setIsLoading(true))
        try {
            await RoleService.updateRole(role)
        }
        catch (e) {
            dispatch(RoleActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            dispatch(RoleActionCreators.setRoleError({ name } as IRoleFormError))
            result = false
        }
        finally {
            dispatch(RoleActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteRole: (id: number) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(RoleActionCreators.setError(""))
        dispatch(RoleActionCreators.setIsLoading(true))
        try {
            dispatch(RoleActionCreators.setIsLoading(true))
            await RoleService.deleteRole(id)
        }
        catch (e) {
            dispatch(RoleActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(RoleActionCreators.setIsLoading(false))
            return result
        }
    }
}