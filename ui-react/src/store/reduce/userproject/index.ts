import { IUserProject } from "../../../models/IUserProject"
import { UserProjectAction, UserProjectActionEnum, UserProjectState } from "./types"


const initialState: UserProjectState = {
    error: '',
    isLoading: false,
    userProject: {} as IUserProject
}

export default function userProjectReducer(state = initialState, action: UserProjectAction): UserProjectState {
    switch (action.type) {
        case UserProjectActionEnum.SET_USERPROJECT:
            return { ...state, userProject: action.payload }
        case UserProjectActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case UserProjectActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}