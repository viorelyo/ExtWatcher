import React, { Component } from "react";
import { Link, withRouter } from "react-router-dom";
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

class TopMenu extends Component {
  constructor(props) {
    super(props);
    this.state = {
      query: "",
      modalOpen: false
    };
  }

  onInputChange = event => {
    this.setState({
      query: event.target.value
    });
  };

  onSubmit = () => {
    const escapedSearchQuery = encodeURI(this.state.query);
    this.props.history.push(`/results?search_query=${escapedSearchQuery}`);
  };

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
            <Form onSubmit={this.onSubmit}>
              <Form.Field>
                <Input
                  icon="search"
                  placeholder="Search..."
                  size="small"
                  list="queries"
                  value={this.state.query}
                  onChange={this.onInputChange}
                />
                <datalist id="queries">
                  <option value="hash=" />
                  <option value="filename=" />
                  <option value="type=" />
                  <option value="datetime=" />
                </datalist>
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
              <Header
                icon="copyright outline"
                content="ExtWatcher"
                size="large"
              />
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

export default withRouter(TopMenu);
