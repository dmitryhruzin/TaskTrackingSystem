import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import ProjectCard from "./ProjectCard"

const ProjectList: FC = () => {
    const { projects } = useTypedSelector(state => state.projects)
    return (
        <>
            {
                projects.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <ProjectCard project={t} />
                    </div>
                )
            }
        </>
    )
}

export default ProjectList