import React from "react";
import { Feed, Icon } from "semantic-ui-react";

function SubmitEvent(props) {
  return (
    <Feed.Event>
      <Feed.Label>
        <Icon name="cloud upload" />
      </Feed.Label>
      <Feed.Content>
        <Feed.Date>{props.datetime}</Feed.Date>
        <Feed.Summary>
          URL: <a href={props.submitted_url}>{props.submitted_url}</a> submitted
          from IP:
          {props.origin_ip}
        </Feed.Summary>
      </Feed.Content>
    </Feed.Event>
  );
}

export default SubmitEvent;
