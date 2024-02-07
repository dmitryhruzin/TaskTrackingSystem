import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import TaskCard from "./TaskCard"

const TaskList: FC = () => {
    const { tasks } = useTypedSelector(state => state.tasks)
    return (
        <>
            {
                tasks.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <TaskCard task={t} />
                    </div>
                )
            }
        </>
    )
}

export default TaskList