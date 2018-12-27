import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Search from './components/Search';
import Collection from './components/Collection';
import Comic from './components/Comic';
import './App.css';

export default () => (
  <Layout>
    <Route exact path='/' component={Search} />
    <Route path='/collection' component={Collection} />
    <Route path='/Comic/:id' component={Comic} />
  </Layout>
);
