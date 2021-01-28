import React from "react";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";

const SelectList = (props) => {
  const { id, label, value, onChange, items } = props;
  return (
    <Form.Group controlId={id}>
      <Form.Row>
        <Col>
          <Form.Label>{label}</Form.Label>
        </Col>
        <Col>
          <Form.Control
            as="select"
            value={value}
            onChange={(e) => onChange(e.currentTarget.value)}
          >
            {items.map((x) => (
              <option value={x.value}>{x.label}</option>
            ))}
          </Form.Control>
        </Col>
      </Form.Row>
    </Form.Group>
  );
};

export default SelectList;
