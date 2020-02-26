import { all, call, put, fork } from "redux-saga/effects";
import { watchAllFiles } from "./file";

export default function*() {
  yield all([fork(watchAllFiles)]);
}

export function* fetchEntity(request, entity, ...args) {
  try {
    const response = yield call(request);

    yield put(entity.success(response, ...args));
  } catch (error) {
    yield put(entity.failure(error, ...args));
  }
}
