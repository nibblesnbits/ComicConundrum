import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Collection';

class Comic extends Component {
  render() {
    const { id } = this.props.match.params;
    return (
      <div>
        {id}
      </div>
    );
  }
}

export default connect(
  state => state.comics,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Comic);