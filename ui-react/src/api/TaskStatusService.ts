import { AxiosResponse } from "axios"
import { IStatus } from "../models/IStatus"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class TaskStatusService {
    static getStatuses = async (): Promise<AxiosResponse<IStatus[]>> => {
        return axiosWithJwt.get(url + "/api/tasks/statuses")
    }
    static getStatus = async (id: number): Promise<AxiosResponse<IStatus>> => {
        return axiosWithJwt.get(url + `/api/tasks/statuses/${id}`)
    }
    static addStatus = async (status: IStatus): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/tasks/statuses`, status)
    }
    static updateStatus = async (status: IStatus): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/tasks/statuses`, status)
    }
    static deleteStatus = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/tasks/statuses/${id}`)
    }
}