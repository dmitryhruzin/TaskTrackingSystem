import { AppDispatch } from "../.."
import TaskService from "../../../api/TaskService"
import { ITask } from "../../../models/ITask"
import { SetErrorAction, SetIsLoadingAction, SetTasksAction, TasksActionEnum } from "./types"


export enum GetTasksEnum {
    BY_USER_ID = "UserTask",
    BY_MANAGER_ID = "ManagerTask",
    BY_PROJECT_ID = "Project",
    ALL_TASK = "Task"
}

export const TasksActionCreators = {
    setTasks: (tasks: ITask[]): SetTasksAction => ({ type: TasksActionEnum.SET_TASKS, payload: tasks }),
    setError: (payload: string): SetErrorAction => ({ type: TasksActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: TasksActionEnum.SET_IS_LOADING, payload }),
    loadTasks: (type: GetTasksEnum, id?: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(TasksActionCreators.setTasks([] as ITask[]))
            dispatch(TasksActionCreators.setError(""))
            dispatch(TasksActionCreators.setIsLoading(true))
            let response
            switch (type) {
                case GetTasksEnum.ALL_TASK:
                    response = await TaskService.getTasks()
                    break
                case GetTasksEnum.BY_PROJECT_ID:
                    if (id) {
                        response = await TaskService.getTasksByProjectId(id)
                    }
                    break
                case GetTasksEnum.BY_USER_ID:
                    if (id) {
                        response = await TaskService.getTasksByUserId(id)
                    }
                    break
                case GetTasksEnum.BY_MANAGER_ID:
                    if (id) {
                        response = await TaskService.getTasksByManagerId(id)
                    }
                    break
            }
            const tasks = response?.data
            if (tasks?.length !== 0) {
                dispatch(TasksActionCreators.setTasks(tasks!))
            }
            else {
                dispatch(TasksActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(TasksActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(TasksActionCreators.setIsLoading(false))
        }
    }
}