import React, { Component } from "react";
import { Route, Switch, withRouter, Redirect } from "react-router-dom";
import "semantic-ui-css/semantic.min.css";

import AppLayout from "./components/AppLayout/AppLayout";

import Home from "./pages/Home/Home";
import Statistics from "./pages/Statistics/Statistics";
import Analyze from "./pages/Analyze/Analyze";
import Search from "./pages/Search/Search";
import UnknownPage from "./pages/UnknownPage/UnknownPage";
import Downloads from "./pages/Downloads/Downloads";

class App extends Component {
  render() {
    return (
      <AppLayout location={this.props.location}>
        <Switch>
          <Route exact path="/" component={Home} />
          <Route exact path="/stats" component={Statistics} />
          <Route exact path="/analyze" component={Analyze} />
          <Route exact path="/downloads" component={Downloads} />
          <Route
            path="/results"
            render={() => <Search key={this.props.location.key} />}
          />
          <Route path="/unknown" component={UnknownPage} />
          <Redirect to="/unknown" />
        </Switch>
      </AppLayout>
    );
  }
}

export default withRouter(App);
