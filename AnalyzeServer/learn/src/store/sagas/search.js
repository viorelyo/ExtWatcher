import { fork, take } from "redux-saga/effects";
import * as searchActions from "../actions/search";
import { REQUEST } from "../actions";
import { fetchEntity } from "./index";

export function* watchSearchFiles() {
  while (true) {
    const { searchQuery } = yield take(searchActions.SEARCH_FILES[REQUEST]);
    yield fork(workerSearchFiles, searchQuery);
  }
}

export function* workerSearchFiles(searchQuery) {
  // var params = { filename: searchQuery };
  // var url = new URL("/search-files");
  // url.search = new URLSearchParams(params).toString();
  // const request = getSearchFiles.bind(searchQuery);
  var url = "/search-files?search_query=" + searchQuery;

  const request = () => {
    return fetch(url).then(response => response.json());
  };

  yield fetchEntity(request, searchActions.searchFiles, searchQuery);
}
