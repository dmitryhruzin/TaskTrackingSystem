import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IRegisterUser } from "../models/IRegisterUser"
import { UserActionCreators } from "../store/reduce/user/action-creators"

const AddUserForm: FC = () => {
    const navigate = useNavigate()
    const { registerError, isLoading } = useTypedSelector(t => t.user)
    const { roles } = useTypedSelector(t => t.roles)
    const [email, setLogin] = useState('')
    const [userName, setUserName] = useState('')
    const [password, setPassword] = useState('')
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [roleName, setRoleName] = useState(roles[0]?.name)
    const { addUser } = useActions(UserActionCreators)
    const submit = async () => {
        const result = await addUser({ email, userName, password, confirmPassword: password, firstName, lastName, role: roleName } as IRegisterUser)
        if (result) {
            navigate("/users")
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="text" placeholder="Enter email" isInvalid={registerError?.email?.length > 0} onChange={e => setLogin(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {registerError.email?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupUserName">
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Username" isInvalid={registerError?.userName?.length > 0} onChange={e => setUserName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {registerError.userName?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" isInvalid={registerError.password?.length > 0} onChange={e => setPassword(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type='invalid'>
                    {registerError.password?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupFirstName">
                <Form.Label>Firstname</Form.Label>
                <Form.Control type="text" placeholder="Firstname" isInvalid={registerError?.firstName?.length > 0} onChange={e => setFirstName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {registerError.firstName?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupLastName">
                <Form.Label>Lastname</Form.Label>
                <Form.Control type="text" placeholder="Lastname" isInvalid={registerError?.lastName?.length > 0} onChange={e => setLastName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {registerError.lastName?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupRole">
                <Form.Label>Role</Form.Label>
                <Form.Select aria-label="Role" onChange={e => setRoleName(e.target.value)} disabled={isLoading}>
                    {
                        roles?.map(t =>
                            <option key={t.id} value={t.name}>{t.name}</option>
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

export default AddUserForm