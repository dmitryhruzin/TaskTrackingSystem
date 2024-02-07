import { AxiosResponse } from "axios"
import { IProject } from "../models/IPoject"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class ProjectService {
    static getProjects = async (): Promise<AxiosResponse<IProject[]>> => {
        return axiosWithJwt.get(url + "/api/projects")
    }
    static getProjectsByUserId = async (id: number): Promise<AxiosResponse<IProject[]>> => {
        return axiosWithJwt.get(url + `/api/users/${id}/projects`)
    }
    static getProject = async (id: number): Promise<AxiosResponse<IProject>> => {
        return axiosWithJwt.get(url + `/api/projects/${id}`)
    }
    static addProject = async (project: IProject): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/projects`, project)
    }
    static updateProject = async (project: IProject): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/projects`, project)
    }
    static deleteProject = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/projects/${id}`)
    }
}