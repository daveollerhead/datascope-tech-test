import React, { Component } from "react";
import Joi from "joi-browser";
import Button from "react-bootstrap/Button";
import Input from "./input";

class InputForm extends Component {
  state = {
    data: {},
    errors: {},
  };

  validateProperty = ({ name, value }) => {
    const obj = { [name]: value };
    const schema = { [name]: this.schema[name] };
    const { error } = Joi.validate(obj, schema);

    return error ? error.details[0].message : null;
  };

  validate = () => {
    const options = { abortEarly: false };
    const result = Joi.validate(this.state.data, this.schema, options);
    if (!result.error) {
      return null;
    }

    const errors = {};
    for (let item of result.error.details) {
      errors[item.path[0]] = item.message;
    }

    return errors;
  };

  handleChange = ({ currentTarget: input }) => {
    const errors = { ...this.state.errors };
    const errorMessage = this.validateProperty(input);

    if (errorMessage) {
      errors[input.name] = errorMessage;
    } else {
      delete errors[input.name];
    }

    const data = { ...this.state.data };
    data[input.name] = input.value;
    this.setState({ data, errors });
  };

  handleSubmit = async (e) => {
    e.preventDefault();

    const errors = this.validate();
    this.setState({ errors: errors || {} });
    if (errors) {
      return;
    }

    this.doSubmit();
  };

  renderButton = (label) => {
    return (
      <Button
        block
        variant="primary"
        type="submit"
        onClick={this.handleSubmit}
        disabled={this.validate()}
      >
        {label}
      </Button>
    );
  };

  renderInput = (name, label, placeholder, type = "text") => {
    const { data, errors } = this.state;

    return (
      <Input
        name={name}
        type={type}
        label={label}
        placeholder={placeholder}
        value={data[name]}
        onChange={this.handleChange}
        error={errors[name]}
      />
    );
  };
}

export default InputForm;
