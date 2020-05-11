import { ALL_FILES } from "../actions/file";
import { SUCCESS } from "../actions";
import { createSelector } from "reselect";

const initialState = {
  files: {}
};

export default function(state = initialState, action) {
  switch (action.type) {
    case ALL_FILES[SUCCESS]:
      return reduceWorkerAllFiles(action.response, state);
    default:
      return state;
  }
}

function reduceWorkerAllFiles(response, prevState) {
  return {
    ...prevState,
    files: response.files
  };
}

export const getAllFiles = createSelector(
  state => state.file,
  allFiles => allFiles.files
);
