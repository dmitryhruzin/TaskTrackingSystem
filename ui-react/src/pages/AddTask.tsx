import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import TaskForm from "../components/TaskForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { GetStatusesEnum, StatusesActionCreators } from "../store/reduce/statuses/action-creators"
import LoadingPage from "./LoadingPage"

const AddTask: FC = () => {
    const { isLoading } = useTypedSelector(t => t.statuses)
    const { loadStatuses } = useActions(StatusesActionCreators)
    useEffect(() => {
        loadStatuses(GetStatusesEnum.BY_TASK)
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <TaskForm />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default AddTask