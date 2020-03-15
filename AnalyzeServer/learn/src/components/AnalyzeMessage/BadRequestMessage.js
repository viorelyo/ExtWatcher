import React from "react";
import { Message, Icon } from "semantic-ui-react";

function BadRequestMessage(props) {
  return (
    <Message icon warning>
      <Icon name="warning sign" />
      <Message.Content>
        <Message.Header>Bad Input</Message.Header>
        {props.message}
      </Message.Content>
    </Message>
  );
}

export default BadRequestMessage;
