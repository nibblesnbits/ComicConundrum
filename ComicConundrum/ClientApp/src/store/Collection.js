const requestCollection = 'RECEIVE_COLLECTION';
const recieveCollectionResults = 'RECEIVE_COLLECTION_COMPLETE';
const save = 'REQUEST_COMIC_SAVE';
const saveComplete = 'COMIC_SAVE_COMPLETE';
const deleteComic = 'REQUEST_COMIC_DELETE';
const setFilter = 'SET_COLLECTION_FILTER';

const initialState = {
  collection: [],
  filter: '',
  isLoading: false
};

export const actionCreators = {
  getCollection: () => async (dispatch) => {
    dispatch({ type: requestCollection });

    const url = `api/collection`;
    const response = await fetch(url);
    const comics = await response.json();

    dispatch({ type: recieveCollectionResults, comics });
  },
  save: comic => async (dispatch) => {

    dispatch({ type: save, comic });

    const url = `api/collection/save`;
    await fetch(url, {
      method: 'post',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(comic)
    });

    dispatch({ type: saveComplete });
  },
  delete: id => async (dispatch) => {

    dispatch({ type: deleteComic, id });

    const url = `api/collection/${id}`;
    await fetch(url, {
      method: 'delete'
    });

    dispatch({ type: requestCollection });
  },
  setFilter: (filter) => ({
      type: setFilter,
      filter
  })
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestCollection) {
    return {
      ...state,
      title: action.title,
      isLoading: true
    };
  }

  if (action.type === save) {
    return {
      ...state,
      isLoading: true
    };
  }

  if (action.type === setFilter) {
    return {
      ...state,
      filter: action.filter
    };
  }

  if (action.type === saveComplete) {
    return {
      ...state,
      isLoading: false
    };
  }

  if (action.type === recieveCollectionResults) {
    return {
      ...state,
      title: action.title,
      comics: action.comics,
      isLoading: false
    };
  }

  return state;
};
