import { takeEvery } from "redux-saga/effects";
import * as fileActions from "../actions/file";
import { REQUEST } from "../actions";
import { fetchEntity } from "./index";

function getAllFiles() {
  return fetch("/analyzed-files").then(response => response.json());
}

export function* watchAllFiles() {
  // while (true) {
  //   yield take(fileActions.ALL_FILES[REQUEST]);
  //   yield fork(workerAllFiles);
  // }
  yield takeEvery(fileActions.ALL_FILES[REQUEST], workerAllFiles);
}

export function* workerAllFiles() {
  const request = getAllFiles;
  yield fetchEntity(request, fileActions.allFiles);
}
