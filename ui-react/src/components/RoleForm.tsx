import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IRole } from "../models/IRole"
import { RoleActionCreators } from "../store/reduce/role/action-creators"


interface RoleFormProps {
    role?: IRole
}

const RoleForm: FC<RoleFormProps> = ({ role }) => {
    const { roleError, isLoading } = useTypedSelector(t => t.role)
    const navigate = useNavigate()
    const [name, setName] = useState(role?.name)
    const { addRole, updateRole } = useActions(RoleActionCreators)
    const sumbit = async () => {
        let result
        if (role) {
            role.name = name!
            result = await updateRole(role)
        }
        else {
            result = await addRole({ name } as IRole)
        }
        if (result) {
            navigate("/roles")
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupName">
                <Form.Label>Role Name</Form.Label>
                <Form.Control type="text" placeholder="Role Name" defaultValue={name} isInvalid={roleError?.name?.length > 0} onChange={e => setName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {roleError.name?.map(t => <p key={t}>{t}</p>)}
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

export default RoleForm