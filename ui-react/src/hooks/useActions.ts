import { useDispatch } from "react-redux";
import { ActionCreatorsMapObject, bindActionCreators } from "redux";


export const useActions = (action: ActionCreatorsMapObject) => {
    const dispatch = useDispatch();
    return bindActionCreators(action, dispatch);
}