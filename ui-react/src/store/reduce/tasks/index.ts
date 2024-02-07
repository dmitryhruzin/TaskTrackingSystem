import { ITask } from "../../../models/ITask"
import { TasksAction, TasksActionEnum, TasksState } from "./types"


const initialState: TasksState = {
    error: '',
    isLoading: false,
    tasks: [] as ITask[]
}

export default function tasksReducer(state = initialState, action: TasksAction): TasksState {
    switch (action.type) {
        case TasksActionEnum.SET_TASKS:
            return { ...state, tasks: action.payload }
        case TasksActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case TasksActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}