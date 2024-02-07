import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import StatusList from "../components/StatusList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { StatusEnum } from "../store/reduce/status/action-creators"
import { GetStatusesEnum, StatusesActionCreators } from "../store/reduce/statuses/action-creators"
import LoadingPage from "./LoadingPage"

const ProjectStatuses: FC = () => {
    const { isLoading } = useTypedSelector(t => t.statuses)
    const { loadStatuses } = useActions(StatusesActionCreators)
    useEffect(() => {
        loadStatuses(GetStatusesEnum.BY_PROJECT)
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 align-items-center">
                <StatusList type={StatusEnum.BY_PROJECT} />
            </Stack>
            :
            <LoadingPage />
    )
}

export default ProjectStatuses