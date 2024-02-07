import { IUserProject } from "../../../models/IUserProject"
import { UserProjectsAction, UserProjectsActionEnum, UserProjectsState } from "./types"


const initialState: UserProjectsState = {
    error: '',
    isLoading: false,
    userProjects: [] as IUserProject[]
}

export default function userProjectsReducer(state = initialState, action: UserProjectsAction): UserProjectsState {
    switch (action.type) {
        case UserProjectsActionEnum.SET_USERPROJECTS:
            return { ...state, userProjects: action.payload }
        case UserProjectsActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case UserProjectsActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}