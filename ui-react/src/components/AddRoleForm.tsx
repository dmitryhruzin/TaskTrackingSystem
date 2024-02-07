import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IRole } from "../models/IRole"
import { UserActionCreators } from "../store/reduce/user/action-creators"

interface AddRoleFormProps {
    roles: IRole[]
}

const AddRoleForm: FC<AddRoleFormProps> = ({ roles }) => {
    const param = useParams()
    const navigate = useNavigate()
    const { isLoading } = useTypedSelector(t => t.user)
    const [roleId, setRoleId] = useState(roles[0]?.id.toString())
    const { addUserToRole } = useActions(UserActionCreators)
    const submit = async () => {
        const result = await addUserToRole(Number(param.id), roles.find(t => t.id == Number(roleId)))
        if (result) {
            navigate(`/users/${Number(param.id)}`)
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupRole">
                <Form.Label>Role</Form.Label>
                <Form.Select aria-label="Role" onChange={e => setRoleId(e.target.value)} disabled={isLoading}>
                    {
                        roles?.map(t =>
                            <option key={t.id} value={t.id}>{t.name}</option>
                        )
                    }
                </Form.Select>
            </Form.Group>
            <Button variant="primary" type="submit" disabled={isLoading} onClick={() => submit()}>
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

export default AddRoleForm