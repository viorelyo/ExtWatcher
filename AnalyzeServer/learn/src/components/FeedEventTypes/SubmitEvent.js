import React from "react";
import { Feed } from "semantic-ui-react";

function SubmitEvent() {
  return (
    <Feed.Event
      icon="cloud upload"
      date="23.02.2020 - 20:18:00"
      summary="IP: 192.10.12.255 submitted file: 'curs.pdf'."
    />
  );
}

export default SubmitEvent;
