import { FC } from "react"
import Toast from "react-bootstrap/esm/Toast"
import ToastContainer from "react-bootstrap/esm/ToastContainer"
import { ActionCreator } from "redux"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { AuthActionCreators } from "../store/reduce/auth/action-creators"
import { PositionActionCreators } from "../store/reduce/position/action-creators"
import { PositionsActionCreators } from "../store/reduce/positions/actions-creators"
import { ProjectActionCreators } from "../store/reduce/project/action-creators"
import { ProjectsActionCreators } from "../store/reduce/projects/action-creators"
import { RoleActionCreators } from "../store/reduce/role/action-creators"
import { RolesActionCreators } from "../store/reduce/roles/action-creators"
import { StatusActionCreators } from "../store/reduce/status/action-creators"
import { StatusesActionCreators } from "../store/reduce/statuses/action-creators"
import { TaskActionCreators } from "../store/reduce/task/action-creators"
import { TasksActionCreators } from "../store/reduce/tasks/action-creators"
import { UserActionCreators } from "../store/reduce/user/action-creators"
import { UserProjectActionCreators } from "../store/reduce/userproject/action-creators"
import { UserProjectsActionCreators } from "../store/reduce/userprojects/action-creators"
import { UsersActionCreators } from "../store/reduce/users/action-creators"

interface IError {
    key: number
    message: string
    setError: ActionCreator<any>
}

const ErrorContainer: FC = () => {
    const { pushError } = useTypedSelector(t => t.auth)
    const { error: positionError } = useTypedSelector(t => t.position)
    const { error: positionsError } = useTypedSelector(t => t.positions)
    const { error: projectError } = useTypedSelector(t => t.project)
    const { error: projectsError } = useTypedSelector(t => t.projects)
    const { error: roleError } = useTypedSelector(t => t.role)
    const { error: rolesError } = useTypedSelector(t => t.roles)
    const { error: statusError } = useTypedSelector(t => t.status)
    const { error: statusesError } = useTypedSelector(t => t.statuses)
    const { error: taskError } = useTypedSelector(t => t.task)
    const { error: tasksError } = useTypedSelector(t => t.tasks)
    const { error: userError } = useTypedSelector(t => t.user)
    const { error: usersError } = useTypedSelector(t => t.users)
    const { error: userProjectError } = useTypedSelector(t => t.userproject)
    const { error: userProjectsError } = useTypedSelector(t => t.userprojects)
    const { setPushError } = useActions(AuthActionCreators)
    const { setError: setPositionError } = useActions(PositionActionCreators)
    const { setError: setPositionsError } = useActions(PositionsActionCreators)
    const { setError: setProjectError } = useActions(ProjectActionCreators)
    const { setError: setProjectsError } = useActions(ProjectsActionCreators)
    const { setError: setRoleError } = useActions(RoleActionCreators)
    const { setError: setRolesError } = useActions(RolesActionCreators)
    const { setError: setStatusError } = useActions(StatusActionCreators)
    const { setError: setStatusesError } = useActions(StatusesActionCreators)
    const { setError: setTaskError } = useActions(TaskActionCreators)
    const { setError: setTasksError } = useActions(TasksActionCreators)
    const { setError: setUserError } = useActions(UserActionCreators)
    const { setError: setUsersError } = useActions(UsersActionCreators)
    const { setError: setUserProjectError } = useActions(UserProjectActionCreators)
    const { setError: setUserProjectsError } = useActions(UserProjectsActionCreators)
    const errors: IError[] = [
        { key: 1, message: pushError, setError: setPushError },
        { key: 2, message: positionError, setError: setPositionError },
        { key: 3, message: positionsError, setError: setPositionsError },
        { key: 4, message: projectError, setError: setProjectError },
        { key: 5, message: projectsError, setError: setProjectsError },
        { key: 6, message: roleError, setError: setRoleError },
        { key: 7, message: rolesError, setError: setRolesError },
        { key: 8, message: statusError, setError: setStatusError },
        { key: 9, message: statusesError, setError: setStatusesError },
        { key: 10, message: taskError, setError: setTaskError },
        { key: 11, message: tasksError, setError: setTasksError },
        { key: 12, message: userError, setError: setUserError },
        { key: 13, message: usersError, setError: setUsersError },
        { key: 14, message: userProjectError, setError: setUserProjectError },
        { key: 15, message: userProjectsError, setError: setUserProjectsError },
    ]
    return (
        <ToastContainer className="p-3" position="top-center">
            <>
                {
                    errors.map(error =>
                        <Toast key={error.key} show={!!error.message} onClose={() => error.setError("")} delay={3000} autohide>
                            <Toast.Header>
                                <strong className="me-auto">Error</strong>
                            </Toast.Header>
                            <Toast.Body>{error.message}</Toast.Body>
                        </Toast>
                    )
                }
            </>
        </ToastContainer>
    )
}

export default ErrorContainer