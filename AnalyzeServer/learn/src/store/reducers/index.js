import { combineReducers } from "redux";
import fileReducer from "./file";
import feedReducer from "./feed";
import searchReducer from "./search";

export default combineReducers({
  file: fileReducer,
  feed: feedReducer,
  search: searchReducer
});
