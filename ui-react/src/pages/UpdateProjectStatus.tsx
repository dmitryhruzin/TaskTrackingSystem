import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import StatusForm from "../components/StatusForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { StatusActionCreators, StatusEnum } from "../store/reduce/status/action-creators"
import LoadingPage from "./LoadingPage"

const UpdateProjectStatus: FC = () => {
    const param = useParams()
    const { isLoading, status } = useTypedSelector(t => t.status)
    const { loadStatus } = useActions(StatusActionCreators)
    useEffect(() => {
        loadStatus(StatusEnum.BY_PROJECT, Number(param.id))
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <StatusForm type={StatusEnum.BY_PROJECT} status={status} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdateProjectStatus