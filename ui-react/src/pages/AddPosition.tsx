import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import PositionForm from "../components/PositionForm"

const AddPosition: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Card className="p-5">
                <PositionForm />
            </Card>
        </Stack>
    )
}

export default AddPosition