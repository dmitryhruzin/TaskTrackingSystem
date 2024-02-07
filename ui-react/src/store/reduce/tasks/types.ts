import { ITask } from "../../../models/ITask"


export interface TasksState {
    tasks: ITask[]
    isLoading: boolean
    error: string
}

export enum TasksActionEnum {
    SET_ERROR = 'SET_TASKS_ERROR',
    SET_TASKS = 'SET_TASKS',
    SET_IS_LOADING = "SET_TASKS_IS_LOADING"
}

export interface SetErrorAction {
    type: TasksActionEnum.SET_ERROR
    payload: string
}
export interface SetTasksAction {
    type: TasksActionEnum.SET_TASKS
    payload: ITask[]
}
export interface SetIsLoadingAction {
    type: TasksActionEnum.SET_IS_LOADING
    payload: boolean
}
export type TasksAction =
    SetErrorAction |
    SetTasksAction |
    SetIsLoadingAction 