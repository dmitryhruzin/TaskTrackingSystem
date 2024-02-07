import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import { useNavigate } from "react-router-dom"
import { IUser } from "../models/IUser"


interface UserCardProps {
    user: IUser
}

const UserCard: FC<UserCardProps> = ({ user }) => {
    const navigate = useNavigate()
    return (
        <Card
            style={{ width: 300, marginRight: 15, marginLeft: 15 }}
            onClick={() => { navigate(`/users/${user.id}`) }}
        >
            <Card.Body>
                <Card.Title>{user.userName}</Card.Title>
                <p>
                    {
                        `${user.firstName} ${user.lastName}`
                    }
                    {

                    }
                </p>
                <p>
                    Email: {
                        user.email
                    }
                </p>
                <p>
                    Tasks: {
                        user.taskIds.length
                    }
                </p>
            </Card.Body>
        </Card>
    )
}

export default UserCard