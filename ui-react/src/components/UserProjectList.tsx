import { FC, useEffect } from "react"
import { useActions } from "../hooks/useActions"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { UserProjectsActionCreators } from "../store/reduce/userprojects/action-creators"
import UserProjectCard from "./UserProjectCard"


interface UserProjectListProps {
    id?: number
}

const UserProjectList: FC<UserProjectListProps> = ({ id }) => {
    const { userProjects } = useTypedSelector(state => state.userprojects)
    const { loadUserProjects } = useActions(UserProjectsActionCreators)
    useEffect(() => {
        loadUserProjects(id)
    }, [])
    return (
        <>
            {
                userProjects.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <UserProjectCard userProject={t} />
                    </div>
                )
            }
        </>
    )
}

export default UserProjectList