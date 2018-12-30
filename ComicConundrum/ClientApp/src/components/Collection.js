import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators as comicsActions } from '../store/Comics';
import { actionCreators as collectionActions } from '../store/Collection';
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
    this.props.getCollection();
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
    const { owned, isLoading } = this.props;
    return (
      <div>
        <h1>My Collection</h1>
        <input type="search" onChange={this.filterChange} value={this.state.filter} placeholder="Search" />
        {isLoading &&
          <div className="spinner">
            <div className="rect1"></div>
            <div className="rect2"></div>
            <div className="rect3"></div>
            <div className="rect4"></div>
            <div className="rect5"></div>
          </div>
        }
        {renderGrid(this.props, this.deleteComic, owned, isLoading)}
      </div>
    );
  }
}

function renderGrid({ comics }, deleteClick, owned, isLoading) {
  return (
    <ComicGrid 
    comics={comics} 
    size={4} 
    deleteClick={deleteClick} 
    ownedList={owned}
    isLoading={isLoading} />
  );
}

const makeMapStateToProps = () => {
  const getIds = makeGetCollectionIds();
  const filteredCollection = makeGetFilteredCollection();
  const mapStateToProps = (state, props) => {
    return {
      ...state.collection,
      comics: filteredCollection(state),
      owned: getIds(state),
    };
  };
  return mapStateToProps;
}

export default connect(
  makeMapStateToProps,
  dispatch => bindActionCreators({ ...comicsActions, ...collectionActions }, dispatch)
)(Collection);