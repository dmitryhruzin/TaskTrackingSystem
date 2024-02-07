import { AxiosResponse } from "axios"
import { IStatus } from "../models/IStatus"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class ProjectStatusService {
    static getStatuses = async (): Promise<AxiosResponse<IStatus[]>> => {
        return axiosWithJwt.get(url + "/api/projects/statuses")
    }
    static getStatus = async (id: number): Promise<AxiosResponse<IStatus>> => {
        return axiosWithJwt.get(url + `/api/projects/statuses/${id}`)
    }
    static addStatus = async (status: IStatus): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/projects/statuses`, status)
    }
    static updateStatus = async (status: IStatus): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/projects/statuses`, status)
    }
    static deleteStatus = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/projects/statuses/${id}`)
    }
}