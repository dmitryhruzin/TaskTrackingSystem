import { AppDispatch } from "../.."
import PositionService from "../../../api/PositionService"
import { IPosition } from "../../../models/IPosition"
import { PositionsActionEnum, SetErrorAction, SetIsLoadingAction, SetPositionsAction } from "./types"


export const PositionsActionCreators = {
    setPositions: (positions: IPosition[]): SetPositionsAction => ({ type: PositionsActionEnum.SET_POSITIONS, payload: positions }),
    setError: (payload: string): SetErrorAction => ({ type: PositionsActionEnum.SET_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: PositionsActionEnum.SET_IS_LOADING, payload }),
    loadPositions: (id?: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(PositionsActionCreators.setPositions([] as IPosition[]))
            dispatch(PositionsActionCreators.setError(""))
            dispatch(PositionsActionCreators.setIsLoading(true))
            let response
            if (id) {
                response = await PositionService.getPositionsByUserId(id)
            }
            else {
                response = await PositionService.getPositions()
            }
            const positions = response.data
            if (positions.length !== 0) {
                dispatch(PositionsActionCreators.setPositions(positions))
            }
            else {
                dispatch(PositionsActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(PositionsActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(PositionsActionCreators.setIsLoading(false))
        }
    }
}