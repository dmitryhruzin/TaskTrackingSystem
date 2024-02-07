import { AxiosError } from "axios"
import { AppDispatch } from "../.."
import ProjectService from "../../../api/ProjectService"
import { IProject } from "../../../models/IPoject"
import { IProjectFormError } from "../../../models/IProjectFormError"
import { ProjectActionEnum, SetErrorAction, SetIsLoadingAction, SetProjectAction, SetProjectErrorAction } from "./types"


export const ProjectActionCreators = {
    setProject: (project: IProject): SetProjectAction => ({ type: ProjectActionEnum.SET_PROJECT, payload: project }),
    setError: (payload: string): SetErrorAction => ({ type: ProjectActionEnum.SET_ERROR, payload }),
    setProjectError: (payload: IProjectFormError): SetProjectErrorAction => ({ type: ProjectActionEnum.SET_PROJECT_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: ProjectActionEnum.SET_IS_LOADING, payload }),
    loadProject: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(ProjectActionCreators.setProject({} as IProject))
            dispatch(ProjectActionCreators.setError(""))
            dispatch(ProjectActionCreators.setIsLoading(true))
            const response = await ProjectService.getProject(id)
            const project = response.data
            if (project) {
                dispatch(ProjectActionCreators.setProject(project))
            }
            else {
                dispatch(ProjectActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(ProjectActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(ProjectActionCreators.setIsLoading(false))
        }
    },
    addProject: (project: IProject) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(ProjectActionCreators.setError(""))
        dispatch(ProjectActionCreators.setProjectError({} as IProjectFormError))
        dispatch(ProjectActionCreators.setIsLoading(true))
        try {
            await ProjectService.addProject(project)
        }
        catch (e) {
            dispatch(ProjectActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            const description = ae?.Description
            const startDate = ae?.StartDate
            const expiryDate = ae?.ExpiryDate
            dispatch(ProjectActionCreators.setProjectError({ name, description, startDate, expiryDate } as IProjectFormError))
            result = false
        }
        finally {
            dispatch(ProjectActionCreators.setIsLoading(false))
            return result
        }
    },
    updateProject: (project: IProject) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(ProjectActionCreators.setError(""))
        dispatch(ProjectActionCreators.setProjectError({} as IProjectFormError))
        dispatch(ProjectActionCreators.setIsLoading(true))
        try {
            await ProjectService.updateProject(project)
        }
        catch (e) {
            dispatch(ProjectActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            const description = ae?.Description
            const startDate = ae?.StartDate
            const expiryDate = ae?.ExpiryDate
            dispatch(ProjectActionCreators.setProjectError({ name, description, startDate, expiryDate } as IProjectFormError))
            result = false
        }
        finally {
            dispatch(ProjectActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteProject: (id: number) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(ProjectActionCreators.setError(""))
        dispatch(ProjectActionCreators.setIsLoading(true))
        try {
            await ProjectService.deleteProject(id)
        }
        catch (e) {
            dispatch(ProjectActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(ProjectActionCreators.setIsLoading(false))
            return result
        }
    }
}