import React from "react";
import { Menu } from "semantic-ui-react";
import "./SideMenu.scss";

import { SideMenuItem } from "./SideMenuItem/SideMenuItem";

export class SideMenu extends React.Component {
  render() {
    return (
      <Menu
        inverted
        borderless
        vertical
        icon="labeled"
        fixed="left"
        className="side-nav"
        size="tiny"
      >
        <SideMenuItem
          path="/"
          label="Home"
          icon="home"
          location={this.props.location}
        />
        <SideMenuItem
          path="/stats"
          label="Statistics"
          icon="chart bar"
          location={this.props.location}
        />
        <SideMenuItem
          path="/analyze"
          label="Analyze"
          icon="dna"
          location={this.props.location}
        />
        <SideMenuItem
          path="/downloads"
          label="Download"
          icon="download"
          location={this.props.location}
        />
      </Menu>
    );
  }
}

export default SideMenu;
