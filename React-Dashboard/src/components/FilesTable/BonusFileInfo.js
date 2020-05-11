import React from "react";
import { Grid, Icon, Segment, Header } from "semantic-ui-react";

export function BonusFileInfo(props) {
  return (
    <Segment color="teal">
      <Grid>
        <Grid.Column floated="left" width={4}>
          <Header as="h4">
            <Icon name="time" />
            <Header.Content>
              Process time
              <Header.Subheader>{props.file.analysis_time.toFixed(2)} s</Header.Subheader>
            </Header.Content>
          </Header>
        </Grid.Column>
        <Grid.Column floated="right" width={4}>
          <Header as="h4">
            <Icon name="save" />
            <Header.Content>
              Size
              <Header.Subheader>{props.file.filesize}</Header.Subheader>
            </Header.Content>
          </Header>
        </Grid.Column>
      </Grid>
    </Segment>
  );
}
