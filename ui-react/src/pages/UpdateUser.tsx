import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import UpdateUserForm from "../components/UpdateUserForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { UserActionCreators } from "../store/reduce/user/action-creators"
import LoadingPage from "./LoadingPage"

const UpdateUser: FC = () => {
    const param = useParams()
    const { user, isLoading } = useTypedSelector(t => t.user)
    const { loadUser } = useActions(UserActionCreators)
    useEffect(() => {
        loadUser(Number(param.id))
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <UpdateUserForm updateUser={user} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdateUser