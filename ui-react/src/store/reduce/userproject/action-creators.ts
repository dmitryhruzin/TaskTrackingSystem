import { AppDispatch } from "../.."
import UserProjectService from "../../../api/UserProjectService"
import { IUserProject } from "../../../models/IUserProject"
import { SetErrorAction, SetIsLoadingAction, SetUserProjectAction, UserProjectActionEnum } from "./types"


export const UserProjectActionCreators = {
    setUserProject: (userProject: IUserProject): SetUserProjectAction => ({ type: UserProjectActionEnum.SET_USERPROJECT, payload: userProject }),
    setError: (payload: string): SetErrorAction => ({ type: UserProjectActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: UserProjectActionEnum.SET_IS_LOADING, payload }),
    loadUserProject: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(UserProjectActionCreators.setUserProject({} as IUserProject))
            dispatch(UserProjectActionCreators.setError(""))
            dispatch(UserProjectActionCreators.setIsLoading(true))
            const response = await UserProjectService.getUserProject(id)
            const userProject = response.data
            if (userProject) {
                dispatch(UserProjectActionCreators.setUserProject(userProject))
            }
            else {
                dispatch(UserProjectActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(UserProjectActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(UserProjectActionCreators.setIsLoading(false))
        }
    },
    addUserProject: (userProject: IUserProject) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserProjectActionCreators.setIsLoading(true))
        dispatch(UserProjectActionCreators.setError(""))
        try {
            await UserProjectService.addUserProject(userProject)
        }
        catch (e) {
            dispatch(UserProjectActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserProjectActionCreators.setIsLoading(false))
            return result
        }
    },
    addUserProjects: (userProjects: IUserProject[]) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserProjectActionCreators.setIsLoading(true))
        dispatch(UserProjectActionCreators.setError(""))
        try {
            await UserProjectService.addUserProjects(userProjects)
        }
        catch (e) {
            dispatch(UserProjectActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserProjectActionCreators.setIsLoading(false))
            return result
        }
    },
    updateUserProject: (userProject: IUserProject) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserProjectActionCreators.setIsLoading(true))
        dispatch(UserProjectActionCreators.setError(""))
        try {
            await UserProjectService.updateUserProject(userProject)
        }
        catch (e) {
            dispatch(UserProjectActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserProjectActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteUserProject: (id: number) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserProjectActionCreators.setIsLoading(true))
        dispatch(UserProjectActionCreators.setError(""))
        try {
            await UserProjectService.deleteUserProject(id)
        }
        catch (e) {
            dispatch(UserProjectActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserProjectActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteUserProjects: (ids: number[]) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(UserProjectActionCreators.setIsLoading(true))
        dispatch(UserProjectActionCreators.setError(""))
        try {
            await UserProjectService.deleteUserProjects(ids)
        }
        catch (e) {
            dispatch(UserProjectActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(UserProjectActionCreators.setIsLoading(false))
            return result
        }
    }
}