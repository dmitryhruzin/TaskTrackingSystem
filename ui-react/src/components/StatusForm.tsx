import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IStatus } from "../models/IStatus"
import { StatusActionCreators, StatusEnum } from "../store/reduce/status/action-creators"

interface StatusFormProps {
    type: StatusEnum
    status?: IStatus
}

const StatusForm: FC<StatusFormProps> = ({ type, status }) => {
    const navigate = useNavigate()
    const { statusError, isLoading } = useTypedSelector(t => t.status)
    const [name, setName] = useState(status?.name)
    const { addStatus, updateStatus } = useActions(StatusActionCreators)
    const submit = async () => {
        let result
        if (status) {
            status.name = name!
            result = await updateStatus(type, status)
        }
        else {
            result = await addStatus(type, { name } as IStatus)
        }
        if (result) {
            switch (type) {
                case StatusEnum.BY_PROJECT:
                    navigate("/project/statuses")
                    break
                case StatusEnum.BY_TASK:
                    navigate("/task/statuses")
                    break
            }
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupName">
                <Form.Label>Status Name</Form.Label>
                <Form.Control type="text" placeholder="Status Name" defaultValue={name} isInvalid={statusError?.name?.length > 0} onChange={e => setName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {statusError.name?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
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

export default StatusForm