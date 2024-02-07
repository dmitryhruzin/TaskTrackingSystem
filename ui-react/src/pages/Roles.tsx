import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import RoleList from "../components/RoleList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RolesActionCreators } from "../store/reduce/roles/action-creators"
import LoadingPage from "./LoadingPage"

const Roles: FC = () => {
    const { isLoading } = useTypedSelector(t => t.roles)
    const { loadRoles } = useActions(RolesActionCreators)
    useEffect(() => {
        loadRoles()
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 align-items-center">
                <RoleList />
            </Stack>
            :
            <LoadingPage />
    )
}

export default Roles