import React from "react";
import { Icon, Menu } from "semantic-ui-react";
import { Link } from "react-router-dom";

export class SideMenuItem extends React.Component {
  render() {
    const isActive = this.isSelected();
    const color = isActive ? "teal" : null;
    const active = isActive;

    return (
      <Menu.Item active={active} color={color} as={Link} to={this.props.path}>
        <Icon name={this.props.icon} />
        {this.props.label}
      </Menu.Item>
    );
  }

  isSelected() {
    const { pathname } = this.props.location;
    if (this.props.path === "/") {
      return pathname === this.props.path;
    }
    return pathname.includes(this.props.path);
  }
}

export default SideMenuItem;
