import { IProject } from "../../../models/IPoject"
import { ProjectsAction, ProjectsActionEnum, ProjectsState } from "./types"


const initialState: ProjectsState = {
    error: '',
    isLoading: false,
    projects: [] as IProject[]
}

export default function projectsReducer(state = initialState, action: ProjectsAction): ProjectsState {
    switch (action.type) {
        case ProjectsActionEnum.SET_PROJECTS:
            return { ...state, projects: action.payload }
        case ProjectsActionEnum.SET_ERROR:
            return { ...state, error: action.payload }
        case ProjectsActionEnum.SET_IS_LOADING:
            return { ...state, isLoading: action.payload }
        default:
            return state
    }
}