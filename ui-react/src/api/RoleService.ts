import axios, { AxiosResponse } from "axios"
import { IRole } from "../models/IRole"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class RoleService {
    static getRoles = async (): Promise<AxiosResponse<IRole[]>> => {
        return axiosWithJwt.get(url + "/api/users/roles")
    }
    static getRolesByUserId = async (id: number): Promise<AxiosResponse<IRole[]>> => {
        return axiosWithJwt.get(url + `/api/users/${id}/roles`)
    }
    static getRole = async (id: number): Promise<AxiosResponse<IRole>> => {
        return axiosWithJwt.get(url + `/api/users/roles/${id}`)
    }
    static addRole = async (role: IRole): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/users/roles`, role)
    }
    static addToRole = async (id: number, role: IRole): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/users/${id}/role`, role)
    }
    static addToRoles = async (id: number, roles: IRole[]): Promise<AxiosResponse> => {
        return axiosWithJwt.post(url + `/api/users/${id}/roles`, roles)
    }
    static updateRole = async (role: IRole): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/users/roles`, role)
    }
    static deleteRole = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/users/roles/${id}`)
    }
    static deleteToRole = async (id: number, name: string): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/users/${id}/role`, { params: { name } })
    }
    static deleteToRoles = async (id: number, names: string[]): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/users/${id}/roles`, { params: { names } })
    }
}