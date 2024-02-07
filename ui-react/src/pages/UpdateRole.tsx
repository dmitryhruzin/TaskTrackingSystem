import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import RoleForm from "../components/RoleForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RoleActionCreators } from "../store/reduce/role/action-creators"
import LoadingPage from "./LoadingPage"

const UpdateRole: FC = () => {
    const param = useParams()
    const { isLoading, role } = useTypedSelector(t => t.role)
    const { loadRole } = useActions(RoleActionCreators)
    useEffect(() => {
        loadRole(Number(param.id))
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <RoleForm role={role} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdateRole