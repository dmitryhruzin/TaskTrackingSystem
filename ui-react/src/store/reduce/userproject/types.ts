import { IUserProject } from "../../../models/IUserProject"


export interface UserProjectState {
    userProject: IUserProject
    isLoading: boolean
    error: string
}

export enum UserProjectActionEnum {
    SET_ERROR = 'SET_USERPROJECT_ERROR',
    SET_USERPROJECT = 'SET_USERPROJECT',
    SET_IS_LOADING = "SET_USERPROJECT_IS_LOADING"
}

export interface SetErrorAction {
    type: UserProjectActionEnum.SET_ERROR
    payload: string
}
export interface SetUserProjectAction {
    type: UserProjectActionEnum.SET_USERPROJECT
    payload: IUserProject
}
export interface SetIsLoadingAction {
    type: UserProjectActionEnum.SET_IS_LOADING
    payload: boolean
}
export type UserProjectAction =
    SetErrorAction |
    SetUserProjectAction |
    SetIsLoadingAction 