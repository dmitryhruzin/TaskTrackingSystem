import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import { useNavigate } from "react-router-dom"
import { IProject } from "../models/IPoject"
import { convertDate } from "../utils/convertDate"


interface PrjectCardProps {
    project: IProject
}

const ProjectCard: FC<PrjectCardProps> = ({ project }) => {
    const navigate = useNavigate()
    return (
        <Card
            style={{ width: 600, marginRight: 15, marginLeft: 15 }}
            onClick={() => { navigate(`/projects/${project.id}`) }}
        >
            <Card.Body>
                <Card.Title>{project.name}</Card.Title>
                <p>
                    {
                        project.description
                    }
                </p>
                <p>
                    {
                        project.statusName
                    }
                </p>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <div>
                        Tasks:
                        {project.taskIds.length}
                    </div>
                    <div>
                        Start Date: <a style={{ marginRight: 15 }}>
                            {
                                convertDate(project.startDate)
                            }
                        </a>
                        Expiry Date: <>
                            {
                                convertDate(project.expiryDate)
                            }
                        </>
                    </div>
                </div>
            </Card.Body>
        </Card>
    )
}

export default ProjectCard