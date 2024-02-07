import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { StatusEnum } from "../store/reduce/status/action-creators"
import StatusCard from "./StatusCard"


interface StatusListProps {
    type: StatusEnum
}

const StatusList: FC<StatusListProps> = ({ type }) => {
    const { statuses } = useTypedSelector(state => state.statuses)
    return (
        <>
            {
                statuses.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <StatusCard status={t} type={type} />
                    </div>
                )
            }
        </>
    )
}

export default StatusList