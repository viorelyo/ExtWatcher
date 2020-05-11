import React from "react";
import { Feed } from "semantic-ui-react";

import UploadEvent from "../FeedEventTypes/UploadEvent";
import SubmitEvent from "../FeedEventTypes/SubmitEvent";

function EventsFeed(props) {
  if (!props.feed || !props.feed.length) {
    return <div />;
  }

  const events = props.feed.map(event => {
    let eventType;
    switch (event.event_type) {
      case "upload":
        eventType = (
          <UploadEvent
            datetime={event.datetime}
            filename={event.content}
            origin_ip={event.origin_ip}
          />
        );
        break;
      case "submit":
        eventType = (
          <SubmitEvent
            datetime={event.datetime}
            submitted_url={event.content}
            origin_ip={event.origin_ip}
          />
        );
        break;
      default:
        eventType = null;
        break;
    }

    return <Feed key={event.content}>{eventType}</Feed>;
  });

  return <div>{events}</div>;
}

export default EventsFeed;
