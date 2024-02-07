import { FC } from "react"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { arrayRole, IRole, RoleNames } from "../models/IRole"
import { RoleActionCreators } from "../store/reduce/role/action-creators"


interface RoleCardProps {
    role: IRole
}

const RoleCard: FC<RoleCardProps> = ({ role }) => {
    const navigate = useNavigate()
    const { deleteRole } = useActions(RoleActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const deleteRol = async () => {
        const result = await deleteRole(role.id)
        if (result) {
            navigate(0)
        }
    }
    const updateRol = () => {
        navigate(`/update/role/${role.id}`)
    }
    return (
        <Card
            style={{ width: 300, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{role.name}</Card.Title>
                {
                    (roles.find(t => t.name == RoleNames.ADMINISTRATOR) && !arrayRole.includes(role.name))
                    &&
                    <div style={{ display: 'flex', justifyContent: 'space-around' }}>
                        <Button onClick={() => deleteRol()}>
                            Delete
                        </Button>
                        <Button onClick={() => updateRol()}>
                            Update
                        </Button>
                    </div>
                }
            </Card.Body>
        </Card>
    )
}

export default RoleCard