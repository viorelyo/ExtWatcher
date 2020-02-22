import React from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { Container } from 'semantic-ui-react';

import 'semantic-ui-css/semantic.min.css';
import './App.css';

import SideMenu from './components/SideMenu';
import TopMenu from './components/TopMenu';

import Home from './pages/Home';
import Statistics from './pages/Statistics';


function App() {
    return (
        <Router>
            <SideMenu />
            <TopMenu />
            <Container>
                <Route path="/home" component={Home} />
                <Route path="/stats" component={Statistics} />
                {/* <Route path="/downloads" component={Downloads} /> */}
                {/* <Route path="/analyze" component={Analyze} /> */}
            </Container>
        </Router>
    );
  }
  
export default App;