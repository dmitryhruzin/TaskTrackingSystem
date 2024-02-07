import axios, { AxiosResponse } from "axios";
import { IForgotPassword } from "../models/IForgotPassword";
import { ILogin } from "../models/ILogin";
import { IResetPassword } from "../models/IResetPassword";
import { IToken } from "../models/IToken";
import { url } from "./axiosConfigure";

export default class LoginService {
    static async login(user: ILogin): Promise<AxiosResponse<IToken>> {
        return axios.post<IToken>(url + '/api/auth', user)
    }
    static async forgotPassword(model: IForgotPassword): Promise<AxiosResponse> {
        return axios.post(url + '/api/auth/forgot', model)
    }
    static async resetPassword(model: IResetPassword): Promise<AxiosResponse> {
        return axios.post(url + '/api/auth/reset', model)
    }
}