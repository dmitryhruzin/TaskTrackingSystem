import axios, { AxiosResponse } from "axios";
import { IRegisterUser } from "../models/IRegisterUser";
import { IUpdateUser } from "../models/IUpdateUser";
import { IUser } from "../models/IUser";
import { axiosWithJwt, url } from "./axiosConfigure";


export default class UserService {
    static getUsers = async (): Promise<AxiosResponse<IUser[]>> => {
        return axiosWithJwt.get(url + "/api/users")
    }
    static getUsersByProjectId = async (id: number): Promise<AxiosResponse<IUser[]>> => {
        return axiosWithJwt.get(url + `/api/projects/${id}/users`)
    }
    static getUsersByTaskId = async (id: number): Promise<AxiosResponse<IUser[]>> => {
        return axiosWithJwt.get(url + `/api/tasks/${id}/users`)
    }
    static getUser = async (id: number): Promise<AxiosResponse<IUser>> => {
        return axiosWithJwt.get(url + `/api/users/${id}`)
    }
    static getUserByEmail = async (email: string): Promise<AxiosResponse<IUser>> => {
        return axiosWithJwt.get(url + `/api/users/email/${email}`)
    }
    static addUser = async (user: IRegisterUser): Promise<AxiosResponse> => {
        return axios.post(url + `/api/users`, user)
    }
    static updateUser = async (user: IUpdateUser): Promise<AxiosResponse> => {
        return axiosWithJwt.put(url + `/api/users`, user)
    }
    static deleteUser = async (id: number): Promise<AxiosResponse> => {
        return axiosWithJwt.delete(url + `/api/users/${id}`)
    }
}