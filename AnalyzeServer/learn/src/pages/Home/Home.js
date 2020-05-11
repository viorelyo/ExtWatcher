import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as feedActions from "../../store/actions/feed";
import { getAllFeed } from "../../store/reducers/feed";

import HomeContent from "./HomeContent/HomeContent";

class Home extends React.Component {
  render() {
    return <HomeContent showLoader={this.shouldShowLoader()} />;
  }

  componentDidMount() {
    this.props.fetchAllFeed();
  }

  shouldShowLoader() {
    if (!this.props.allFeed || !this.props.allFeed.length) {
      return true;
    }
    return false;
  }
}

function mapStateToProps(state) {
  return {
    allFeed: getAllFeed(state)
  };
}

function mapDispatchToProps(dispatch) {
  const fetchAllFeed = feedActions.allFeed.request;
  return bindActionCreators({ fetchAllFeed }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Home);
