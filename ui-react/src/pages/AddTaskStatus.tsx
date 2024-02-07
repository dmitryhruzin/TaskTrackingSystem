import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import StatusForm from "../components/StatusForm"
import { StatusEnum } from "../store/reduce/status/action-creators"

const AddTaskStatus: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Card className="p-5">
                <StatusForm type={StatusEnum.BY_TASK} />
            </Card>
        </Stack>
    )
}

export default AddTaskStatus