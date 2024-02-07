import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import UserProjectForm from "../components/UserProjectForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { PositionsActionCreators } from "../store/reduce/positions/actions-creators"
import { TaskActionCreators } from "../store/reduce/task/action-creators"
import { GetUsersEnum, UsersActionCreators } from "../store/reduce/users/action-creators"
import { contains } from "../utils/contains"
import LoadingPage from "./LoadingPage"

const AddUserProject: FC = () => {
    const param = useParams()
    const { isLoading: isLoadingUsers, users } = useTypedSelector(t => t.users)
    const { isLoading: isLoadingPositions } = useTypedSelector(t => t.positions)
    const { isLoading: isLoadingTask, users: taskUsers } = useTypedSelector(t => t.task)
    const { loadUsers } = useActions(UsersActionCreators)
    const { loadUsers: loadTaskUsers } = useActions(TaskActionCreators)
    const { loadPositions } = useActions(PositionsActionCreators)
    useEffect(() => {
        loadUsers(GetUsersEnum.ALL_USER)
        loadPositions()
        loadTaskUsers(Number(param.id))
    }, [])
    return (
        (!isLoadingUsers && !isLoadingPositions && !isLoadingTask)
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <UserProjectForm users={users.filter(t => !contains(taskUsers, t.id))}/>
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default AddUserProject