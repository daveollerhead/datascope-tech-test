import { useState } from "react";
import Joi from "joi-browser";

const useValidation = (schema) => {
  const [errors, setErrors] = useState({});

  const getErrorMessage = ({ name, value }) => {
    const obj = { [name]: value };
    const schemaz = { [name]: schema[name] };
    const { error } = Joi.validate(obj, schemaz);

    return error ? error.details[0].message : null;
  };

  const validateProperty = (input) => {
    const inputErrors = { ...errors };
    const errorMessage = getErrorMessage(input);

    if (errorMessage) {
      inputErrors[input.name] = errorMessage;
    } else {
      delete inputErrors[input.name];
    }

    setErrors(inputErrors);
  };

  const validateSchema = (data) => {
    const options = { abortEarly: false };
    const result = Joi.validate(data, schema, options);
    if (!result.error) {
      return null;
    }

    const inputErrors = {};
    for (let item of result.error.details) {
      inputErrors[item.path[0]] = item.message;
    }

    setErrors(inputErrors);
    return inputErrors;
  };

  return {
    errors,
    validateSchema,
    validateProperty,
  };
};

export default useValidation;
