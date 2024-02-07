import { FC, useEffect } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import PositionForm from "../components/PositionForm"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { PositionActionCreators } from "../store/reduce/position/action-creators"
import LoadingPage from "./LoadingPage"

const UpdatePosition: FC = () => {
    const param = useParams()
    const { isLoading, position } = useTypedSelector(t => t.position)
    const { loadPosition } = useActions(PositionActionCreators)
    useEffect(() => {
        loadPosition(Number(param.id))
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
                <Card className="p-5">
                    <PositionForm position={position} />
                </Card>
            </Stack>
            :
            <LoadingPage />
    )
}

export default UpdatePosition