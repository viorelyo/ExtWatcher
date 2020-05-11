import React from "react";
import { Loader, Divider, Grid } from "semantic-ui-react";
import { getAllFiles } from "../../../store/reducers/file";
import { connect } from "react-redux";

import FilesTable from "../../../components/FilesTable/FilesTable";
import PieChartComponent from "../../../components/Charts/PieChartComponent";
import RadarChartComponent from "../../../components/Charts/RadarChartComponent";

class StatisticsContent extends React.Component {
  render() {
    const allFiles = this.getAllFiles();

    return (
      <div>
        <Loader active={this.props.showLoader} size="large">
          Loading
        </Loader>

        <Grid stackable>
          <Grid.Column width={8}>
            <PieChartComponent files={allFiles} />
          </Grid.Column>
          <Grid.Column width={8}>
            <RadarChartComponent files={allFiles} />
          </Grid.Column>
        </Grid>

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
    allFiles: getAllFiles(state),
  };
}

export default connect(mapStateToProps, null)(StatisticsContent);
