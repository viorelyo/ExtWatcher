import React from "react";
import { Route, Switch } from "react-router-dom";

import "semantic-ui-css/semantic.min.css";
import "./App.css";

import { AppLayout } from "./components/AppLayout/AppLayout";

import Home from "./pages/Home";
import Statistics from "./pages/Statistics";

function App() {
  return (
    <AppLayout>
      <Switch>
        <Route path="/home" component={Home} />
        <Route path="/stats" component={Statistics} />
        {/* <Route path="/downloads" component={Downloads} /> */}
        {/* <Route path="/analyze" component={Analyze} /> */}
      </Switch>
    </AppLayout>
  );
}

export default App;
