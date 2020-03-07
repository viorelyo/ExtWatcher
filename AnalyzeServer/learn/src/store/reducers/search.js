import { SEARCH_FILES } from "../actions/search";
import { SUCCESS } from "../actions";

export default function(state = {}, action) {
  switch (action.type) {
    case SEARCH_FILES[SUCCESS]:
      return reduceSearchFiles(action.response, action.searchQuery, state);
    default:
      return state;
  }
}

function reduceSearchFiles(response, searchQuery, prevState) {
  let searchResults = response.files;
  if (prevState.query === searchQuery) {
    const prevResults = prevState.results || [];
    searchResults = prevResults.concat(searchResults);
  }
  return {
    query: searchQuery,
    results: searchResults
  };
}

export const getSearchResults = state => state.search.results;
