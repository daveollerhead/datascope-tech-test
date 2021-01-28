import React from "react";
import Button from "react-bootstrap/Button";

const SubmitButton = (props) => {
  return (
    <Button block variant="primary" type="submit" {...props}>
      {props.label}
    </Button>
  );
};

export default SubmitButton;
