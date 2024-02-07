import { IUserProject } from "../../../models/IUserProject"


export interface UserProjectsState {
    userProjects: IUserProject[]
    isLoading: boolean
    error: string
}

export enum UserProjectsActionEnum {
    SET_ERROR = 'SET_USERPROJECTS_ERROR',
    SET_USERPROJECTS = 'SET_USERPROJECTS',
    SET_IS_LOADING = "SET_USERPROJECTS_IS_LOADING"
}

export interface SetErrorAction {
    type: UserProjectsActionEnum.SET_ERROR
    payload: string
}
export interface SetUserProjectsAction {
    type: UserProjectsActionEnum.SET_USERPROJECTS
    payload: IUserProject[]
}
export interface SetIsLoadingAction {
    type: UserProjectsActionEnum.SET_IS_LOADING
    payload: boolean
}
export type UserProjectsAction =
    SetErrorAction |
    SetUserProjectsAction |
    SetIsLoadingAction 