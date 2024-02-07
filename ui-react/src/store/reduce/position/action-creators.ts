import { AxiosError } from "axios"
import { AppDispatch } from "../.."
import PositionService from "../../../api/PositionService"
import { IPosition } from "../../../models/IPosition"
import { IPositionFormError } from "../../../models/IPositionFormError"
import { PositionActionEnum, SetErrorAction, SetIsLoadingAction, SetPositionAction, SetPositionErrorAction } from "./types"


export const PositionActionCreators = {
    setPosition: (position: IPosition): SetPositionAction => ({ type: PositionActionEnum.SET_POSITION, payload: position }),
    setError: (payload: string): SetErrorAction => ({ type: PositionActionEnum.SET_ERROR, payload }),
    setPositionError: (payload: IPositionFormError): SetPositionErrorAction => ({ type: PositionActionEnum.SET_POSITION_ERROR, payload }),
    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: PositionActionEnum.SET_IS_LOADING, payload }),
    loadPosition: (id: number) => async (dispatch: AppDispatch) => {
        try {
            dispatch(PositionActionCreators.setPosition({} as IPosition))
            dispatch(PositionActionCreators.setError(""))
            dispatch(PositionActionCreators.setIsLoading(true))
            const response = await PositionService.getPosition(id)
            const position = response.data
            if (position) {
                dispatch(PositionActionCreators.setPosition(position))
            }
            else {
                dispatch(PositionActionCreators.setError("Not found."))
            }
        }
        catch (e) {
            dispatch(PositionActionCreators.setError((e as Error).message))
        }
        finally {
            dispatch(PositionActionCreators.setIsLoading(false))
        }
    },
    addPosition: (position: IPosition) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(PositionActionCreators.setPositionError({} as IPositionFormError))
        dispatch(PositionActionCreators.setError(""))
        dispatch(PositionActionCreators.setIsLoading(true))
        try {
            await PositionService.addPosition(position)
        }
        catch (e) {
            dispatch(PositionActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            const description = ae?.Description
            dispatch(PositionActionCreators.setPositionError({ name, description } as IPositionFormError))
            result = false
        }
        finally {
            dispatch(PositionActionCreators.setIsLoading(false))
            return result
        }
    },
    updatePosition: (position: IPosition) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(PositionActionCreators.setPositionError({} as IPositionFormError))
        dispatch(PositionActionCreators.setError(""))
        dispatch(PositionActionCreators.setIsLoading(true))
        try {
            await PositionService.updatePosition(position)
        }
        catch (e) {
            dispatch(PositionActionCreators.setError((e as Error).message))
            const ae = ((e as AxiosError).response!.data as any).errors
            const name = ae?.Name
            const description = ae?.Description
            dispatch(PositionActionCreators.setPositionError({ name, description } as IPositionFormError))
            result = false
        }
        finally {
            dispatch(PositionActionCreators.setIsLoading(false))
            return result
        }
    },
    deletePosition: (id: number) => async (dispatch: AppDispatch) => {
        let result = true
        dispatch(PositionActionCreators.setError(""))
        dispatch(PositionActionCreators.setIsLoading(true))
        try {
            await PositionService.deletePosition(id)
        }
        catch (e) {
            dispatch(PositionActionCreators.setError((e as Error).message))
            result = false
        }
        finally {
            dispatch(PositionActionCreators.setIsLoading(false))
            return result
        }
    }
}