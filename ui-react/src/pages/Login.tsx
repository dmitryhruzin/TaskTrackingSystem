import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import LoginForm from "../components/LoginForm"

const Login: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Card className="p-5">
                <LoginForm />
            </Card>
        </Stack>
    )
}

export default Login