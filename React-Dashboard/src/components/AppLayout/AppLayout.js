import React from "react";
import "./AppLayout.scss";
import TopMenu from "../TopMenu/TopMenu";
import SideMenu from "../SideMenu/SideMenu";

class AppLayout extends React.Component {
  render() {
    return (
      <div className="app-layout">
        <TopMenu />
        <SideMenu location={this.props.location} />
        {this.props.children}
      </div>
    );
  }
}

export default AppLayout;
