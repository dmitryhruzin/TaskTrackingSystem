import { AppDispatch } from "../.."
import ProjectStatusService from "../../../api/ProjectStatusService"
import TaskStatusService from "../../../api/TaskStatusService"
import { IStatus } from "../../../models/IStatus"
import { SetErrorAction, SetIsLoadingAction, SetStatusesAction, StatusesActionEnum } from "./types"


export enum GetStatusesEnum {
    BY_TASK = "Task",
    BY_PROJECT = "Project"
}

export const StatusesActionCreators = {
    setStatuses: (statuses: IStatus[]): SetStatusesAction => ({ type: StatusesActionEnum.SET_STATUSES, payload: statuses }),
    setError: (payload: string): SetErrorAction => ({ type: StatusesActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: StatusesActionEnum.SET_IS_LOADING, payload }),
    loadStatuses: (type: GetStatusesEnum) => async (dispatch: AppDispatch) => {
        try {
            dispatch(StatusesActionCreators.setStatuses([] as IStatus[]))
            dispatch(StatusesActionCreators.setError(""))
            dispatch(StatusesActionCreators.setIsLoading(true))
            let response
            switch (type) {
                case GetStatusesEnum.BY_PROJECT:
                    response = await ProjectStatusService.getStatuses()
                    break
                case GetStatusesEnum.BY_TASK:
                    response = await TaskStatusService.getStatuses()
                    break
            }
            const statuses = response?.data
            if (statuses?.length !== 0) {
                dispatch(StatusesActionCreators.setStatuses(statuses!))
            }
            else {
                dispatch(StatusesActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(StatusesActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(StatusesActionCreators.setIsLoading(false))
        }
    }
}