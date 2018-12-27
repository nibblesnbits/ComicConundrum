import React from 'react';
import { Card, CardImg, CardBody, CardTitle, Button, ButtonGroup } from 'reactstrap';
import { Link } from 'react-router-dom';

export default function ComicCard({ comic, saveClick, deleteClick, owned}) {
  return (
    <Card>
      <CardImg top width="100%" src={`${comic.thumbnail.path}.${comic.thumbnail.extension}`} alt="Card image cap" />
      <CardBody>
        <CardTitle>{comic.title}</CardTitle>
        <ButtonGroup>
          {!owned && <Button color="primary" onClick={() => saveClick(comic)}>I own this!</Button>}
          {owned && <Button color="danger" onClick={() => deleteClick(comic.id)}>I lost this!</Button>}
          <Link className="btn btn-success" to={`/comic/${comic.id}`}>View</Link>
        </ButtonGroup>
      </CardBody>
    </Card>
  );
}