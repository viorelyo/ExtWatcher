import React from "react";
import "./AppLayout.scss";
import TopMenu from "../TopMenu/TopMenu";
import SideMenu from "../SideMenu/SideMenu";

export function AppLayout(props) {
  return (
    <div className="app-layout">
      <TopMenu />
      <SideMenu />
      {props.children}
    </div>
  );
}
