import { FC, useEffect } from "react"
import Button from "react-bootstrap/esm/Button"
import Card from "react-bootstrap/esm/Card"
import Dropdown from "react-bootstrap/esm/Dropdown"
import { useNavigate, useParams } from "react-router-dom"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RoleNames } from "../models/IRole"
import { AuthActionCreators } from "../store/reduce/auth/action-creators"
import { UserActionCreators } from "../store/reduce/user/action-creators"


const UserDetailCard: FC = () => {
    const navigate = useNavigate()
    const { user: authUser } = useTypedSelector(t => t.auth)
    const { user } = useTypedSelector(t => t.user)
    const { loadUser, deleteUser } = useActions(UserActionCreators)
    const { logout } = useActions(AuthActionCreators)
    const { roles } = useTypedSelector(state => state.auth)
    const param = useParams()
    useEffect(() => {
        loadUser(Number(param.id))
    }, [])
    const submit = async () => {
        const result = await deleteUser(user.id)
        if (result && authUser.id == user.id) {
            logout()
        }
        else if (result) {
            navigate("/users")
        }
    }
    return (
        <Card
            style={{ width: 600, marginRight: 15, marginLeft: 15 }}
        >
            <Card.Body>
                <Card.Title>{`${user.firstName} ${user.lastName}`}</Card.Title>
                <p>
                    Email: {
                        user.email
                    }
                </p>
                <p>
                    UserName: {
                        user.userName
                    }
                </p>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    {(authUser.id == user.id) &&
                        <>
                            <Button onClick={() => navigate(`/update/user/${user.id}`)}>
                                Update Informations
                            </Button>
                            <Button onClick={() => submit()}>
                                Delete Account
                            </Button>
                        </>
                    }
                    {
                        roles.find(t => t.name == RoleNames.ADMINISTRATOR)
                        &&
                        <Dropdown>
                            <Dropdown.Toggle variant="success" id="dropdown-basic">
                                Administrator's Buttons
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                                <Dropdown.Item onClick={() => navigate(`/user/${user.id}/add/role`)}>Add Role</Dropdown.Item>
                                <Dropdown.Item onClick={() => navigate(`/user/${user.id}/delete/role`)}>Get User Roles</Dropdown.Item>
                                <Dropdown.Item onClick={() => navigate(`/update/user/${user.id}`)}>Update User</Dropdown.Item>
                                <Dropdown.Item onClick={() => submit()}>Delete User</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    }
                </div>
            </Card.Body>
        </Card>
    )
}

export default UserDetailCard