import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IUser } from "../models/IUser"
import { IUserProject } from "../models/IUserProject"
import { UserProjectActionCreators } from "../store/reduce/userproject/action-creators"

interface UserProjectFormProps {
    users: IUser[]
    userProject?: IUserProject
}

const UserProjectForm: FC<UserProjectFormProps> = ({ users, userProject }) => {
    const { positions } = useTypedSelector(t => t.positions)
    const [userId, setUserId] = useState(userProject ? userProject.userId : users[0]?.id.toString())
    const [positionId, setPositionId] = useState(userProject ? userProject.positionId : positions[0]?.id.toString())
    const { isLoading } = useTypedSelector(t => t.userproject)
    const { addUserProject, updateUserProject } = useActions(UserProjectActionCreators)
    const param = useParams()
    const navigate = useNavigate()
    const submit = async () => {
        let result
        if (userProject) {
            userProject.positionId = Number(positionId!)
            userProject.userId = Number(userId!)
            userProject.userEmail = users.find(t => t.id == Number(userId))?.email!
            result = await updateUserProject(userProject)
        }
        else {
            result = await addUserProject({ userId: Number(userId), positionId: Number(positionId), taskId: Number(param.id), userEmail: users.find(t => t.id == Number(userId))?.email } as IUserProject)

        }
        if (result) {
            navigate(`/tasks/${Number(param.id)}`)
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupUser">
                <Form.Label>User</Form.Label>
                <Form.Select aria-label="User" onChange={e => setUserId(e.target.value)} disabled={isLoading}>
                    {
                        users?.map(t =>
                            <option key={t.id} value={t.id}>{t.userName}</option>
                        )
                    }
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupPosition">
                <Form.Label>Position</Form.Label>
                <Form.Select aria-label="Position" onChange={e => setPositionId(e.target.value)} disabled={isLoading}>
                    {
                        positions?.map(t =>
                            <option key={t.id} value={t.id}>{t.name}</option>
                        )
                    }
                </Form.Select>
            </Form.Group>
            <Button variant="primary" type="button" onClick={() => submit()} disabled={isLoading}>
                {
                    isLoading &&
                    <Spinner
                        animation="border"
                        size="sm"
                    />
                }
                Submit
            </Button>
        </Form>
    )
}

export default UserProjectForm