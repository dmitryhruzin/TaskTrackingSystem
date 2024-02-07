import { FC } from "react"
import Stack from "react-bootstrap/esm/Stack"
import UserDetailCard from "../components/UserDetailCard"

const User: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <UserDetailCard />
        </Stack>
    )
}

export default User