import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import TaskList from "../components/TaskList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { GetTasksEnum, TasksActionCreators } from "../store/reduce/tasks/action-creators"
import LoadingPage from "./LoadingPage"

const ProjectTasks: FC = () => {
    const param = useParams()
    const { isLoading } = useTypedSelector(t => t.tasks)
    const { loadTasks } = useActions(TasksActionCreators)
    useEffect(() => {
        loadTasks(GetTasksEnum.BY_PROJECT_ID, Number(param.id))
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

export default ProjectTasks