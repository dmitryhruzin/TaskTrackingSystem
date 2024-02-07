export interface IProject {
    id: number
    name: string
    description: string
    startDate: Date
    expiryDate: Date
    statusName: string
    statusId: number
    taskIds: number[]
}