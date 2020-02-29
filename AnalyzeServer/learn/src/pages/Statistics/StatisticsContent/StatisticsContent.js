import React from "react";
import { Loader } from "semantic-ui-react";
import { getAllFiles } from "../../../store/reducers/file";
import { connect } from "react-redux";
import "./StatisticsContent.scss";

import FilesTable from "../../../components/FilesTable/FilesTable";
import FileCounter from "../../../components/FileCounter/FileCounter";

class StatisticsContent extends React.Component {
  render() {
    const allFiles = this.getAllFiles();

    return (
      <div>
        <Loader active={this.props.showLoader} size="large">
          Loading
        </Loader>

        <FileCounter files={allFiles} />
        <FilesTable files={allFiles} />
      </div>
    );
  }

  getAllFiles() {
    return this.props.allFiles;
  }
}

function mapStateToProps(state) {
  return {
    allFiles: getAllFiles(state)
  };
}

export default connect(mapStateToProps, null)(StatisticsContent);
