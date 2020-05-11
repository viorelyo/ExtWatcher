import React, { Component } from "react";
import { Table, Image } from "semantic-ui-react";
import "./FilesTable.scss";

import { CellType } from "./ResultCellType/CellType";
import { BonusFileInfo } from "./BonusFileInfo";

import pdf from "../../assets/images/pdf.svg";

class FilesTable extends Component {
  constructor(props) {
    super(props);

    this.state = {
      expandedRows: [],
    };
  }

  handleRowClick(rowId) {
    const currentExpandedRows = this.state.expandedRows;
    const isRowCurrentlyExpanded = currentExpandedRows.includes(rowId);

    const newExpandedRows = isRowCurrentlyExpanded
      ? currentExpandedRows.filter((id) => id !== rowId)
      : currentExpandedRows.concat(rowId);

    this.setState({ expandedRows: newExpandedRows });
  }

  renderItemDetails(file) {
    return <BonusFileInfo file={file} />;
  }

  renderItem(file, index) {
    const clickCallback = () => this.handleRowClick(index);

    let type;
    switch (file.filetype) {
      case "pdf":
        type = pdf;
        break;
      default:
        type = pdf;
        break;
      //TODO new supported filetypes will be added here
    }

    const itemRows = [
      <Table.Row onClick={clickCallback} key={"row-data-" + index}>
        <Table.Cell collapsing>
          <Image src={type} size="mini" centered />
        </Table.Cell>
        <Table.Cell collapsing textAlign="center">
          {file.file_hash}
        </Table.Cell>
        <Table.Cell textAlign="center">{file.filename}</Table.Cell>
        <Table.Cell textAlign="center">{file.datetime}</Table.Cell>
        <CellType type={file.result} />
      </Table.Row>,
    ];

    if (this.state.expandedRows.includes(index)) {
      itemRows.push(
        <Table.Row key={"row-expanded-" + index}>
          <Table.Cell colSpan="5">{this.renderItemDetails(file)}</Table.Cell>
        </Table.Row>
      );
    }

    return itemRows;
  }

  render() {
    if (!this.props.files || !this.props.files.length) {
      return <div />;
    }

    let allItemRows = [];
    this.props.files.forEach((file, index) => {
      const perItemRows = this.renderItem(file, index);
      allItemRows = allItemRows.concat(perItemRows);
    });

    return (
      <Table celled selectable>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell textAlign="center">Filetype</Table.HeaderCell>
            <Table.HeaderCell textAlign="center">MD5</Table.HeaderCell>
            <Table.HeaderCell textAlign="center">Filename</Table.HeaderCell>
            <Table.HeaderCell textAlign="center">Date</Table.HeaderCell>
            <Table.HeaderCell textAlign="center">Result</Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        {/* ======= Buggy implementation of Semantic-UI Library. Maybe later will be fixed. ====== */}

        {/* <Accordion
        fluid={true}
        as={Table.Body}
        panels={this.props.files.map((file) => {
          let type;
          switch (file.filetype) {
            case "pdf":
              type = pdf;
              break;
            default:
              type = pdf;
              break;
            //TODO new supported filetypes will be added here
          }

          return {
            key: file.file_hash,
            title: {
              as: Table.Row,
              children: [
                <Table.Cell collapsing>
                  <Image src={type} size="mini" centered />
                </Table.Cell>,
                <Table.Cell collapsing textAlign="center">
                  {file.file_hash}
                </Table.Cell>,
                <Table.Cell textAlign="center">{file.filename}</Table.Cell>,
                <Table.Cell textAlign="center">{file.datetime}</Table.Cell>,
                <CellType type={file.result} />,
              ],
            },
            content: {
              children: [
                  <BonusFileInfo file={file} key={file.file_hash} />
              ],
            },
          };
        })}
      /> */}

        <Table.Body>{allItemRows}</Table.Body>
      </Table>
    );
  }
}

export default FilesTable;
