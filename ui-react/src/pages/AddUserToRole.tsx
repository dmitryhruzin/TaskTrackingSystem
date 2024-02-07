import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import AddRoleForm from "../components/AddRoleForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RolesActionCreators } from "../store/reduce/roles/action-creators"
import { UserActionCreators } from "../store/reduce/user/action-creators"
import { contains } from "../utils/contains"
import LoadingPage from "./LoadingPage"


const AddUserToRole: FC = () => {
    const param = useParams()
    const { isLoading: isLoadingUserRoles, roles: userRoles } = useTypedSelector(t => t.user)
    const { isLoading: isLoadingRoles, roles } = useTypedSelector(t => t.roles)
    const { loadRoles } = useActions(RolesActionCreators)
    const { loadRoles: loadUserRoles } = useActions(UserActionCreators)
    useEffect(() => {
        loadRoles()
        loadUserRoles(Number(param.id))
    }, [])
    return (
        (!isLoadingRoles && !isLoadingUserRoles)
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <AddRoleForm roles={roles.filter(t => !contains(userRoles, t.id))} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default AddUserToRole