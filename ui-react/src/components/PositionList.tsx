import { FC } from "react"
import { useTypedSelector } from "../hooks/useTypedSelector"
import PositionCard from "./PositionCard"

const PositionList: FC = () => {
    const { positions } = useTypedSelector(state => state.positions)
    return (
        <>
            {
                positions.map(t =>
                    <div key={t.id} style={{ margin: 15 }}>
                        <PositionCard position={t} />
                    </div>
                )
            }
        </>
    )
}

export default PositionList