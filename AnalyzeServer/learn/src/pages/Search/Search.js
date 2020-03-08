import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import * as searchActions from "../../store/actions/search";
import { getSearchResults } from "../../store/reducers/search";
import { getSearchParam } from "../../services/url";
import { Loader, Header, Divider, Segment, Icon } from "semantic-ui-react";

import FilesTable from "../../components/FilesTable/FilesTable";

class Search extends React.Component {
  render() {
    const result = this.checkQuery() ? this.props.searchResults : [];

    let content;
    if (!this.checkQuery()) {
      content = (
        <Segment placeholder>
          <Header icon>
            <Icon name="search" />
            Your query is invalid.
          </Header>
        </Segment>
      );
    } else if (this.resultsFound()) {
      content = <FilesTable files={result} />;
    } else {
      content = (
        <Segment placeholder>
          <Header icon>
            <Icon name="search" />
            We don't have any files matching your query.
          </Header>
        </Segment>
      );
    }

    return (
      <div>
        <Loader active={this.shouldShowLoader()} size="large">
          Loading
        </Loader>

        <Header size="huge" content="Search results" />
        <Divider />

        {content}
      </div>
    );
  }

  componentDidMount() {
    if (!this.getSearchQuery()) {
      this.props.history.push("/stats");
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

  shouldShowLoader() {
    if (!this.props.searchResults) {
      if (this.checkQuery()) return true;
    }
    return false;
  }

  resultsFound() {
    if (this.props.searchResults) {
      if (this.checkQuery()) {
        return !(this.props.searchResults.length === 0);
      }
    }
    return true;
  }

  checkQuery() {
    if (this.props.searchResults === "Invalid query") {
      return false;
    }
    return true;
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
