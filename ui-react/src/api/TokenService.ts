import axios, { AxiosResponse } from "axios"
import { IToken } from "../models/IToken"
import { axiosWithJwt, url } from "./axiosConfigure"


export default class TokenService {
    static refreshToken = async (token: IToken): Promise<AxiosResponse<IToken>> => {
        return axios.post(url + `/api/tokens/refresh`, token)
    }
    static async revokeToken(token: IToken): Promise<AxiosResponse> {
        return axiosWithJwt.post(url + `/api/tokens/revoke`, token)
    }
}