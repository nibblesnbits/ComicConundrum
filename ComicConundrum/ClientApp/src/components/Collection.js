import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Collection';
import ComicGrid from './ComicGrid';
import { debounce } from '../util';
import { makeGetFilteredCollection, makeGetCollectionIds } from '../selectors/collectionSelectors';

class Collection extends Component {

  constructor(props) {
    super(props);
    this.state = { filter: '' };
    this.filterChange = this.filterChange.bind(this)
    this.deleteComic = this.deleteComic.bind(this)
    this.search = debounce(this.search.bind(this), 1000);
  }

  componentDidMount() {
    this.props.getCollection();
  }

  deleteComic(id) {
    this.props.delete(id);
  }

  search(filter) {
    const { setFilter } = this.props;
    setFilter(filter);
  }

  filterChange(e) {
    const filter = e.target.value;
    this.setState({
      filter
    });
    this.search(filter);
  }

  render() {
    const { owned } = this.props;
    return (
      <div>
        <h1>My Collection</h1>
        <input type="search" onChange={this.filterChange} value={this.state.filter} placeholder="Search" />
        {renderGrid(this.props, this.deleteComic, owned)}
      </div>
    );
  }
}

function renderGrid({ comics }, deleteClick, owned) {
  return (
    <ComicGrid comics={comics} size={4} deleteClick={deleteClick} ownedList={owned} />
  );
}

const makeMapStateToProps = () => {
  const getIds = makeGetCollectionIds();
  const filteredCollection = makeGetFilteredCollection();
  const mapStateToProps = (state, props) => {
    return {
      comics: filteredCollection(state),
      owned: getIds(state),
      filter: state.collection.filter
    };
  };
  return mapStateToProps;
}

export default connect(
  makeMapStateToProps,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Collection);