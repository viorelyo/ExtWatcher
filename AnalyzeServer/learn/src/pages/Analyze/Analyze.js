import React from "react";
import {
  Segment,
  Input,
  Button,
  Header,
  Icon,
  Divider
} from "semantic-ui-react";
import { validURL } from "../../services/url";
import "./Analyze.scss";
import AnalyzeMessage from "../../components/AnalyzeMessage/AnalyzeMessage";
import BadRequestMessage from "../../components/AnalyzeMessage/BadRequestMessage";

class Analyze extends React.Component {
  state = {
    inputValue: "",
    inputErrorActive: false,
    loading: true,
    showResult: false,
    showWarning: false,
    data: {}
  };

  render() {
    let showedMessage = this.state.showResult ? (
      <AnalyzeMessage
        loading={this.state.loading}
        result={this.state.data.message}
      />
    ) : (
      <div />
    );

    if (this.state.showWarning) {
      showedMessage = <BadRequestMessage message={this.state.data.message} />;
    }

    return (
      <div>
        <Segment placeholder textAlign="center">
          <Header as="h2" icon textAlign="center">
            <Divider hidden />
            <Icon name="dna" circular />
            <Header.Content>Analyze</Header.Content>
            <Header.Subheader>
              Analyze suspicious files by submitting URL where you have found
              them
            </Header.Subheader>
          </Header>

          <Divider hidden />
          <Input
            action
            placeholder="URL"
            error={this.state.inputErrorActive}
            onChange={this.handleInput}
            ref={input => (this.inputtext = input)}
          >
            <input className="url-input" />
            <Button color="teal" icon="globe" onClick={this.handleClick} />
          </Input>
          <Divider hidden />
        </Segment>

        {showedMessage}
      </div>
    );
  }

  handleInput = e => {
    this.setState({
      inputErrorActive: !this.isUrlValid(e.target.value),
      inputValue: e.target.value
    });
  };

  handleClick = () => {
    this.setState({ showWarning: false });

    var data = new FormData();
    data.append("url", this.state.inputValue);

    this.setState({ loading: true }, () => {
      fetch("http://localhost:5000/api/url-submit", {
        method: "POST",
        body: data
      }).then(response => {
        if (response.ok) {
          response.json().then(data => {
            this.setState({
              loading: false,
              data: data
            });
          });
        } else {
          response.json().then(data => {
            this.setState({
              loading: false,
              showWarning: true,
              data: data
            });
          });
        }
      });
    });
    this.setState({ showResult: true });
  };

  isUrlValid(url) {
    return validURL(url);
  }
}

export default Analyze;
