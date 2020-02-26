import React from "react";
import {} from "../../store/reducers/file";
import * as fileActions from "../../store/actions/file";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";

import StatisticsContent from "./StatisticsContent/StatisticsContent";

export class Statistics extends React.Component {
  render() {
    return (
      <React.Fragment>
        <StatisticsContent />
      </React.Fragment>
    );
  }

  componentDidMount() {
    this.props.fetchAllFiles();
  }
}

function mapDispatchToProps(dispatch) {
  const fetchAllFiles = fileActions.allFiles.request;
  return bindActionCreators({ fetchAllFiles }, dispatch);
}

export default connect(null, mapDispatchToProps)(Statistics);
