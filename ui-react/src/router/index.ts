import AddPosition from "../pages/AddPosition"
import AddProject from "../pages/AddProject"
import AddRole from "../pages/AddRole"
import AddTask from "../pages/AddTask"
import AddUserProject from "../pages/AddUserProject"
import Home from "../pages/Home"
import Login from "../pages/Login"
import MyPositions from "../pages/MyPositions"
import MyProjects from "../pages/MyProjects"
import MyTasks from "../pages/MyTasks"
import Positions from "../pages/Positions"
import Project from "../pages/Project"
import Projects from "../pages/Projects"
import Registration from "../pages/Registration"
import Roles from "../pages/Roles"
import Task from "../pages/Task"
import Tasks from "../pages/Tasks"
import UpdatePosition from "../pages/UpdatePosition"
import UpdateProject from "../pages/UpdateProject"
import UpdateRole from "../pages/UpdateRole"
import UpdateTask from "../pages/UpdateTask"
import UpdateUser from "../pages/UpdateUser"
import UpdateUserProject from "../pages/UpdateUserProject"
import User from "../pages/User"
import UserProjects from "../pages/UserProjects"
import Users from "../pages/Users"
import TaskStatuses from "../pages/TaskStatuses"
import ProjectStatuses from "../pages/ProjectStatuses"
import AddProjectStatus from "../pages/AddProjectStatus"
import UpdateProjectStatus from "../pages/UpdateProjectStatus"
import AddTaskStatus from "../pages/AddTaskStatus"
import UpdateTaskStatus from "../pages/UpdateTaskStatus"
import ProjectTasks from "../pages/ProjectTasks"
import ProjectUsers from "../pages/ProjectUsers"
import TaskUsers from "../pages/TaskUsers"
import MyRoles from "../pages/MyRoles"
import ManagerTasks from "../pages/ManagerTasks"
import MyProfile from "../pages/MyProfile"
import AddUserToRole from "../pages/AddUserToRole"
import DeleteUserToRole from "../pages/DeleteUserToRole"
import AddUser from "../pages/AddUser"
import ForgotPassword from "../pages/ForgotPassword"
import ResetPassword from "../pages/ResetPassword"

export interface IRoute {
    path: string
    element: React.ComponentType
}

export enum RouteNames {
    ERROR_PAGE = "error",
    LOGIN = "login",
    FORGOT_PASSWORD = "forgot",
    RESET_PASSWORD = "reset/*",
    HOME = "",
    MY_PROJECTS = "user/projects/:id",
    MY_TASKS = "user/tasks/:id",
    MY_POSITIONS = "user/positions/:id",
    MY_ROLES = "user/roles/:id",
    MY_PROFILE = "user/:id",

    UPDATE_PROJECT = "update/projects/:id",
    UPDATE_PROJECT_STATUS = "update/project/status/:id",
    ADD_PROJECT = "add/project",
    ADD_PROJECT_STATUS = "add/project/status",
    PROJECT = "projects/:id",
    PROJECTS = "projects",
    PROJECT_TASKS = "project/:id/tasks",
    PROJECT_USERS = "project/:id/users",
    PROJECT_STATUSES = "project/statuses",

    ADD_TASK = "add/task/:id",
    ADD_TASK_STATUS = "add/task/status",
    UPDATE_TASK = "update/task/:id",
    UPDATE_TASK_STATUS = "update/task/status/:id",
    TASK = "tasks/:id",
    TASKS = "tasks",
    TASK_USERS = "task/:id/users",
    TASK_STATUSES = "task/statuses",
    UPDATE_STATUS_FOR_PROJECT_OR_TASK = ":name/:id/update/status",

    USER_PROJECTS = "task/:id/user/projects",
    ADD_USER_PROJECT = "task/:id/add/user/project",
    UPDATE_USER_PROJECT = "update/user/project/:id",

    POSITIONS = "positions",
    ADD_POSITION = "add/position",
    UPDATE_POSITION = "update/position/:id",

