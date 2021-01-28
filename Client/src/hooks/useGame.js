import { useState, useEffect } from "react";
import GamesService from "../services/gamesService";
import { toast } from "react-toastify";
import Moment from "moment";

const useGame = (id) => {
  const [data, setData] = useState({
    name: "",
    description: "",
    releasedAt: "",
    rating: 0,
  });

  const [isEdit, setIsEdit] = useState(false);

  useEffect(() => {
    const loadData = async () => {
      if (!id) {
        return;
      }
      try {
        const response = await GamesService.get(id);
        const game = response.data;
        setData({
          ...game,
          releasedAt: Moment(game.releasedAt).toISOString().substr(0, 10),
        });
      } catch (ex) {
        if (ex.response && ex.response.status === 404) {
          toast.error("Looks like no game exists with the given ID");
          return;
        }
        toast.error("Sorry something has gone wrong");
      }

      setIsEdit(true);
    };

    loadData();
  }, []);

  const saveGame = async () => {
    try {
      if (isEdit) {
        await GamesService.put(data.id, {
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
        return;
      }

      toast.error("Sorry something has gone wrong");
      return;
    }
  };

  return {
    data,
    setData,
    saveGame,
  };
};

export default useGame;
