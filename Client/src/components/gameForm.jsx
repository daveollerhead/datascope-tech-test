import React from "react";
import Joi from "joi-browser";
import Form from "react-bootstrap/Form";
import useGame from "./../hooks/useGame";
import useValidation from "./../hooks/useValidation";
import Input from "./common/input";
import Button from "react-bootstrap/Button";

const GameForm = (props) => {
  const schema = {
    id: Joi.number(),
    name: Joi.string().required().max(100).label("Name"),
    description: Joi.string().required().max(500).label("Description"),
    releasedAt: Joi.date().required().max(new Date()).label("Release Date"),
    rating: Joi.number().required().max(10).min(0).label("Rating"),
  };

  const { errors, validateSchema, validateProperty } = useValidation(schema);
  const { data, setData, saveGame } = useGame(props.match.params.id);

  const handleChange = ({ target }) => {
    validateProperty(target);

    const newData = { ...data };
    newData[target.name] = target.value;
    setData(newData);
  };

  const handleDisabled = () => {
    if (Object.keys(errors).length > 0) {
      return true;
    }

    return false;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (validateSchema(data)) {
      return;
    }

    await saveGame();

    return props.history.push("/games");
  };

  const renderInput = (name, label, placeholder, type = "text") => {
    return (
      <Input
        name={name}
        type={type}
        label={label}
        placeholder={placeholder}
        value={data[name]}
        onChange={(e) => handleChange(e)}
        error={errors[name]}
      />
    );
  };

  return (
    <div className="col-xl-4 col-lg-6">
      <h2>Rate a new Game</h2>
      <Form>
        {renderInput("name", "Name", "Enter name for game to rate")}

        {renderInput(
          "description",
          "Description",
          "Enter description of game to rate"
        )}

        {renderInput(
          "releasedAt",
          "Release Date",
          "Enter release date of game to rate",
          "date"
        )}

        {renderInput("rating", "Rating", "Enter rating for game", "number")}

        <Button
          block
          variant="primary"
          type="submit"
          onClick={handleSubmit}
          disabled={handleDisabled()}
        >
          Submit
        </Button>
      </Form>
    </div>
  );
};

export default GameForm;
