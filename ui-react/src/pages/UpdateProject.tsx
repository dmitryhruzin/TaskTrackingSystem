import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import ProjectForm from "../components/ProjectForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { ProjectActionCreators } from "../store/reduce/project/action-creators"
import { GetStatusesEnum, StatusesActionCreators } from "../store/reduce/statuses/action-creators"
import LoadingPage from "./LoadingPage"

const UpdateProject: FC = () => {
    const param = useParams()
    const { project, isLoading } = useTypedSelector(t => t.project)
    const { isLoading: isLoadingStatuses } = useTypedSelector(t => t.statuses)
    const { loadStatuses } = useActions(StatusesActionCreators)
    const { loadProject } = useActions(ProjectActionCreators)
    useEffect(() => {
        loadProject(Number(param.id))
        loadStatuses(GetStatusesEnum.BY_PROJECT)
    }, [])
    return (
        (!isLoadingStatuses && !isLoading)
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <ProjectForm project={project} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdateProject