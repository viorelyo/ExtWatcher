import React, { Component } from "react";
import { Link } from "react-router-dom";
import {
  Menu,
  Image,
  Input,
  Form,
  Icon,
  Modal,
  Button,
  Header
} from "semantic-ui-react";
import "./TopMenu.scss";
import logo from "../../assets/images/logo.jpg";

export default class TopMenu extends Component {
  state = { modalOpen: false };

  handleOpen = () => this.setState({ modalOpen: true });
  handleClose = () => this.setState({ modalOpen: false });

  render() {
    return (
      <Menu inverted borderless fixed="top" className="top-menu">
        <Menu.Item header className="logo">
          <Image src={logo} size="tiny" as={Link} to="/" />
        </Menu.Item>

        <Menu.Menu className="nav-container">
          <Menu.Item className="search-input">
            <Form>
              <Form.Field>
                <Input icon="search" placeholder="Search..." size="small" />
              </Form.Field>
            </Form>
          </Menu.Item>
        </Menu.Menu>

        <Menu.Menu position="right">
          <Menu.Item name="about">
            <Modal
              trigger={
                <Icon
                  link
                  size="big"
                  name="question circle outline"
                  onClick={this.handleOpen}
                />
              }
              open={this.state.modalOpen}
              onClose={this.handleClose}
              basic
              size="small"
            >
              <Header icon="copyright outline" content="ExtWatcher" />
              <Modal.Content>
                <h3>Cloud malware Analyzer based on Machine Learning.</h3>
                <h5 className="vg-rights">
                  2019 - 2020. All rights reserved. Viorel GURDIS
                </h5>
              </Modal.Content>
              <Modal.Actions>
                <Button color="green" onClick={this.handleClose} inverted>
                  <Icon name="checkmark" /> Got it
                </Button>
              </Modal.Actions>
            </Modal>
          </Menu.Item>
        </Menu.Menu>
      </Menu>
    );
  }
}
