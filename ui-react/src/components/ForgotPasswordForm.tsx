import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IForgotPassword } from "../models/IForgotPassword"
import { RouteNames } from "../router"
import { AuthActionCreators } from "../store/reduce/auth/action-creators"


const ForgotPasswordForm: FC = () => {
    const { forgotError, isLoading } = useTypedSelector(state => state.auth)
    const [username, setLogin] = useState('')
    const [isSubmit, setSubmit] = useState(false)
    const { forgot } = useActions(AuthActionCreators)
    const submit = async () => {
        const result = await forgot({ email: username, clientURI: "http://localhost:3000/reset/" } as IForgotPassword)
        if(result){
            setSubmit(true)
        }
    }
    return (
        !isSubmit
            ?
            <Form>
                <Form.Group className="mb-3" controlId="formGroupEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="text" placeholder="Enter email" isInvalid={forgotError?.email?.length > 0} onChange={e => setLogin(e.target.value)} disabled={isLoading} />
                    <Form.Control.Feedback type="invalid">
                        {forgotError.email?.map(t => <p key={t}>{t}</p>)}
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
                {
                    !isLoading &&
                    <a href={RouteNames.LOGIN} style={{ marginLeft: 15 }}>Back</a>
                }
            </Form>
            :
            <p>
                Completed
            </p>
    )
}

export default ForgotPasswordForm