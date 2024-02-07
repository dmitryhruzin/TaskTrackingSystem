import { FC, useEffect } from "react"
import { Button, Dropdown } from "react-bootstrap"
import Card from "react-bootstrap/esm/Card"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RoleNames } from "../models/IRole"
import { ProjectActionCreators } from "../store/reduce/project/action-creators"
import { convertDate } from "../utils/convertDate"



const ProjectDetailCard: FC = () => {
    const navigate = useNavigate()
    const { project } = useTypedSelector(t => t.project)
    const { loadProject, deleteProject } = useActions(ProjectActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const param = useParams()
    useEffect(() => {
        loadProject(Number(param.id))
    }, [])
    const submit = async () => {
        const result = await deleteProject(project.id)
        if (result) {
            navigate('/projects')
        }
    }
    return (
        <Card
            style={{ width: 600, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{project.name}</Card.Title>
                <p>
                    {
                        project.description
                    }
                </p>
                <p style={{ marginTop: 15 }}>
                    Tasks: {project.taskIds?.length}
                </p>

                <p>
                    Start Date: {
                        convertDate(project.startDate)
                    }
                </p>
                <p>
                    Expiry Date: {
                        convertDate(project.expiryDate)
                    }
                </p>
                <p>
                    Status: {
                        project.statusName
                    }
                </p>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Button onClick={() => navigate(`/project/${project.id}/tasks`)}>
                        Get All Tasks
                    </Button>
                    <Button onClick={() => navigate(`/project/${project.id}/users`)}>
                        Get All Users
                    </Button>
                    {
                        roles.find(t => t.name == RoleNames.MANAGER)
                        &&
                        <Dropdown>
                            <Dropdown.Toggle variant="success" id="dropdown-basic">
                                Manager's Buttons
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                                <Dropdown.Item onClick={() => navigate(`/add/task/${project.id}`)}>Add Task</Dropdown.Item>
                                <Dropdown.Item onClick={() => navigate(`/update/projects/${project.id}`)}>Update Project</Dropdown.Item>
                                <Dropdown.Item onClick={() => submit()}>Delete Project</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    }
                </div>
            </Card.Body>
        </Card>
    )
}

export default ProjectDetailCard