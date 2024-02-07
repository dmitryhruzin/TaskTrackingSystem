import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import { useNavigate } from "react-router-dom"
import { ITask } from "../models/ITask"
import { convertDate } from "../utils/convertDate"


interface TaskCardProps {
    task: ITask
}

const TaskCard: FC<TaskCardProps> = ({ task }) => {
    const navigate = useNavigate()
    return (
        <Card
            style={{ width: 600, marginRight: 15, marginLeft: 15 }}
            onClick={() => { navigate(`/tasks/${task.id}`) }}
        >
            <Card.Body>
                <Card.Title>{task.name}</Card.Title>
                <p>
                    {
                        task.description
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
                    <div>
                        Users:
                        {task.userProjectIds.length}
                    </div>
                    <div>
                        Start Date: <a style={{ marginRight: 15 }}>
                            {
                                convertDate(task.startDate)
                            }
                        </a>
                        Expiry Date: <>
                            {
                                convertDate(task.expiryDate)
                            }
                        </>
                    </div>
                </div>
            </Card.Body>
        </Card>
    )
}

export default TaskCard