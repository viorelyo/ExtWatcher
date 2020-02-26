import React from "react";
import { Feed, Header, Divider } from "semantic-ui-react";
import "./Home.scss";

import UploadEvent from "../../components/FeedEventTypes/UploadEvent";
import SubmitEvent from "../../components/FeedEventTypes/SubmitEvent";

function Home() {
  return (
    <div className="home-page">
      <Header size="huge" content="Feed" />
      <Divider />

      <Feed>
        <UploadEvent />
        <SubmitEvent />
        <SubmitEvent />
        <SubmitEvent />
        <UploadEvent />
        <UploadEvent />
        <UploadEvent />
      </Feed>
    </div>
  );
}

export default Home;
