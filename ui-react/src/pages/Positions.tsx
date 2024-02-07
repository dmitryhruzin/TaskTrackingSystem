import { FC, useEffect } from "react"
import Stack from "react-bootstrap/esm/Stack"
import { useParams } from "react-router-dom"
import PositionList from "../components/PositionList"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { PositionsActionCreators } from "../store/reduce/positions/actions-creators"
import LoadingPage from "./LoadingPage"

const Positions: FC = () => {
    const { isLoading } = useTypedSelector(t => t.positions)
    const { loadPositions } = useActions(PositionsActionCreators)
    useEffect(() => {
        loadPositions()
    }, [])
    return (
        !isLoading
            ?
            <Stack className="d-flex flex-column min-vh-100 align-items-center">
                <PositionList />
            </Stack>
            :
            <LoadingPage />
    )
}

export default Positions