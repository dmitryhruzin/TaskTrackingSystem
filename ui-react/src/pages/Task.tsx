import { FC } from "react"
import Stack from "react-bootstrap/esm/Stack"
import TaskDetailCard from "../components/TaskDetailCard"

const Task: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <TaskDetailCard />
        </Stack>
    )
}

export default Task