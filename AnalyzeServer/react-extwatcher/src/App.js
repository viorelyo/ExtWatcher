import React from 'react';
import { Route } from 'react-router';
import Layout from './layout/Layout';
import Home from './pages/Home'
// import './App.css';

const App = () => {
  return (
      <Layout>
        <Route exact path="/" component={Home} />
        {/* <Route path="/home" component={Home} /> */}
      </Layout>
    );
}

export default App;