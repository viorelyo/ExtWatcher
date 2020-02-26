import React from "react";
import {} from "../../store/reducers/file";
import * as fileActions from "../../store/actions/file";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { getAllFiles } from "../../store/reducers/file";

import StatisticsContent from "./StatisticsContent/StatisticsContent";

export class Statistics extends React.Component {
  render() {
    return (
      <React.Fragment>
        <StatisticsContent showLoader={this.shouldShowLoader()} />
      </React.Fragment>
    );
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
