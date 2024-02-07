export interface ITask {
    id: number
    name: string
    description: string
    startDate: Date
    expiryDate: Date
    managerUserName?: string
    managerId?: number
    statusName: string
    statusId: number
    projectName: string
    projectId: number
    userProjectIds: number[]
}