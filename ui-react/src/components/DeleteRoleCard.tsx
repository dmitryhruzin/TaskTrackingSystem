import { FC } from "react"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { IRole } from "../models/IRole"
import { UserActionCreators } from "../store/reduce/user/action-creators"

interface DeleteRoleCardProps {
    role: IRole
    isOne: boolean
}

const DeleteRoleCard: FC<DeleteRoleCardProps> = ({ role, isOne }) => {
    const param = useParams()
    const navigate = useNavigate()
    const { deleteUserToRole } = useActions(UserActionCreators)
    const deleteRole = async () => {
        const result = await deleteUserToRole(Number(param.id), role.name)
        if (result) {
            navigate(0)
        }
    }
    return (
        <Card
            style={{ width: 200, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{role.name}</Card.Title>
                {
                    !isOne &&
                    <Button onClick={() => deleteRole()}>
                        Delete
                    </Button>
                }

            </Card.Body>
        </Card>
    )
}

export default DeleteRoleCard