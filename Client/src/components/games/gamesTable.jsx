import React from "react";
import { useHistory } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Moment from "moment";

const GamesTable = (props) => {
  const history = useHistory({});

  const { games, onDelete } = props;
  const handleEdit = (game) => {
    return history.push(`/game/${game.id}`);
  };
  return (
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
                  onClick={() => onDelete(x)}
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
  );
};

export default GamesTable;
