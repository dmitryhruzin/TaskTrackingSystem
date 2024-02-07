import { IProject } from "../../../models/IPoject"
import { IProjectFormError } from "../../../models/IProjectFormError"
import { ProjectAction, ProjectActionEnum, ProjectState } from "./types"


const initialState: ProjectState = {
    error: '',
    isLoading: false,
    project: {} as IProject,
    projectError: {} as IProjectFormError
}

export default function projectReducer(state = initialState, action: ProjectAction): ProjectState {
    switch (action.type) {
        case ProjectActionEnum.SET_PROJECT:
            return { ...state, project: action.payload }
        case ProjectActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case ProjectActionEnum.SET_PROJECT_ERROR:
            return { ...state, projectError: action.payload }
        case ProjectActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}