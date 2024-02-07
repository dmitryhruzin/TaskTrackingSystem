import { AppDispatch } from "../.."
import UserService from "../../../api/UserService"
import { IUser } from "../../../models/IUser"
import { SetErrorAction, SetIsLoadingAction, SetUsersAction, UsersActionEnum } from "./types"

export enum GetUsersEnum {
    BY_TASK_ID = "Task",
    BY_PROJECT_ID = "Project",
    ALL_USER = "User"
}

export const UsersActionCreators = {
    setUsers: (users: IUser[]): SetUsersAction => ({ type: UsersActionEnum.SET_USERS, payload: users }),
    setError: (payload: string): SetErrorAction => ({ type: UsersActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: UsersActionEnum.SET_IS_LOADING, payload }),
    loadUsers: (type: GetUsersEnum, id?: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(UsersActionCreators.setUsers([] as IUser[]))
            dispatch(UsersActionCreators.setError(""))
            dispatch(UsersActionCreators.setIsLoading(true))
            let response
            switch (type) {
                case GetUsersEnum.ALL_USER:
                    response = await UserService.getUsers()
                    break
                case GetUsersEnum.BY_PROJECT_ID:
                    if (id) {
                        response = await UserService.getUsersByProjectId(id)
                    }
                    break
                case GetUsersEnum.BY_TASK_ID:
                    if (id) {
                        response = await UserService.getUsersByTaskId(id)
                    }
                    break
            }
            const users = response?.data
            if (users?.length !== 0) {
                dispatch(UsersActionCreators.setUsers(users!))
            }
            else {
                dispatch(UsersActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(UsersActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(UsersActionCreators.setIsLoading(false))
        }
    }
}