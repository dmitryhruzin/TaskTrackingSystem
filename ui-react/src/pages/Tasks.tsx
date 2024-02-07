import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import TaskList from "../components/TaskList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { GetTasksEnum, TasksActionCreators } from "../store/reduce/tasks/action-creators"
import LoadingPage from "./LoadingPage"

const Tasks: FC = () => {
    const { isLoading } = useTypedSelector(t => t.tasks)
    const { loadTasks } = useActions(TasksActionCreators)
    useEffect(() => {
        loadTasks(GetTasksEnum.ALL_TASK)
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 align-items-center">
                <TaskList />
            </Stack>
            :
            <LoadingPage />
    )
}

export default Tasks