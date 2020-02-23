import React from "react";
import { Feed, Header, Divider } from "semantic-ui-react";
import "./Home.scss";

import UploadEvent from "../../components/FeedEventTypes/UploadEvent";
import AnalyzeEvent from "../../components/FeedEventTypes/AnalyzeEvent";

function Home() {
  return (
    <div className="home-page">
      <Header size="huge" content="Feed" />
      <Divider />

      <Feed>
        <UploadEvent />
        <AnalyzeEvent />
        <AnalyzeEvent />
        <AnalyzeEvent />
        <UploadEvent />
        <UploadEvent />
        <UploadEvent />
      </Feed>
    </div>
  );
}

export default Home;
