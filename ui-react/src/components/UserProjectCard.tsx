import { FC } from "react"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RoleNames } from "../models/IRole"
import { IUserProject } from "../models/IUserProject"
import { UserProjectActionCreators } from "../store/reduce/userproject/action-creators"


interface UserProjectCardProps {
    userProject: IUserProject
}

const UserProjectCard: FC<UserProjectCardProps> = ({ userProject }) => {
    const { deleteUserProject } = useActions(UserProjectActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const navigate = useNavigate()
    const deletePU = async () => {
        const result = await deleteUserProject(userProject.id)
        if (result) {
            navigate(0)
        }
    }
    const updatePU = () => {
        navigate(`/update/user/project/${userProject.id}`)
    }
    return (
        <Card
            style={{ width: 300, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{userProject.id}</Card.Title>
                <p>
                    User: {
                        userProject.userName
                    }
                </p>
                <p>
                    Position: {
                        userProject.positionName
                    }
                </p>
                <p>
                    Task: {
                        userProject.taskName
                    }
                </p>
                {
                    roles.find(t => t.name == RoleNames.MANAGER)
                    &&
                    <div style={{ display: 'flex', justifyContent: 'space-around' }}>
                        <Button onClick={() => deletePU()}>
                            Delete
                        </Button>
                        <Button onClick={() => updatePU()}>
                            Update
                        </Button>
                    </div>
                }
            </Card.Body>
        </Card>
    )
}

export default UserProjectCard