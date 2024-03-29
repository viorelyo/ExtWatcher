import React from "react";
import { Feed, Icon } from "semantic-ui-react";

function UploadEvent(props) {
  return (
    <Feed.Event>
      <Feed.Label>
        <Icon name="upload" />
      </Feed.Label>
      <Feed.Content>
        <Feed.Date>{props.datetime}</Feed.Date>
        <Feed.Summary>
          File:{" "}
          <a href={"https://www.google.com/search?q=" + props.filename}>
            {props.filename}
          </a>{" "}
          uploaded from IP:
          {props.origin_ip}
        </Feed.Summary>
      </Feed.Content>
    </Feed.Event>
  );
}

export default UploadEvent;
