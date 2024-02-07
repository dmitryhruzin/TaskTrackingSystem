import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IProject } from "../models/IPoject"
import { ProjectActionCreators } from "../store/reduce/project/action-creators"
import { convertDate } from "../utils/convertDate"

interface ProjectFormProps {
  project?: IProject
}

const ProjectForm: FC<ProjectFormProps> = ({ project }) => {
  const { statuses } = useTypedSelector(t => t.statuses)
  statuses.unshift(...statuses.splice(statuses.indexOf(statuses.find(t => t?.id == project?.statusId)!), 1))
  const navigate = useNavigate()
  const { projectError, isLoading } = useTypedSelector(t => t.project)
  const [name, setProject] = useState(project?.name)
  const [description, setDescription] = useState(project?.description)
  const [expiryDate, setExpiryDate] = useState(project ? convertDate(project.expiryDate) : convertDate(new Date(Date.now())))
  const [statusId, setStatusId] = useState(project ? project.statusId : statuses[0]?.id.toString())
  const { addProject, updateProject } = useActions(ProjectActionCreators)
  const submit = async () => {
    if (project) {
      project.name = name!
      project.description = description!
      project.expiryDate = new Date(expiryDate!)
      project.statusId = Number(statusId!)
      const result = await updateProject(project)
      if (result) {
        navigate(`/projects/${project.id}`)
      }
    }
    else {
      const result = await addProject({ name, description, startDate: new Date(Date.now()), expiryDate: new Date(expiryDate), statusId: Number(statusId) } as IProject)
      if (result) {
        navigate('/projects')
      }
    }
  }
  return (
    <Form>
      <Form.Group className="mb-3" controlId="formGroupName">
        <Form.Label>Name</Form.Label>
        <Form.Control type="text" placeholder="Name" defaultValue={name} isInvalid={projectError?.name?.length > 0} onChange={e => setProject(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type="invalid">
          {projectError.name?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupDescription">
        <Form.Label>Description</Form.Label>
        <Form.Control as="textarea" rows={3} placeholder="Description" defaultValue={description} isInvalid={projectError?.description?.length > 0} onChange={e => setDescription(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type="invalid">
          {projectError.description?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupDate">
        <Form.Label>Expiry Date</Form.Label>
        <Form.Control type="date" isInvalid={projectError.expiryDate?.length > 0} defaultValue={expiryDate} onChange={e => setExpiryDate(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type='invalid'>
          {projectError.expiryDate?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupStatus">
        <Form.Label>Status</Form.Label>
        <Form.Select aria-label="Default select example" onChange={e => setStatusId(e.target.value)} disabled={isLoading}>
          {
            statuses?.map(t =>
              <option key={t?.id} value={t?.id}>{t?.name}</option>
            )
          }
        </Form.Select>
      </Form.Group>
      <Button variant="primary" type="button" disabled={isLoading} onClick={() => submit()}>
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

export default ProjectForm