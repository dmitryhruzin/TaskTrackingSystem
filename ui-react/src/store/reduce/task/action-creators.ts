import { AxiosError } from "axios"
import { AppDispatch } from "../.."
import TaskService from "../../../api/TaskService"
import UserService from "../../../api/UserService"
import { IStatus } from "../../../models/IStatus"
import { ITask } from "../../../models/ITask"
import { ITaskFormError } from "../../../models/ITaskFormError"
import { IUser } from "../../../models/IUser"
import { SetErrorAction, SetIsLoadingAction, SetTaskAction, SetTaskErrorAction, SetTaskUsersAction, TaskActionEnum } from "./types"


export const TaskActionCreators = {
    setTask: (task: ITask): SetTaskAction => ({ type: TaskActionEnum.SET_TASK, payload: task }),
    setError: (payload: string): SetErrorAction => ({ type: TaskActionEnum.SET_ERROR, payload }),
    setUsers: (payload: IUser[]): SetTaskUsersAction => ({ type: TaskActionEnum.SET_TASK_USERS, payload }),
    setTaskError: (payload: ITaskFormError): SetTaskErrorAction => ({ type: TaskActionEnum.SET_TASK_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: TaskActionEnum.SET_IS_LOADING, payload }),
    loadTask: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(TaskActionCreators.setTask({} as ITask))
            dispatch(TaskActionCreators.setError(""))
            dispatch(TaskActionCreators.setIsLoading(true))
            const response = await TaskService.getTask(id)
            const task = response.data
            if (task) {
                dispatch(TaskActionCreators.setTask(task))
            }
            else {
                dispatch(TaskActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(TaskActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(TaskActionCreators.setIsLoading(false))
        }
    },
    loadUsers: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(TaskActionCreators.setUsers([] as IUser[]))
            dispatch(TaskActionCreators.setError(""))
            dispatch(TaskActionCreators.setIsLoading(true))
            const response = await UserService.getUsersByTaskId(id)
            const users = response?.data
            if (users?.length !== 0) {
                dispatch(TaskActionCreators.setUsers(users!))
            }
            else {
                dispatch(TaskActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(TaskActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(TaskActionCreators.setIsLoading(false))
        }
    },
    addTask: (task: ITask) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(TaskActionCreators.setError(""))
        dispatch(TaskActionCreators.setTaskError({} as ITaskFormError))
        dispatch(TaskActionCreators.setIsLoading(true))
        try {
            await TaskService.addTask(task)
        }
        catch (e) {
            dispatch(TaskActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            const description = ae?.Description
            const startDate = ae?.StartDate
            const expiryDate = ae?.ExpiryDate
            dispatch(TaskActionCreators.setTaskError({ name, description, startDate, expiryDate } as ITaskFormError))
            result = false
        }
        finally {
            dispatch(TaskActionCreators.setIsLoading(false))
            return result
        }
    },
    updateTask: (task: ITask) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(TaskActionCreators.setError(""))
        dispatch(TaskActionCreators.setTaskError({} as ITaskFormError))
        dispatch(TaskActionCreators.setIsLoading(true))
        try {
            await TaskService.updateTask(task)
        }
        catch (e) {
            dispatch(TaskActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            const description = ae?.Description
            const startDate = ae?.StartDate
            const expiryDate = ae?.ExpiryDate
            dispatch(TaskActionCreators.setTaskError({ name, description, startDate, expiryDate } as ITaskFormError))
            result = false
        }
        finally {
            dispatch(TaskActionCreators.setIsLoading(false))
            return result
        }
    },
    updateTaskStatus: (id: number, status: IStatus) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(TaskActionCreators.setError(""))
        dispatch(TaskActionCreators.setIsLoading(true))
        try {
            await TaskService.updateTaskStatus(id, status)
        }
        catch (e) {
            dispatch(TaskActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(TaskActionCreators.setIsLoading(false))
            return result
        }
    },
    deleteTask: (id: number) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(TaskActionCreators.setError(""))
        dispatch(TaskActionCreators.setIsLoading(true))
        try {
            await TaskService.deleteTask(id)
        }
        catch (e) {
            dispatch(TaskActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(TaskActionCreators.setIsLoading(false))
            return result
        }
    }
}