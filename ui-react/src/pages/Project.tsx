import { FC } from "react"
import Stack from "react-bootstrap/esm/Stack"
import ProjectDetailCard from "../components/ProjectDetailCard"

const Project: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <ProjectDetailCard />
        </Stack>
    )
}

export default Project