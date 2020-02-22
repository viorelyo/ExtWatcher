import React, { useState } from "react";
import { Icon, Menu } from "semantic-ui-react";
import { Link } from "react-router-dom";
import "./SideMenu.scss";

function SideMenu() {
  const pathname = window.location.pathname;
  const path = pathname === "/" ? "home" : pathname.substr(1);

  const [activeItem, setActiveItem] = useState(path);
  const [color] = useState("teal");

  const handleItemClick = (e, { name }) => {
    setActiveItem(name);
  };

  return (
    <Menu
      inverted
      borderless
      vertical
      icon="labeled"
      fixed="left"
      className="side-nav"
    >
      <Menu.Item
        name="home"
        active={activeItem === "home"}
        onClick={handleItemClick}
        color={color}
        as={Link}
        to="/home"
      >
        <Icon name="home" />
        Home
      </Menu.Item>

      <Menu.Item
        name="stats"
        active={activeItem === "stats"}
        onClick={handleItemClick}
        color={color}
        as={Link}
        to="/stats"
      >
        <Icon name="chart bar" />
        Statistics
      </Menu.Item>

      <Menu.Item
        name="analyze"
        active={activeItem === "analyze"}
        onClick={handleItemClick}
        color={color}
        as={Link}
        to="/analyze"
      >
        <Icon name="dna" />
        Analyze
      </Menu.Item>

      <Menu.Item
        name="downloads"
        active={activeItem === "downloads"}
        onClick={handleItemClick}
        color={color}
        as={Link}
        to="/downloads"
      >
        <Icon name="download" />
        Downloads
      </Menu.Item>
    </Menu>
  );
}

export default SideMenu;
