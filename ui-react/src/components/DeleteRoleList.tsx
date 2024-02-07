import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import DeleteRoleCard from "./DeleteRoleCard"


const DeleteRoleList: FC = () => {
    const { roles } = useTypedSelector(state => state.user)
    return (
        <>
            {
                roles.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <DeleteRoleCard role={t} isOne={roles.length == 1} />
                    </div>
                )
            }
        </>
    )
}

export default DeleteRoleList