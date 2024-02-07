import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import ForgotPasswordForm from "../components/ForgotPasswordForm"


const ForgotPassword: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Card className="p-5">
                <ForgotPasswordForm />
            </Card>
        </Stack>
    )
}

export default ForgotPassword