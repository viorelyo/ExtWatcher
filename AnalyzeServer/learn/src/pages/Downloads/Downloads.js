import React from "react";
import {
  Button,
  Icon,
  Rating,
  Card,
  Statistic,
  Grid,
  Divider,
  Accordion,
  Label,
  Message
} from "semantic-ui-react";
import logo from "../../assets/images/icon.png";

const extra = (
  <Button animated="vertical" color="blue" circular onClick={download}>
    <Button.Content hidden>
      <Icon name="download" />
    </Button.Content>
    <Button.Content visible>
      <Icon name="windows" />
    </Button.Content>
  </Button>
);

function download() {
  var link = document.createElement("a");
  link.download = "extwatcher-service.zip";
  link.href = "http://localhost:5000/api/extwatcher-service";
  link.click();
}

const panels = [
  {
    key: "1.0.0",
    title: {
      content: <Label color="blue" content="Version 1.0.0" />
    },
    content: {
      content: (
        <Message
          info
          content=".NET Windows Service with support for detecting PDF files."
        />
      )
    }
  },
  {
    key: "2.0.0",
    title: {
      content: <Label color="blue" content="Version 1.1.0" />
    },
    content: {
      content: (
        <Message
          info
          content="Feature Added: System Tray Notification when file is detected."
        />
      )
    }
  }
];

function Downloads() {
  return (
    <Grid>
      <Grid.Column floated="left" width={5}>
        <Statistic size="small">
          <Statistic.Value>5,550</Statistic.Value>
          <Statistic.Label>Downloads</Statistic.Label>
        </Statistic>
        <Divider />
        <Accordion panels={panels} />
      </Grid.Column>
      <Grid.Column floated="right" width={6}>
        <Card
          image={logo}
          header="ExtWatcher Service"
          meta="Windows Service"
          description="ExtWatcher Service will detect, submit and clean threats without causing slowdowns."
          extra={extra}
        />
        <Rating icon="star" defaultRating={3} maxRating={5} size="huge" />
      </Grid.Column>
    </Grid>
  );
}

export default Downloads;
