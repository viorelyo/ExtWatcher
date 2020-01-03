import React from 'react';
import { Route } from 'react-router';
import Layout from './layout/Layout';
import Home from './pages/Home'
// import './App.css';

export default () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route path="/home" component={Home} />
  </Layout>
);
