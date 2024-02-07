import { FC, useState } from "react"
import Button from "react-bootstrap/esm/Button"
import Form from "react-bootstrap/esm/Form"
import Spinner from "react-bootstrap/esm/Spinner"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RouteNames } from "../router"
import { AuthActionCreators } from "../store/reduce/auth/action-creators"


const LoginForm: FC = () => {
  const { error, isLoading } = useTypedSelector(state => state.auth)
  const [username, setLogin] = useState('')
  const [password, setPassword] = useState('')
  const { login } = useActions(AuthActionCreators)
  return (
    <Form>
      <Form.Group className="mb-3" controlId="formGroupEmail">
        <Form.Label>Email address</Form.Label>
        <Form.Control type="text" placeholder="Enter email" isInvalid={error?.email?.length > 0} onChange={e => setLogin(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type="invalid">
          {error.email?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-3" controlId="formGroupPassword">
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Password" isInvalid={error.password?.length > 0} onChange={e => setPassword(e.target.value)} disabled={isLoading} />
        <Form.Control.Feedback type='invalid'>
          {error.password?.map(t => <p key={t}>{t}</p>)}
        </Form.Control.Feedback>
      </Form.Group>
      <Button variant="primary" type="submit" disabled={isLoading} onClick={() => login(username, password)}>
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
        <p style={{ display: 'flex', justifyContent: 'space-between' }}>
          <a href={RouteNames.FORGOT_PASSWORD}>Forgot password</a>
          <a href={RouteNames.REGISTRATION}>Registration</a>
        </p>
      }
    </Form>
  )
}

export default LoginForm