    USER = "users/:id",
    ADD_USER = "add/user",
    USERS = "users",
    REGISTRATION = "registration",
    UPDATE_USER = "update/user/:id",
    MANAGER_TASKS = "user/manager/:id/tasks",

    ROLES = "roles",
    ADD_ROLE = "add/role",
    ADD_USER_TO_ROLE = "user/:id/add/role",
    UPDATE_ROLE = "update/role/:id",
    DELETE_USER_TO_ROLE = "user/:id/delete/role",
}

export const anonRoutes: IRoute[] = [
    { path: RouteNames.LOGIN, element: Login },
    { path: RouteNames.FORGOT_PASSWORD, element: ForgotPassword },
    { path: RouteNames.RESET_PASSWORD, element: ResetPassword },
    { path: RouteNames.REGISTRATION, element: Registration },
]

export const commonRoutes: IRoute[] = [
    { path: RouteNames.USER, element: User },
    { path: RouteNames.MY_PROFILE, element: MyProfile },
    { path: RouteNames.UPDATE_USER, element: UpdateUser },
    { path: RouteNames.MY_ROLES, element: MyRoles },
    { path: RouteNames.HOME, element: Home },
]

export const adminRoutes: IRoute[] = [
    ...commonRoutes,
    { path: RouteNames.ADD_ROLE, element: AddRole },
    { path: RouteNames.ADD_USER_TO_ROLE, element: AddUserToRole },
    { path: RouteNames.DELETE_USER_TO_ROLE, element: DeleteUserToRole },
    { path: RouteNames.UPDATE_ROLE, element: UpdateRole },
    { path: RouteNames.ROLES, element: Roles },
    { path: RouteNames.USERS, element: Users },
    { path: RouteNames.ADD_USER, element: AddUser },
]

export const userManagerCommonRoutes: IRoute[] = [
    ...commonRoutes,
    { path: RouteNames.PROJECT, element: Project },
    { path: RouteNames.TASK, element: Task },
    { path: RouteNames.PROJECT_TASKS, element: ProjectTasks },
    { path: RouteNames.PROJECT_USERS, element: ProjectUsers },
    { path: RouteNames.TASK_USERS, element: TaskUsers },
]

export const userRoutes: IRoute[] = [
    ...userManagerCommonRoutes,
    { path: RouteNames.MY_PROJECTS, element: MyProjects },
    { path: RouteNames.MY_TASKS, element: MyTasks },
    { path: RouteNames.MY_POSITIONS, element: MyPositions },
]

export const managerRoutes: IRoute[] = [
    ...userManagerCommonRoutes,
    { path: RouteNames.TASKS, element: Tasks },
    { path: RouteNames.MANAGER_TASKS, element: ManagerTasks },
    { path: RouteNames.POSITIONS, element: Positions },
    { path: RouteNames.PROJECTS, element: Projects },
    { path: RouteNames.ADD_PROJECT, element: AddProject },
    { path: RouteNames.UPDATE_PROJECT, element: UpdateProject },
    { path: RouteNames.ADD_TASK, element: AddTask },
    { path: RouteNames.UPDATE_TASK, element: UpdateTask },
    { path: RouteNames.ADD_POSITION, element: AddPosition },
    { path: RouteNames.UPDATE_POSITION, element: UpdatePosition },
    { path: RouteNames.TASK_STATUSES, element: TaskStatuses },
    { path: RouteNames.PROJECT_STATUSES, element: ProjectStatuses },
    { path: RouteNames.ADD_PROJECT_STATUS, element: AddProjectStatus },
    { path: RouteNames.UPDATE_PROJECT_STATUS, element: UpdateProjectStatus },
    { path: RouteNames.ADD_TASK_STATUS, element: AddTaskStatus },
    { path: RouteNames.UPDATE_TASK_STATUS, element: UpdateTaskStatus },
    { path: RouteNames.USER_PROJECTS, element: UserProjects },
    { path: RouteNames.ADD_USER_PROJECT, element: AddUserProject },
    { path: RouteNames.UPDATE_USER_PROJECT, element: UpdateUserProject },
]