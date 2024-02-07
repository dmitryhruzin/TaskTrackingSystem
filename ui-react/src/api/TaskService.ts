import { AxiosResponse } from "axios"
import { IStatus } from "../models/IStatus"
import { ITask } from "../models/ITask"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class TaskService {
    static getTasks = async (): Promise<AxiosResponse<ITask[]>> => {
        return axiosWithJwt.get(url + "/api/tasks")
    }
    static getTasksByUserId = async (id: number): Promise<AxiosResponse<ITask[]>> => {
        return axiosWithJwt.get(url + `/api/users/${id}/tasks`)
    }
    static getTasksByManagerId = async (id: number): Promise<AxiosResponse<ITask[]>> => {
        return axiosWithJwt.get(url + `/api/users/${id}/manager/tasks`)
    }
    static getTasksByProjectId = async (id: number): Promise<AxiosResponse<ITask[]>> => {
        return axiosWithJwt.get(url + `/api/projects/${id}/tasks`)
    }
    static getTask = async (id: number): Promise<AxiosResponse<ITask>> => {
        return axiosWithJwt.get(url + `/api/tasks/${id}`)
    }
    static addTask = async (task: ITask): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/tasks`, task)
    }
    static updateTask = async (task: ITask): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/tasks`, task)
    }
    static updateTaskStatus = async (id: number, status: IStatus): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/tasks/${id}/status`, status)
    }
    static deleteTask = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/tasks/${id}`)
    }
}