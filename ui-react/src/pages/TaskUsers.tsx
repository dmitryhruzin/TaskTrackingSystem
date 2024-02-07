import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import UserList from "../components/UserList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { GetUsersEnum, UsersActionCreators } from "../store/reduce/users/action-creators"
import LoadingPage from "./LoadingPage"


const TaskUsers: FC = () => {
    const param = useParams()
    const { isLoading } = useTypedSelector(t => t.users)
    const { loadUsers } = useActions(UsersActionCreators)
    useEffect(() => {
        loadUsers(GetUsersEnum.BY_TASK_ID, Number(param.id))
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 align-items-center">
                <UserList />
            </Stack>
            :
            <LoadingPage />
    )
}

export default TaskUsers