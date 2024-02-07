import { IProject } from "../../../models/IPoject"


export interface ProjectsState {
    projects: IProject[]
    isLoading: boolean
    error: string
}

export enum ProjectsActionEnum {
    SET_ERROR = 'SET_PROJECTS_ERROR',
    SET_PROJECTS = 'SET_PROJECTS',
    SET_IS_LOADING = "SET_PROJECTS_IS_LOADING"
}

export interface SetErrorAction {
    type: ProjectsActionEnum.SET_ERROR
    payload: string
}
export interface SetProjectsAction {
    type: ProjectsActionEnum.SET_PROJECTS
    payload: IProject[]
}
export interface SetIsLoadingAction {
    type: ProjectsActionEnum.SET_IS_LOADING
    payload: boolean
}
export type ProjectsAction =
    SetErrorAction |
    SetProjectsAction |
    SetIsLoadingAction 