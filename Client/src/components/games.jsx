import React, { Fragment, useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Form from "react-bootstrap/Form";
import GamesService from "../services/gamesService";
import { toast } from "react-toastify";
import Moment from "moment";
import PaginationComponent from "./common/pagination";
import Col from "react-bootstrap/Col";

function Games() {
  const history = useHistory({});
  const [games, setGames] = useState([]);
  const [pagination, setPagination] = useState({
    page: 1,
    pageSize: 5,
  });

  const loadGames = async (page, pageSize) => {
    const response = await GamesService.getPaged(page, pageSize);
    const data = response.data;
    setGames(data);

    setPagination(JSON.parse(response.headers["x-pagination"]));
  };

  useEffect(() => {
    loadGames(pagination.page, pagination.pageSize);
  }, []);

  const handleDelete = async (game) => {
    const previousState = games;
    const games = games.filter((m) => m.id !== game.id);
    setGames(games);

    try {
      await GamesService.remove(game.id);
    } catch (ex) {
      if (!ex.response || ex.response.status !== 404) {
        toast.error("Sorry something has gone wrong");
        setGames(previousState);
        return;
      }

      toast("Looks like that game had already been deleted....");
    }

    const page =
      games.length <= 1 && pagination.page > 1
        ? pagination.page - 1
        : pagination.page;

    loadGames(page, pagination.pageSize);
  };

  const handleEdit = (game) => {
    return history.push(`/game/${game.id}`);
  };

  const handlePageChange = async (page) => {
    setPagination({ page: page, ...pagination });
    await loadGames(page, pagination.pageSize);
  };

  const handlePageSizeChange = async ({ currentTarget: input }) => {
    setPagination({ pageSize: input.value, ...pagination });
    await loadGames(pagination.page, input.value);
  };

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
                    onClick={() => handleDelete(x)}
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
            onPageChange={handlePageChange}
          />
        </div>
        <div className="offset-md-6">
          <Form.Group controlId="pageSize">
            <Form.Row>
              <Col>
                <Form.Label>page size</Form.Label>
              </Col>
              <Col>
                <Form.Control as="select" onChange={handlePageSizeChange}>
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
