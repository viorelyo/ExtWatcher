import React from 'react';
import { Table, Button, Icon } from 'semantic-ui-react';

export const Files = ({ files }) => {
    return (
        <Table compact celled>
            <Table.Header fullWidth>
            <Table.Row>
                <Table.HeaderCell>Name</Table.HeaderCell>
                <Table.HeaderCell>Filepath</Table.HeaderCell>
                <Table.HeaderCell width={2}>Actions</Table.HeaderCell>
                </Table.Row>
            </Table.Header>

            <Table.Body>
                {files.map(file => <Table.Row>
                        <Table.Cell>{file.filename}</Table.Cell>
                        <Table.Cell>{file.filepath}</Table.Cell>
                        <Table.Cell>
                            <Button size="mini" icon>
                                <Icon name="pencil" />
                            </Button>
                            <Button color="red" size="mini" icon>
                                <Icon name="delete" />
                            </Button>
                        </Table.Cell>
                    </Table.Row>
                )}
            </Table.Body>
            {/* // <List.Item key={file.filename}>
                    //     <Header>{file.filename}</Header>
                    // </List.Item> */}
            <Table.Footer fullWidth>
                <Table.Row>
                <Table.HeaderCell colSpan={5} />
                </Table.Row>
            </Table.Footer>
        </Table>
    );
};