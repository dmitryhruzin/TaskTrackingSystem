import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import UserProjectForm from "../components/UserProjectForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { PositionsActionCreators } from "../store/reduce/positions/actions-creators"
import { UserProjectActionCreators } from "../store/reduce/userproject/action-creators"
import { GetUsersEnum, UsersActionCreators } from "../store/reduce/users/action-creators"
import LoadingPage from "./LoadingPage"

const UpdateUserProject: FC = () => {
    const param = useParams()
    const { isLoading, userProject } = useTypedSelector(t => t.userproject)
    const { isLoading: isLoadingUsers, users } = useTypedSelector(t => t.users)
    const { isLoading: isLoadingPositions } = useTypedSelector(t => t.positions)
    const { loadUserProject } = useActions(UserProjectActionCreators)
    const { loadUsers } = useActions(UsersActionCreators)
    const { loadPositions } = useActions(PositionsActionCreators)
    useEffect(() => {
        loadUserProject(Number(param.id))
        loadUsers(GetUsersEnum.ALL_USER)
        loadPositions()
    }, [])
    return (
        (!isLoading && !isLoadingUsers && !isLoadingPositions)
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <UserProjectForm userProject={userProject} users={users.filter(t => t.id != userProject.userId)} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdateUserProject