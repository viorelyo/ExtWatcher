import {
  createAction,
  createRequestTypes,
  REQUEST,
  SUCCESS,
  FAILURE
} from "./index";

export const ALL_FILES = createRequestTypes("ALL_FILES");
export const allFiles = {
  request: () => createAction(ALL_FILES[REQUEST], {}),
  success: response => createAction(ALL_FILES[SUCCESS], { response }),
  failure: response => createAction(ALL_FILES[FAILURE], { response })
};
