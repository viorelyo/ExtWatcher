import React from "react";
import { Header, Divider, Loader } from "semantic-ui-react";
import { getAllFeed } from "../../../store/reducers/feed";
import { connect } from "react-redux";

import EventsFeed from "../../../components/EventsFeed/EventsFeed";

class HomeContent extends React.Component {
  render() {
    const allFeed = this.getAllFeed();

    return (
      <div>
        <Loader active={this.props.showLoader} size="large">
          Loading
        </Loader>

        <Header size="huge" content="Feed" />
        <Divider />
        <EventsFeed feed={allFeed} />
      </div>
    );
  }

  getAllFeed() {
    return this.props.allFeed;
  }
}

function mapStateToProps(state) {
  return {
    allFeed: getAllFeed(state)
  };
}

export default connect(mapStateToProps, null)(HomeContent);
