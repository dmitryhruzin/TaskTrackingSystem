import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import DeleteRoleList from "../components/DeleteRoleList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { UserActionCreators } from "../store/reduce/user/action-creators"
import LoadingPage from "./LoadingPage"


const DeleteUserToRole: FC = () => {
    const param = useParams()
    const { isLoading } = useTypedSelector(t => t.user)
    const { loadRoles: loadUserRoles } = useActions(UserActionCreators)
    useEffect(() => {
        loadUserRoles(Number(param.id))
    }, [])
    return (
        (!isLoading)
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <DeleteRoleList />
            </Stack>
            :
            <LoadingPage />
    )
}

export default DeleteUserToRole