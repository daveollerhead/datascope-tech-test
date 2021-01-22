import React from "react";
import Form from "react-bootstrap/Form";

function Input({ name, label, error, ...rest }) {
  return (
    <Form.Group controlId={name}>
      <Form.Label>{label}</Form.Label>
      <Form.Control name={name} {...rest} />
      {error && <Form.Text className="text-danger">{error}</Form.Text>}
    </Form.Group>
  );
}

export default Input;
