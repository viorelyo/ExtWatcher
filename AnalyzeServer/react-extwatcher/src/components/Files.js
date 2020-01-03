import React from 'react';
import { List, Header } from 'semantic-ui-react';

export const Files = ({ files }) => {
    return (
        <List>
            {files.map(file => {
                return (
                    <List.Item key={file.filename}>
                        <Header>{file.filename}</Header>
                    </List.Item>
                )
            })}
        </List>
    );
};