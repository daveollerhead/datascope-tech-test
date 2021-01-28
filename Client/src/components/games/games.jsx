import React, { Fragment } from "react";
import Form from "react-bootstrap/Form";
import PaginationComponent from "../common/pagination";
import Col from "react-bootstrap/Col";
import useGames from "../../hooks/useGames";
import Loader from "react-loader-spinner";
import GamesHeader from "./gamesHeader";
import GamesTable from "./gamesTable";

function Games() {
  const {
    games,
    isLoading,
    pagination,
    handleSetPageSize,
    handleDeleteGame,
    setPage,
  } = useGames();

  if (isLoading) {
    return (
      <Fragment>
        <GamesHeader />
        <Loader
          className="text-center"
          type="Puff"
          color="#00BFFF"
          height={100}
          width={100}
          timeout={3000} //3 secs
        />
      </Fragment>
    );
  }

  if (!games || games.length < 1) {
    return (
      <Fragment>
        <GamesHeader />
        <p>Currently there are no rated games, why not add one</p>
      </Fragment>
    );
  }

  return (
    <Fragment>
      <GamesHeader />
      <GamesTable games={games} onDelete={handleDeleteGame} />
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
                  value={pagination.pageSize}
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
