import { FC } from "react"
import { useActions } from "../hooks/useActions"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import { IPosition } from "../models/IPosition"
import { PositionActionCreators } from "../store/reduce/position/action-creators"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { useNavigate } from "react-router-dom"
import { RoleNames } from "../models/IRole"


interface PositionCardProps {
    position: IPosition
}

const PositionCard: FC<PositionCardProps> = ({ position }) => {
    const navigate = useNavigate()
    const { deletePosition } = useActions(PositionActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const deletePos = async () => {
        const result = await deletePosition(position.id)
        if (result) {
            navigate(0)
        }
    }
    const updatePos = () => {
        navigate(`/update/position/${position.id}`)
    }
    return (
        <Card
            style={{ width: 300, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{position.name}</Card.Title>
                <p>
                    {
                        position.description
                    }
                </p>
                {
                    roles.find(t => t.name == RoleNames.MANAGER)
                    &&
                    <div style={{ display: 'flex', justifyContent: 'space-around' }}>
                        <Button onClick={() => deletePos()}>
                            Delete
                        </Button>
                        <Button onClick={() => updatePos()}>
                            Update
                        </Button>
                    </div>
                }
            </Card.Body>
        </Card>
    )
}

export default PositionCard