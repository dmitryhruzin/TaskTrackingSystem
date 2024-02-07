export const contains = (arr: any, elem: number) => {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].id === elem) {
            return true;
        }
    }
    return false;
}