import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import * as searchActions from "../../store/actions/search";
import { getSearchResults } from "../../store/reducers/search";
import { getSearchParam } from "../../services/url";

class Search extends React.Component {
  render() {
    return <div>TODO</div>;
  }

  componentDidMount() {
    if (!this.getSearchQuery()) {
      this.props.history.push("/");
    }
    this.searchFiles();
  }

  getSearchQuery() {
    return getSearchParam(this.props.location, "search_query");
  }

  searchFiles() {
    const searchQuery = this.getSearchQuery();
    this.props.searchFiles(searchQuery);
  }
}

function mapDispatchToProps(dispatch) {
  const searchFiles = searchActions.searchFiles.request;
  return bindActionCreators({ searchFiles }, dispatch);
}

function mapStateToProps(state) {
  return {
    searchResults: getSearchResults(state)
  };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Search));
