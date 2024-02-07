import { FC } from "react"
import { Card } from "react-bootstrap"
import Stack from "react-bootstrap/esm/Stack"
import RegistrationForm from "../components/RegistrationForm"

const Registration: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Card className="p-5">
                <RegistrationForm />
            </Card>
        </Stack>
    )
}

export default Registration