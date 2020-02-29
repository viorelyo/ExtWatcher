import React from "react";
import { Table, Image, Accordion } from "semantic-ui-react";
import "./FilesTable.scss";
import { CellType } from "./ResultCellType/CellType";

import pdf from "../../assets/images/pdf.svg";

function FilesTable(props) {
  if (!props.files || !props.files.length) {
    return <div />;
  }

  // const fileItems = props.files.map(file => {
  //   let type;
  //   switch (file.filetype) {
  //     case "pdf":
  //       type = pdf;
  //       break;
  //     default:
  //       type = pdf;
  //       break;
  //     //TODO new supported filetypes will be added here
  //   }

  //   return (
  //     <Table.Row key={file.file_hash}>
  //       <Table.Cell collapsing>
  //         <Image src={type} size="mini" centered />
  //       </Table.Cell>
  //       <Table.Cell collapsing textAlign="center">
  //         {file.file_hash}
  //       </Table.Cell>
  //       <Table.Cell textAlign="center">{file.filename}</Table.Cell>
  //       <Table.Cell textAlign="center">26/02/2020 - 23:03:00</Table.Cell>
  //       <CellType type={file.verdict} />
  //     </Table.Row>
  //   );
  // });

  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell textAlign="center">Filetype</Table.HeaderCell>
          <Table.HeaderCell textAlign="center">MD5</Table.HeaderCell>
          <Table.HeaderCell textAlign="center">Filename</Table.HeaderCell>
          <Table.HeaderCell textAlign="center">Date</Table.HeaderCell>
          <Table.HeaderCell textAlign="center">Result</Table.HeaderCell>
        </Table.Row>
      </Table.Header>

      <Accordion
        fluid={true}
        as={Table.Body}
        panels={props.files.map(file => {
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
                <Table.Cell textAlign="center">
                  26/02/2020 - 23:03:00
                </Table.Cell>,
                <CellType type={file.verdict} />
              ]
            },
            content: {
              children: [
                <Image src={type} size="massive" centered />,
                <Image src={type} size="massive" centered />
              ]
            }
          };
        })}
      />

      {/* <Table.Body>{fileItems}</Table.Body> */}
    </Table>
  );
}

export default FilesTable;