import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IPosition } from "../models/IPosition"
import { PositionActionCreators } from "../store/reduce/position/action-creators"

interface PositionFormProps {
    position?: IPosition
}

const PositionForm: FC<PositionFormProps> = ({ position }) => {
    const { positionError, isLoading } = useTypedSelector(t => t.position)
    const navigate = useNavigate()
    const [name, setName] = useState(position?.name)
    const [description, setDescription] = useState(position?.description)
    const { addPosition, updatePosition } = useActions(PositionActionCreators)
    const sumbit = async () => {
        let result
        if (position) {
            position.description = description!
            position.name = name!
            result = await updatePosition(position)
        }
        else {
            result = await addPosition({ name, description } as IPosition)
        }
        if (result) {
            navigate("/positions")
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupName">
                <Form.Label>Position Name</Form.Label>
                <Form.Control type="text" placeholder="Position Name" defaultValue={name} isInvalid={positionError?.name?.length > 0} onChange={e => setName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {positionError.name?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupDescription">
                <Form.Label>Description</Form.Label>
                <Form.Control as="textarea" rows={3} placeholder="Description" defaultValue={description} isInvalid={positionError?.description?.length > 0} onChange={e => setDescription(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {positionError.description?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Button variant="primary" type="button" disabled={isLoading} onClick={() => sumbit()}>
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

export default PositionForm