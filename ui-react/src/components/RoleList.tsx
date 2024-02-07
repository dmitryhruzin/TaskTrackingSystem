import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import RoleCard from "./RoleCard"


const RoleList: FC = () => {
    const { roles } = useTypedSelector(state => state.roles)
    return (
        <>
            {
                roles.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <RoleCard role={t} />
                    </div>
                )
            }
        </>
    )
}

export default RoleList