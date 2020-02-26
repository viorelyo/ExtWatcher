import React from "react";
import { Statistic, Divider } from "semantic-ui-react";
import { getAllFiles } from "../../../store/reducers/file";
import { connect } from "react-redux";
import { FilesTable } from "../../../components/FilesTable/FilesTable";

class StatisticsContent extends React.Component {
  render() {
    const allFiles = this.getAllFiles();

    return (
      <div>
        <Statistic>
          <Statistic.Label>Files</Statistic.Label>
          <Statistic.Value>{allFiles.length}</Statistic.Value>
        </Statistic>

        <Divider />

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
