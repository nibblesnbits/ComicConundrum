import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import ComicCard from './ComicCard'
import { batchArray } from '../util';

export default function ComicGrid({ comics, size, saveClick, deleteClick, ownedList }) {
  const rows = batchArray(comics, size || 6);
  return (
    <Container>
      {rows.map((items, i) => (
        <Row key={i}>
          {items.map(c => (
            <Col key={c.id}>
              <ComicCard
                comic={c}
                saveClick={saveClick}
                deleteClick={deleteClick}
                owned={(ownedList || []).indexOf(c.id) > -1}
              />
            </Col>
          ))}
        </Row>
      ))}
    </Container>
  );
}