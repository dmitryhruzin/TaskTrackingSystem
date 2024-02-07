import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useNavigate, useSearchParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { IResetPassword } from "../models/IResetPassword"
import { AuthActionCreators } from "../store/reduce/auth/action-creators"


const ResetPasswordForm: FC = () => {
    const navigate = useNavigate()
    const { resetError, isLoading } = useTypedSelector(state => state.auth)
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')
    const { reset } = useActions(AuthActionCreators)
    const [searchParams, setSearchParams] = useSearchParams();
    const params = {} as any;
    searchParams.forEach((key, value) => {
        params[value] = key;
      });
    const submit = async () => {
        const result = await reset({ password, confirmPassword, token: params.token, email: params.email } as IResetPassword)
        if (result) {
            navigate("/login")
        }
    }
    return (
        <Form>
            <Form.Group className="mb-3" controlId="formGroupPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" isInvalid={resetError.password?.length > 0} onChange={e => setPassword(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type='invalid'>
                    {resetError.password?.map(t => <p key={t}>{t}</p>)}
                </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGroupConfirmPassword">
                <Form.Label>Confirm password</Form.Label>
                <Form.Control type="password" placeholder="Confirm Password" isInvalid={resetError.confirmPassword?.length > 0} onChange={e => setConfirmPassword(e.target.value)} disabled={isLoading} />
                <Form.Control.Feedback type='invalid'>
                    {resetError.confirmPassword?.map(t => <p key={t}>{t}</p>)}
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

export default ResetPasswordForm