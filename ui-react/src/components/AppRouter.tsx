import { FC } from "react"
import { Navigate, Route, Routes } from "react-router-dom"
import { useTypedSelector } from "../hooks/useTypedSelector"
import { RoleNames } from "../models/IRole"
import { adminRoutes, anonRoutes, managerRoutes, RouteNames, userManagerCommonRoutes, userRoutes } from "../router"
import Header from "./Header"


const AppRouter: FC = () => {
    const { isAuth, roles } = useTypedSelector(state => state.auth)
    return (
        isAuth
            ?
            <>
                <Header />

                <Routes>
                    {
                        roles.find(t => t.name == RoleNames.MANAGER)
                        &&

                        managerRoutes.map(route =>
                            <Route path={route.path}
                                element={<route.element />}
                                key={route.path}
                            />
                        )
                    }
                    {
                        roles.find(t => t.name == RoleNames.USER)
                        &&

                        userRoutes.map(route =>
                            <Route path={route.path}
                                element={<route.element />}
                                key={route.path}
                            />
                        )
                    }
                    {
                        roles.find(t => t.name == RoleNames.ADMINISTRATOR)
                        &&

                        adminRoutes.map(route =>
                            <Route path={route.path}
                                element={<route.element />}
                                key={route.path}
                            />
                        )
                    }
                    <Route
                        key={1}
                        path="*"
                        element={<Navigate to={RouteNames.HOME}
                            replace />}
                    />
                </Routes>
            </>
            :
            <Routes>
                {anonRoutes.map(route =>
                    <Route path={route.path}
                        element={<route.element />}
                        key={route.path}
                    />
                )}
                <Route
                    key={2}
                    path="*"
                    element={<Navigate to={RouteNames.LOGIN}
                        replace />}
                />
            </Routes>
    )
}

export default AppRouter