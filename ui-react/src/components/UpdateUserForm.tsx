import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IUpdateUser } from "../models/IUpdateUser"
import { IUser } from "../models/IUser"
import { UserActionCreators } from "../store/reduce/user/action-creators"

interface UpdateUserFormProps {
    updateUser: IUser
}

const UpdateUserForm: FC<UpdateUserFormProps> = ({ updateUser }) => {
    const navigate = useNavigate()
    const { updateError, isLoading } = useTypedSelector(t => t.user)
    const { user } = useTypedSelector(t => t.auth)
    const [email, setLogin] = useState(updateUser.email)
    const [userName, setUserName] = useState(updateUser.userName)
    const [firstName, setFirstName] = useState(updateUser.firstName)
    const [lastName, setLastName] = useState(updateUser.lastName)
    const { updateUser: up } = useActions(UserActionCreators)
    const submit = async () => {
        const result = await up({ id: updateUser.id, email, userName, firstName, lastName } as IUpdateUser)
        if (result) {
            if (user.id == updateUser.id) {
                navigate(`/user/${updateUser.id}`)
            }
            else {
                navigate(`/users/${updateUser.id}`)
            }
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="text" placeholder="Enter email" defaultValue={email} isInvalid={updateError?.email?.length > 0} onChange={e => setLogin(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {updateError.email?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupUserName">
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Username" defaultValue={userName} isInvalid={updateError?.userName?.length > 0} onChange={e => setUserName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {updateError.userName?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupFirstName">
                <Form.Label>Firstname</Form.Label>
                <Form.Control type="text" placeholder="Firstname" defaultValue={firstName} isInvalid={updateError?.firstName?.length > 0} onChange={e => setFirstName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {updateError.firstName?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupLastName">
                <Form.Label>Lastname</Form.Label>
                <Form.Control type="text" placeholder="Lastname" defaultValue={lastName} isInvalid={updateError?.lastName?.length > 0} onChange={e => setLastName(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type="invalid">
                    {updateError.lastName?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
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

export default UpdateUserForm