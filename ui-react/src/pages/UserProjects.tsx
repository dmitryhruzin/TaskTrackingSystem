import { FC } from "react"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import UserProjectList from "../components/UserProjectList"

const UserProjects: FC = () => {
    const param = useParams()
    return (
        <Stack className="d-flex flex-column min-vh-100 align-items-center">
            <UserProjectList id={Number(param.id)} />
        </Stack>
    )
}

export default UserProjects