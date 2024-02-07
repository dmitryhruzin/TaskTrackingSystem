import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import UserCard from "./UserCard"


const UserList: FC = () => {
    const { users } = useTypedSelector(state => state.users)
    return (
        <>
            {
                users.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <UserCard user={t} />
                    </div>
                )
            }
        </>
    )
}

export default UserList