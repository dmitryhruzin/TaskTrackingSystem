import { AxiosResponse } from "axios"
import { IUserProject } from "../models/IUserProject"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class UserProjectService {
    static getUserProjects = async (): Promise<AxiosResponse<IUserProject[]>> => {
        return axiosWithJwt.get(url + "/api/userprojects")
    }
    static getUserProject = async (id: number): Promise<AxiosResponse<IUserProject>> => {
        return axiosWithJwt.get(url + `/api/userprojects/${id}`)
    }
    static addUserProjects = async (userProjects: IUserProject[]): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/userprojects`, userProjects)
    }
    static addUserProject = async (userProject: IUserProject): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/userprojects/project`, userProject)
    }
    static updateUserProject = async (userProject: IUserProject): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/userprojects/project`, userProject)
    }
    static deleteUserProjects = async (ids: number[]): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/userprojects`, { params: { ids } })
    }
    static deleteUserProject = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/userprojects/project/${id}`)
    }
}