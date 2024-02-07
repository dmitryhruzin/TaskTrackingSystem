import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import ProjectList from "../components/ProjectList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { ProjectsActionCreators } from "../store/reduce/projects/action-creators"
import LoadingPage from "./LoadingPage"

const Projects: FC = () => {
    const { isLoading } = useTypedSelector(state => state.projects)
    const { loadProjects } = useActions(ProjectsActionCreators)
    useEffect(() => {
        loadProjects()
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 align-items-center">
                <ProjectList />
            </Stack>
            :
            <LoadingPage />
    )
}

export default Projects