import { FC, useEffect } from "react"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import Dropdown from "react-bootstrap/esm/Dropdown"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RoleNames } from "../models/IRole"
import { StatusNames } from "../models/IStatus"
import { GetStatusesEnum, StatusesActionCreators } from "../store/reduce/statuses/action-creators"
import { TaskActionCreators } from "../store/reduce/task/action-creators"
import { convertDate } from "../utils/convertDate"


const TaskDetailCard: FC = () => {
    const navigate = useNavigate()
    const { task } = useTypedSelector(t => t.task)
    const { loadTask, deleteTask, updateTaskStatus } = useActions(TaskActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const { statuses } = useTypedSelector(t => t.statuses)
    const { loadStatuses } = useActions(StatusesActionCreators)
    const param = useParams()
    useEffect(() => {
        loadTask(Number(param.id))
        loadStatuses(GetStatusesEnum.BY_TASK)
    }, [])
    const submit = () => {
        deleteTask(task.id)
    }
    const updateStatus = async (status: string) => {
        const result = await updateTaskStatus(Number(param.id), statuses.find(t => t.name == status))
        if (result) {
            navigate(0)
        }
    }
    return (
        <Card
            style={{ width: 600, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{task.name}</Card.Title>
                <p>
                    {
                        task.description
                    }
                </p>
                <p style={{ marginTop: 15 }}>
                    Users: {task.userProjectIds?.length}
                </p>

                <p>
                    Start Date: {
                        convertDate(task.startDate)
                    }
                </p>
                <p>
                    Expiry Date: {
                        convertDate(task.expiryDate)
                    }
                </p>
                <p>
                    Status: {
                        task.statusName
                    }
                </p>
                <p>
                    Manager: {
                        task.managerUserName
                    }
                </p>
                <p>
                    Project: {
                        task.projectName
                    }
                </p>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Button onClick={() => navigate(`/task/${task.id}/users`)}>
                        Get All Users
                    </Button>
                    {
                        task.statusName == StatusNames.NOT_STARTED
                        &&
                        <Button onClick={() => updateStatus(StatusNames.IN_PROGRESS)}>
                            Start
                        </Button>
                    }
                    {
                        task.statusName == StatusNames.IN_PROGRESS
                        &&
                        <Button onClick={() => updateStatus(StatusNames.FINISHED)}>
                            Finish
                        </Button>
                    }
                    {
                        task.statusName == StatusNames.FINISHED
                        &&
                        <Button onClick={() => updateStatus(StatusNames.IN_PROGRESS)}>
                            Resume
                        </Button>
                    }
                    {
                        roles.find(t => t.name == RoleNames.MANAGER)
                        &&
                        <Dropdown>
                            <Dropdown.Toggle variant="success" id="dropdown-basic">
                                Manager Button
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                                <Dropdown.Item onClick={() => navigate(`/task/${task.id}/add/user/project`)}>Add User</Dropdown.Item>
                                <Dropdown.Item onClick={() => navigate(`/task/${task.id}/user/projects`)}>Get All Detail Users</Dropdown.Item>
                                <Dropdown.Item onClick={() => navigate(`/update/task/${task.id}`)}>Update Task</Dropdown.Item>
                                <Dropdown.Item onClick={() => submit()}>Delete Task</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    }
                </div>
            </Card.Body>
        </Card>
    )
}

export default TaskDetailCard