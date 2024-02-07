import { ITask } from "../../../models/ITask"
import { ITaskFormError } from "../../../models/ITaskFormError"
import { IUser } from "../../../models/IUser"
import { TaskAction, TaskActionEnum, TaskState } from "./types"


const initialState: TaskState = {
    error: '',
    isLoading: false,
    task: {} as ITask,
    taskError: {} as ITaskFormError,
    users : [] as IUser[]
}

export default function taskReducer(state = initialState, action: TaskAction): TaskState {
    switch (action.type) {
        case TaskActionEnum.SET_TASK:
            return { ...state, task: action.payload }
        case TaskActionEnum.SET_TASK_USERS:
            return { ...state, users: action.payload }
        case TaskActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case TaskActionEnum.SET_TASK_ERROR:
            return { ...state, taskError: action.payload }
        case TaskActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}