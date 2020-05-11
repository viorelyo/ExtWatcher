import { takeEvery } from "redux-saga/effects";
import * as feedActions from "../actions/feed";
import { REQUEST } from "../actions";
import { fetchEntity } from "./index";

function getAllFeed() {
  return fetch("/feed").then(response => response.json());
}

export function* watchAllFeed() {
  yield takeEvery(feedActions.ALL_FEED[REQUEST], workerAllFeed);
}

export function* workerAllFeed() {
  const request = getAllFeed;
  yield fetchEntity(request, feedActions.allFeed);
}
