import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators as comicsActions } from '../store/Comics';
import { actionCreators as collectionActions } from '../store/Collection';
import ComicGrid from './ComicGrid';
import { debounce } from '../util';
import { Container, Row, Col } from 'reactstrap';
import { makeGetCollectionIds } from '../selectors/collectionSelectors';

class Search extends Component {

  constructor(props) {
    super(props);
    this.state = { term: '' };
    this.search = debounce(this.search.bind(this), 1000);
    this.searchChange = this.searchChange.bind(this);
    this.saveComic = this.saveComic.bind(this);
    this.deleteComic = this.deleteComic.bind(this);
  }

  componentDidMount() {
    this.props.getCollection();
  }

  search() {
    this.props.search(this.state.term);
  }

  searchChange(e) {
    const term = e.target.value;
    this.setState({ term });
    if (term.length > 3) {
      this.search();
    }
  }

  deleteComic(id) {
    this.props.delete(id);
  }

  saveComic(comic) {
    this.props.save(comic);
    this.props.getCollection();
  }

  render() {
    const { owned, isLoading } = this.props;
    return (
      <div>
        <h1>Comic Search</h1>
        <Container>
          <Row>
            <Col sm="auto">
              <input type="search" onChange={this.searchChange} value={this.state.term} />
            </Col>
            <Col sm="1">
              {isLoading &&
                <div className="spinner">
                  <div className="rect1"></div>
                  <div className="rect2"></div>
                  <div className="rect3"></div>
                  <div className="rect4"></div>
                  <div className="rect5"></div>
                </div>
              }
            </Col>
          </Row>
        </Container>
        {renderGrid(this.props, this.saveComic, this.deleteComic, owned)}
      </div>
    );
  }
}

function renderGrid({ searchResults }, saveClick, deleteClick, owned) {
  return (
    <ComicGrid comics={searchResults} saveClick={saveClick} deleteClick={deleteClick} ownedList={owned} />
  );
}

const makeMapStateToProps = () => {
  const getIds = makeGetCollectionIds();
  const mapStateToProps = (state, props) => {
    return {
      searchResults: state.comics.searchResults,
      owned: getIds(state),
      isLoading: state.comics.isLoading
    };
  };
  return mapStateToProps;
}

export default connect(
  makeMapStateToProps,
  dispatch => bindActionCreators({ ...comicsActions, ...collectionActions }, dispatch)
)(Search);