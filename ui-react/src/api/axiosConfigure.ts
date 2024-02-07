import axios, { AxiosRequestConfig } from "axios";
import { IRole } from "../models/IRole";
import { IToken } from "../models/IToken";
import { IUser } from "../models/IUser";
import { store } from "../store";
import { AuthActionCreators } from "../store/reduce/auth/action-creators";
import TokenService from "./TokenService";


axios.interceptors.request.use(
    async (config: AxiosRequestConfig) => {
        const accessToken = localStorage.getItem("accessToken");
        if (accessToken) {
            config.headers!.Authorization = `Bearer ${accessToken}`;
        }

        return config;
    },
    (error) => Promise.reject(error)
);

axios.interceptors.response.use(
    (response) => response,
    async (error) => {
        const config = error?.config;

        if (error?.response?.status === 401 && !config?.sent) {
            try {
                config.sent = true;

                const refreshToken = localStorage.getItem('refreshToken')
                const accessToken = localStorage.getItem('accessToken')
                const response = await TokenService.refreshToken({ accessToken, refreshToken } as IToken)
                const responseToken = response.data

                if (responseToken.accessToken) {
                    localStorage.setItem('accessToken', responseToken.accessToken)
                    localStorage.setItem('refreshToken', responseToken.refreshToken)
                    config.headers = {
                        ...config.headers,
                        authorization: `Bearer ${responseToken.accessToken}`,
                    };
                    return axios(config);
                }
            }
            catch (e) {
                localStorage.removeItem('auth')
                localStorage.removeItem('login')
                localStorage.removeItem('accessToken')
                localStorage.removeItem('refreshToken')
                localStorage.removeItem('id')
                store.dispatch(AuthActionCreators.setIsAuth(false))
                store.dispatch(AuthActionCreators.setUser({} as IUser))
                store.dispatch(AuthActionCreators.setToken({} as IToken))
                store.dispatch(AuthActionCreators.setRoles([] as IRole[]))
                return Promise.reject(e)
            }
        }
        return Promise.reject(error);
    }
);

export const axiosWithJwt = axios

export const url = "https://localhost:7215"
