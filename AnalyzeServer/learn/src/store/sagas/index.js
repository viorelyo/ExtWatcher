import { all, call, put, fork } from "redux-saga/effects";
import { watchAllFiles } from "./file";
import { watchAllFeed } from "./feed";
import { watchSearchFiles } from "./search";

export default function*() {
  yield all([fork(watchAllFiles), fork(watchAllFeed), fork(watchSearchFiles)]);
}

export function* fetchEntity(request, entity, ...args) {
  try {
    const response = yield call(request);

    yield put(entity.success(response, ...args));
  } catch (error) {
    yield put(entity.failure(error, ...args));
  }
}
