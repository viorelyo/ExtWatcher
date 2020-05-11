import {
  createAction,
  createRequestTypes,
  REQUEST,
  SUCCESS,
  FAILURE
} from "./index";

export const ALL_FEED = createRequestTypes("ALL_FEED");
export const allFeed = {
  request: () => createAction(ALL_FEED[REQUEST], {}),
  success: response => createAction(ALL_FEED[SUCCESS], { response }),
  failure: response => createAction(ALL_FEED[FAILURE], { response })
};
