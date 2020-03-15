import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as fileActions from "../../store/actions/file";
import { getAllFiles } from "../../store/reducers/file";

import StatisticsContent from "./StatisticsContent/StatisticsContent";

class Statistics extends React.Component {
  render() {
    return <StatisticsContent showLoader={this.shouldShowLoader()} />;
  }

  componentDidMount() {
    this.props.fetchAllFiles();
  }

  shouldShowLoader() {
    if (!this.props.allFiles || !this.props.allFiles.length) {
      return true;
    }
    return false;
  }
}

function mapStateToProps(state) {
  return {
    allFiles: getAllFiles(state)
  };
}

function mapDispatchToProps(dispatch) {
  const fetchAllFiles = fileActions.allFiles.request;
  return bindActionCreators({ fetchAllFiles }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Statistics);
