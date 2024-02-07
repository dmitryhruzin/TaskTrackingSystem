import { FC } from "react"
import { useActions } from "../hooks/useActions"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import { arrayStatus, IStatus } from "../models/IStatus"
import { StatusActionCreators, StatusEnum } from "../store/reduce/status/action-creators"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { useNavigate } from "react-router-dom"
import { RoleNames } from "../models/IRole"


interface StatusCardProps {
    status: IStatus,
    type: StatusEnum
}

const StatusCard: FC<StatusCardProps> = ({ status, type }) => {
    const navigate = useNavigate()
    const { deleteStatus } = useActions(StatusActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const deleteSt = async () => {
        const result = await deleteStatus(type, status.id)
        if (result) {
            navigate(0)
        }
    }
    const updateSt = () => {
        switch (type) {
            case StatusEnum.BY_PROJECT:
                navigate(`/update/project/status/${status.id}`)
                break
            case StatusEnum.BY_TASK:
                navigate(`/update/task/status/${status.id}`)
                break
        }
    }
    return (
        <Card
            style={{ width: 300, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{status.name}</Card.Title>
                {
                    (roles.find(t => t.name == RoleNames.MANAGER) && !arrayStatus.includes(status.name))
                    &&
                    <div style={{ display: 'flex', justifyContent: 'space-around' }}>
                        <Button onClick={() => deleteSt()}>
                            Delete
                        </Button>
                        <Button onClick={() => updateSt()}>
                            Update
                        </Button>
                    </div>
                }
            </Card.Body>
        </Card>
    )
}

export default StatusCard