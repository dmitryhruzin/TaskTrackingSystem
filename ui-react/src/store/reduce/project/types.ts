import { IProject } from "../../../models/IPoject"
import { IProjectFormError } from "../../../models/IProjectFormError"


export interface ProjectState {
    project: IProject
    isLoading: boolean
    error: string
    projectError: IProjectFormError
}

export enum ProjectActionEnum {
    SET_ERROR = 'SET_PROJECT_ERROR',
    SET_PROJECT_ERROR = 'SET_PROJECT_FORM_ERROR',
    SET_PROJECT = 'SET_PROJECT',
    SET_IS_LOADING = "SET_PROJECT_IS_LOADING"
}

export interface SetErrorAction {
    type: ProjectActionEnum.SET_ERROR
    payload: string
}
export interface SetProjectErrorAction {
    type: ProjectActionEnum.SET_PROJECT_ERROR
    payload: IProjectFormError
}
export interface SetProjectAction {
    type: ProjectActionEnum.SET_PROJECT
    payload: IProject
}
export interface SetIsLoadingAction {
    type: ProjectActionEnum.SET_IS_LOADING
    payload: boolean
}
export type ProjectAction =
    SetErrorAction |
    SetProjectAction |
    SetIsLoadingAction |
    SetProjectErrorAction