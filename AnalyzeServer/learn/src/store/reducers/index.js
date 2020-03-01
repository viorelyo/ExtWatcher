import { combineReducers } from "redux";
import fileReducer from "./file";
import feedReducer from "./feed";

export default combineReducers({
  file: fileReducer,
  feed: feedReducer
});
