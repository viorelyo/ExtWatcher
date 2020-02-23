import React from "react";
import { Feed } from "semantic-ui-react";

function UploadEvent() {
  return (
    <Feed.Event
      icon="upload"
      date="23.02.2020 - 15:48:00"
      summary="IP: 10.10.12.255 uploaded file: '12.pdf'."
    />
  );
}

export default UploadEvent;
