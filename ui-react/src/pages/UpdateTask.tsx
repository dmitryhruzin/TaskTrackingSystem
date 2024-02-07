import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import TaskForm from "../components/TaskForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { GetStatusesEnum, StatusesActionCreators } from "../store/reduce/statuses/action-creators"
import { TaskActionCreators } from "../store/reduce/task/action-creators"
import LoadingPage from "./LoadingPage"

const UpdateTask: FC = () => {
    const param = useParams()
    const { task, isLoading } = useTypedSelector(t => t.task)
    const { isLoading: isLoadingStatuses } = useTypedSelector(t => t.statuses)
    const { loadStatuses } = useActions(StatusesActionCreators)
    const { loadTask } = useActions(TaskActionCreators)
    useEffect(() => {
        loadTask(Number(param.id))
        loadStatuses(GetStatusesEnum.BY_TASK)
    }, [])
    return (
        (!isLoadingStatuses && !isLoading)
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <TaskForm task={task} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdateTask