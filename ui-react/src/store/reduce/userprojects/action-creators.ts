import { AppDispatch } from "../.."
import UserProjectService from "../../../api/UserProjectService"
import { IUserProject } from "../../../models/IUserProject"
import { SetErrorAction, SetIsLoadingAction, SetUserProjectsAction, UserProjectsActionEnum } from "./types"


export const UserProjectsActionCreators = {
    setUserProjects: (userProjects: IUserProject[]): SetUserProjectsAction => ({ type: UserProjectsActionEnum.SET_USERPROJECTS, payload: userProjects }),
    setError: (payload: string): SetErrorAction => ({ type: UserProjectsActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: UserProjectsActionEnum.SET_IS_LOADING, payload }),
    loadUserProjects: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(UserProjectsActionCreators.setUserProjects([] as IUserProject[]))
            dispatch(UserProjectsActionCreators.setError(""))
            dispatch(UserProjectsActionCreators.setIsLoading(true))
            const response = await UserProjectService.getUserProjects()
            const userProjects = response?.data.filter(t => t.taskId === id)
            if (userProjects?.length !== 0) {
                dispatch(UserProjectsActionCreators.setUserProjects(userProjects!))
            }
            else {
                dispatch(UserProjectsActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(UserProjectsActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(UserProjectsActionCreators.setIsLoading(false))
        }
    }
}