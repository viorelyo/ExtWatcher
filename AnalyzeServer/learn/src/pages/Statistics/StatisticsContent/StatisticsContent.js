import React from "react";
import { Statistic, Divider, Dimmer, Loader } from "semantic-ui-react";
import { getAllFiles } from "../../../store/reducers/file";
import { connect } from "react-redux";
import { FilesTable } from "../../../components/FilesTable/FilesTable";

class StatisticsContent extends React.Component {
  render() {
    const allFiles = this.getAllFiles();

    return (
      <div>
        <Dimmer.Dimmable dimmed={this.props.showLoader}>
          <Dimmer active={this.props.showLoader} inverted>
            <Loader>Loading</Loader>
          </Dimmer>

          <Statistic>
            <Statistic.Label>Files</Statistic.Label>
            <Statistic.Value>{allFiles.length}</Statistic.Value>
          </Statistic>

          <Divider />

          <FilesTable files={allFiles} />
        </Dimmer.Dimmable>
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
