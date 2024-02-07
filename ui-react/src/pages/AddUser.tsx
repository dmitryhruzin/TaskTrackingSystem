import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import AddUserForm from "../components/AddUserForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RolesActionCreators } from "../store/reduce/roles/action-creators"
import LoadingPage from "./LoadingPage"


const AddUser: FC = () => {
    const { isLoading } = useTypedSelector(t => t.roles)
    const { loadRoles } = useActions(RolesActionCreators)
    useEffect(() => {
        loadRoles()
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <AddUserForm />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default AddUser