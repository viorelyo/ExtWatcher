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
    let content;
    if (this.searchFound()) {
      content = <FilesTable files={this.props.searchResults} />;
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
      return true;
    }
    return false;
  }

  searchFound() {
    if (this.props.searchResults) {
      return !(this.props.searchResults.length === 0);
    }
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
