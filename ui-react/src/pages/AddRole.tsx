import { FC } from "react"
import Card from "react-bootstrap/esm/Card"
import Stack from "react-bootstrap/esm/Stack"
import RoleForm from "../components/RoleForm"

const AddRole: FC = () => {
    return (
        <Stack className="d-flex flex-column min-vh-100 justify-content-center align-items-center">
            <Card className="p-5">
                <RoleForm />
            </Card>
        </Stack>
    )
}

export default AddRole