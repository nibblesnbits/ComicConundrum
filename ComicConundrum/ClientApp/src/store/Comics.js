const searchComics = 'REQUEST_COMIC_SEARCH';
const recieveSearchResults = 'RECEIVE_COMIC_SEARCH';
const initialState = { comics: [], isLoading: false };

export const actionCreators = {
    searchComics: title => async (dispatch, getState) => {
        if (title === getState().comicSearch.title) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: searchComics, title });

        const url = `api/comics/search?title=${title}`;
        const response = await fetch(url);
        const comics = await response.json();

        dispatch({ type: recieveSearchResults, title, comics });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === searchComics) {
        return {
            ...state,
            title: action.title,
            isLoading: true
        };
    }

    if (action.type === recieveSearchResults) {
        return {
            ...state,
            title: action.title,
            comics: action.comics,
            isLoading: false
        };
    }

    return state;
};
