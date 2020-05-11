import React from "react";
import { Table, Icon } from "semantic-ui-react";

export function CellType(props) {
  if (props.type === "benign") {
    return (
      <Table.Cell positive collapsing textAlign="center">
        <Icon name="checkmark" />
        benign
      </Table.Cell>
    );
  } else {
    return (
      <Table.Cell error collapsing textAlign="center">
        <Icon name="attention" />
        malicious
      </Table.Cell>
    );
  }
}
