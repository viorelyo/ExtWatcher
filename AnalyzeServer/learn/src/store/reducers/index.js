import { combineReducers } from "redux";
import fileReducer from "./file";

export default combineReducers({
  file: fileReducer
});
