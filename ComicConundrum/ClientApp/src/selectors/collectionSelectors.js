import { createSelector } from 'reselect'

const scope = state => state.collection;

const collection = state => scope(state).comics || [];
const searchFilter = state => scope(state).filter || '';

function getFilteredCollection(list, filter) {
  if (!filter) {
    return list;
  }
  const regex = new RegExp(filter, 'i');
  const map = list.map(c => ({ key: c.id, body: `${c.title}${c.description}` }));
  const ids = map.filter(i => {
    return regex.test(i.body);
  });
  const matching = list.filter(i => ids.some(id => id.key === i.id));
  return matching;
}

export const makeGetFilteredCollection = () => createSelector(
  [collection, searchFilter],
  (list, filter) => getFilteredCollection(list, filter)
);

export const makeGetCollectionIds = () => createSelector(
  [collection],
  (list) => list.map(c => c.id)
);