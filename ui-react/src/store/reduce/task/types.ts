import { ITask } from "../../../models/ITask"
import { ITaskFormError } from "../../../models/ITaskFormError"
import { IUser } from "../../../models/IUser"


export interface TaskState {
    task: ITask
    isLoading: boolean
    error: string
    taskError: ITaskFormError
    users: IUser[]
}

export enum TaskActionEnum {
    SET_ERROR = 'SET_TASK_ERROR',
    SET_TASK_ERROR = 'SET_TASK_FORM_ERROR',
    SET_TASK = 'SET_TASK',
    SET_IS_LOADING = "SET_TASK_IS_LOADING",
    SET_TASK_USERS = 'SET_TASK_USERS'
}

export interface SetTaskErrorAction {
    type: TaskActionEnum.SET_TASK_ERROR
    payload: ITaskFormError
}
export interface SetErrorAction {
    type: TaskActionEnum.SET_ERROR
    payload: string
}
export interface SetTaskAction {
    type: TaskActionEnum.SET_TASK
    payload: ITask
}
export interface SetTaskUsersAction {
    type: TaskActionEnum.SET_TASK_USERS
    payload: IUser[]
}
export interface SetIsLoadingAction {
    type: TaskActionEnum.SET_IS_LOADING
    payload: boolean
}
export type TaskAction =
    SetErrorAction |
    SetTaskAction |
    SetIsLoadingAction |
    SetTaskErrorAction |
    SetTaskUsersAction