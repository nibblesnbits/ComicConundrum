const search = 'REQUEST_COMIC_SEARCH';
const recieveSearchResults = 'RECEIVE_COMIC_SEARCH';
const getById = 'REQUEST_COMIC';
const getByIdComplete = 'REQUEST_COMIC_COMPLETE';
const initialState = {
  searchResults: [],
  isLoading: false
};

export const actionCreators = {
  search: title => async (dispatch, getState) => {
    if (getState().comics.isLoading) {
      return;
    }
    dispatch({ type: search, title });

    const url = `api/comics/search?title=${encodeURIComponent(title)}`;
    const response = await fetch(url);
    const searchResults = await response.json();

    dispatch({ type: recieveSearchResults, title, searchResults });
  },
  getById: id => async (dispatch) => {
    dispatch({ type: getById, id });

    const url = `api/comics/${id}`;
    const response = await fetch(url);
    const comic = await response.json();

    dispatch({ type: getByIdComplete, comic });
  },
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === search) {
    return {
      ...state,
      title: action.title,
      isLoading: true
    };
  }

  if (action.type === getById) {
    return {
      ...state,
      id: action.id,
      isLoading: true
    };
  }

  if (action.type === recieveSearchResults) {
    return {
      ...state,
      title: action.title,
      searchResults: action.searchResults,
      isLoading: false
    };
  }

  if (action.type === getByIdComplete) {
    return {
      ...state,
      comic: action.comic,
      isLoading: false
    };
  }

  return state;
};
