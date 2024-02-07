import { AxiosResponse } from "axios"
import { IPosition } from "../models/IPosition"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class PositionService {
    static getPositions = async (): Promise<AxiosResponse<IPosition[]>> => {
        return axiosWithJwt.get(url + "/api/userprojects/positions")
    }
    static getPositionsByUserId = async (id: number): Promise<AxiosResponse<IPosition[]>> => {
        return axiosWithJwt.get(url + `/api/users/${id}/positions`)
    }
    static getPosition = async (id: number): Promise<AxiosResponse<IPosition>> => {
        return axiosWithJwt.get(url + `/api/userprojects/positions/${id}`)
    }
    static addPosition = async (position: IPosition): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/userprojects/positions`, position)
    }
    static updatePosition = async (position: IPosition): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/userprojects/positions`, position)
    }
    static deletePosition = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/userprojects/positions/${id}`)
    }
}