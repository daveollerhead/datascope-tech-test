import React, { Fragment } from "react";
import { useHistory } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Form from "react-bootstrap/Form";
import Moment from "moment";
import PaginationComponent from "./common/pagination";
import Col from "react-bootstrap/Col";
import useGames from "../hooks/useGames";
import _ from "lodash";

function Games() {
  const history = useHistory({});
  const {
    games,
    isLoading,
    pagination,
    handleSetPageSize,
    handleDeleteGame,
    setPage,
  } = useGames();

  const handleEdit = (game) => {
    return history.push(`/game/${game.id}`);
  };

  if (isLoading) {
    return (
      <Fragment>
        <h2>Games Rating App</h2>
        <p>Loading...</p>
      </Fragment>
    );
  }

  if (!games || games.length < 1) {
    return (
      <Fragment>
        <h2>Games Rating App</h2>
        <p>Currently there are no rated games, why not add one</p>
      </Fragment>
    );
  }

  return (
    <Fragment>
      <h2>Games Rating App</h2>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>
            <th>Description</th>
            <th>Released</th>
            <th>Rating</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {games.map((x) => (
            <tr key={x.id}>
              <td>{x.id}</td>
              <td>{x.name}</td>
              <td>{x.description}</td>
              <td>{Moment(x.releasedAt).format("DD/MM/yyyy")}</td>
              <td>{x.rating}</td>
              <td>
                <ButtonGroup>
                  <Button
                    onClick={() => handleEdit(x)}
                    variant="outline-primary"
                    size="sm"
                  >
                    Edit
                  </Button>
                  <Button
                    onClick={() => handleDeleteGame(x)}
                    variant="outline-danger"
                    size="sm"
                  >
                    Delete
                  </Button>
                </ButtonGroup>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <div className="row">
        <div className="col-md-3">
          <PaginationComponent
            pagination={pagination}
            onPageChange={(page) => setPage(page)}
          />
        </div>
        <div className="offset-md-6">
          <Form.Group controlId="pageSize">
            <Form.Row>
              <Col>
                <Form.Label>page size</Form.Label>
              </Col>
              <Col>
                <Form.Control
                  as="select"
                  onChange={(e) => handleSetPageSize(e.currentTarget.value)}
                >
                  <option>5</option>
                  <option>10</option>
                  <option>25</option>
                </Form.Control>
              </Col>
            </Form.Row>
          </Form.Group>
        </div>
      </div>
    </Fragment>
  );
}

export default Games;
