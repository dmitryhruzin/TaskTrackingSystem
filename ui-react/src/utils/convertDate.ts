export const convertDate = (date: Date) => {
  return new Date(date).toLocaleDateString('en-CA')
}
export const convertDateWithTime = (date: Date) => {
  return new Date(date).toLocaleString()
}