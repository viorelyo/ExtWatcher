import {
  createAction,
  createRequestTypes,
  REQUEST,
  SUCCESS,
  FAILURE
} from "./index";

export const SEARCH_FILES = createRequestTypes("SEARCH_FILES");
export const searchFiles = {
  request: searchQuery => createAction(SEARCH_FILES[REQUEST], { searchQuery }),
  success: (response, searchQuery) =>
    createAction(SEARCH_FILES[SUCCESS], { response, searchQuery }),
  failure: (response, searchQuery) =>
    createAction(SEARCH_FILES[FAILURE], { response, searchQuery })
};
