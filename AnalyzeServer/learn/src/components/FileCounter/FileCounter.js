import React from "react";
import { Statistic, Divider } from "semantic-ui-react";

function FilesTable(props) {
  if (!props.files || !props.files.length) {
    return <div />;
  }

  return (
    <div>
      <Statistic>
        <Statistic.Label>Files</Statistic.Label>
        <Statistic.Value>{props.files.length}</Statistic.Value>
      </Statistic>

      <Divider />
    </div>
  );
}

export default FilesTable;
