import React from "react";
import { Message, Icon } from "semantic-ui-react";

function AnalyzeMessage(props) {
  let iconName = "circle notched";
  let headerContent = "Just one second";
  let msgContent =
    "We are downloading and then analyzing that content for you.";

  let showInfo = true;
  let showSuccess = false;
  let showNegative = false;

  if (!props.loading) {
    showInfo = false;
    msgContent = "";

    if (props.result === "benign") {
      showSuccess = true;
      showNegative = false;
      iconName = "check";
      headerContent = "Benign";
    } else if (props.result === "malicious") {
      showNegative = true;
      showSuccess = false;
      iconName = "warning";
      headerContent = "Malicious";
    }
  }

  return (
    <Message icon info={showInfo} success={showSuccess} negative={showNegative}>
      <Icon name={iconName} loading={showInfo} />
      <Message.Content>
        <Message.Header>{headerContent}</Message.Header>
        {msgContent}
      </Message.Content>
    </Message>
  );
}

export default AnalyzeMessage;
