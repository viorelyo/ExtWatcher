import { ALL_FEED } from "../actions/feed";
import { SUCCESS } from "../actions";
import { createSelector } from "reselect";

const initialState = {
  feed: {}
};

export default function(state = initialState, action) {
  switch (action.type) {
    case ALL_FEED[SUCCESS]:
      return reduceWorkerAllFeed(action.response, state);
    default:
      return state;
  }
}

function reduceWorkerAllFeed(response, prevState) {
  return {
    ...prevState,
    feed: response.feed
  };
}

export const getAllFeed = createSelector(
  state => state.feed,
  allFeed => allFeed.feed
);
