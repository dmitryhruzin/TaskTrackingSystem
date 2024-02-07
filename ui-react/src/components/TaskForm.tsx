import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { ITask } from "../models/ITask"
import { TaskActionCreators } from "../store/reduce/task/action-creators"
import { convertDate } from "../utils/convertDate"

interface TaskFormProps {
  task?: ITask
}

const TaskForm: FC<TaskFormProps> = ({ task }) => {
  const navigate = useNavigate()
  const { taskError, isLoading } = useTypedSelector(t => t.task)
  const { statuses } = useTypedSelector(t => t.statuses)
  statuses.unshift(...statuses.splice(statuses.indexOf(statuses.find(t => t?.id == task?.statusId)!), 1))
  const [name, setTask] = useState(task?.name)
  const [description, setDescription] = useState(task?.description)
  const [expiryDate, setExpiryDate] = useState(task ? convertDate(task.expiryDate) : convertDate(new Date(Date.now())))
  const [statusId, setStatusId] = useState(task ? task.statusId : statuses[0]?.id.toString())
  const { addTask, updateTask } = useActions(TaskActionCreators)
  const param = useParams()
  const submit = async () => {
    if (task) {
      task.name = name!
      task.description = description!
      task.expiryDate = new Date(expiryDate!)
      task.statusId = Number(statusId!)
      const result = await updateTask(task)
      if (result) {
        navigate(`/tasks/${task.id}`)
      }
    }
    else {
      const result = await addTask({ name, description, startDate: new Date(Date.now()), expiryDate: new Date(expiryDate), statusId: Number(statusId), projectId: Number(param.id), managerId: Number(localStorage.getItem('id')) } as ITask)
      if (result) {
        navigate(`/project/${Number(param.id)}/tasks`)
      }
    }
  }
  return (
    <Form>
      <Form.Group className="mb-3" controlId="formGroupName">
        <Form.Label>Name</Form.Label>
        <Form.Control type="text" placeholder="Name" defaultValue={name} isInvalid={taskError?.name?.length > 0} onChange={e => setTask(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type="invalid">
          {taskError.name?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupDescription">
        <Form.Label>Description</Form.Label>
        <Form.Control as="textarea" rows={3} placeholder="Description" defaultValue={description} isInvalid={taskError?.description?.length > 0} onChange={e => setDescription(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type="invalid">
          {taskError.description?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupDate">
        <Form.Label>Expiry Date</Form.Label>
        <Form.Control type="date" isInvalid={taskError.expiryDate?.length > 0} defaultValue={expiryDate} onChange={e => setExpiryDate(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type='invalid'>
          {taskError.expiryDate?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupStatus">
        <Form.Label>Status</Form.Label>
        <Form.Select aria-label="Default select example" onChange={e => setStatusId(e.target.value)} disabled={isLoading}>
          {
            statuses?.map(t =>
              <option key={t.id} value={t.id}>{t.name}</option>
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

export default TaskForm