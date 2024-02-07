import { FC } from "react"
import Spinner from "react-bootstrap/esm/Spinner"
import Stack from "react-bootstrap/esm/Stack"

const LoadingPage: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Spinner animation="border" />
        </Stack>
    )
}

export default LoadingPage