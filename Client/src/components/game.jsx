import React from "react";
import Joi from "joi-browser";
import Form from "react-bootstrap/Form";
import InputForm from "./common/form";
import GamesService from "../services/gamesService";
import { toast } from "react-toastify";
import Moment from "moment";

class Game extends InputForm {
  state = {
    data: {
      name: "",
      description: "",
      releasedAt: "",
      rating: 0,
    },
    errors: {},
    isEdit: false,
  };

  schema = {
    id: Joi.number(),
    name: Joi.string().required().label("Name"),
    description: Joi.string().required().label("Description"),
    releasedAt: Joi.date().required().label("Release Date"),
    rating: Joi.number().required().max(10).min(0).label("Rating"),
  };

  async componentDidMount() {
    const id = this.props.match.params.id;
    if (!id) {
      return;
    }
    try {
      const { data } = await GamesService.get(id);
      this.setState({
        data: {
          ...data,
          releasedAt: Moment(data.releasedAt).toISOString().substr(0, 10),
        },
      });
    } catch (ex) {
      if (ex.response && ex.response.status === 404) {
        toast.error("Looks like no game exists with the given ID");
        return;
      }

      toast.error("Sorry something has gone wrong");
    }

    this.setState({ isEdit: true });
  }

  doSubmit = async () => {
    try {
      const { data } = this.state;

      if (this.state.isEdit) {
        await GamesService.put(this.state.data.id, {
          ...data,
          rating: Number(data.rating),
        });
      } else {
        await GamesService.post({ ...data, rating: Number(data.rating) });
      }
    } catch (ex) {
      if (ex.response && ex.response.status === 400) {
        toast.error(
          "Looks like I missed some validation on the client side, awkward..."
        );
      }

      toast.error("Sorry something has gone wrong");
      return;
    }

    return this.props.history.push("/games");
  };

  render() {
    return (
      <div className="col-xl-4 col-lg-6">
        <h2>Rate a new Game</h2>
        <Form>
          {this.renderInput("name", "Name", "Enter name for game to rate")}

          {this.renderInput(
            "description",
            "Description",
            "Enter description of game to rate"
          )}

          {this.renderInput(
            "releasedAt",
            "Release Date",
            "Enter release date of game to rate",
            "date"
          )}

          {this.renderInput(
            "rating",
            "Rating",
            "Enter rating for game",
            "number"
          )}

          {this.renderButton("Submit")}
        </Form>
      </div>
    );
  }
}

export default Game;
