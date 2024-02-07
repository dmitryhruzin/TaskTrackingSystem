export interface IStatus {
    id: number
    name: string
}

export enum StatusNames {
    NOT_STARTED = "Not started",
    IN_PROGRESS = "In progress",
    FINISHED = "Finished"
}

export const arrayStatus : string[] = [
    StatusNames.NOT_STARTED,
    StatusNames.IN_PROGRESS,
    StatusNames.FINISHED
]