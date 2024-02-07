import { AppDispatch } from "../.."
import RoleService from "../../../api/RoleService"
import { IRole } from "../../../models/IRole"
import { RolesActionEnum, SetErrorAction, SetIsLoadingAction, SetRolesAction } from "./types"


export const RolesActionCreators = {
    setRoles: (roles: IRole[]): SetRolesAction => ({ type: RolesActionEnum.SET_ROLES, payload: roles }),
    setError: (payload: string): SetErrorAction => ({ type: RolesActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: RolesActionEnum.SET_IS_LOADING, payload }),
    loadRoles: (id?: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(RolesActionCreators.setRoles([] as IRole[]))
            dispatch(RolesActionCreators.setError(""))
            dispatch(RolesActionCreators.setIsLoading(true))
            let response
            if (id) {
                response = await RoleService.getRolesByUserId(id)
            }
            else {
                response = await RoleService.getRoles()
            }
            const roles = response.data
            if (roles.length !== 0) {
                dispatch(RolesActionCreators.setRoles(roles))
            }
            else {
                dispatch(RolesActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(RolesActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(RolesActionCreators.setIsLoading(false))
        }
    }
}