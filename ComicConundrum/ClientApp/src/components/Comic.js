import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators as comicsActions } from '../store/Comics';
import { actionCreators as collectionActions } from '../store/Collection';

class Comic extends Component {

  componentDidMount() {
    this.props.getById(this.props.match.params.id);
  }

  render() {
    const { id } = this.props.current || {};
    return (
      <div>
        {id}
      </div>
    );
  }
}

export default connect(
  state => state.comics,
  dispatch => bindActionCreators({ ...comicsActions, ...collectionActions }, dispatch)
)(Comic);