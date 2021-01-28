import React from "react";
import { useHistory } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Moment from "moment";
import TableHeader from "../common/tableHeader";
import TableBody from "./../common/tableBody";

const GamesTable = (props) => {
  const history = useHistory({});
  const { games, onDelete } = props;

  const handleEdit = (game) => {
    return history.push(`/game/${game.id}`);
  };

  const columns = [
    { label: "ID", path: "id" },
    { label: "Title", path: "name" },
    { label: "Description", path: "description" },
    {
      label: "Released",
      path: "releasedAt",
      content: (x) => Moment(x.releasedAt).format("DD/MM/yyyy"),
    },
    { label: "Rating", path: "rating" },
    {
      label: "",
      key: "button-group",
      content: (x) => (
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
      ),
    },
  ];

  return (
    <Table striped bordered hover>
      <TableHeader columns={columns} />
      <TableBody data={games} columns={columns} />
    </Table>
  );
};

export default GamesTable;
