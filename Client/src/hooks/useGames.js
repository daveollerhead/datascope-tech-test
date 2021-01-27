import { useEffect, useState } from "react";
import GamesService from "./../services/gamesService";
import { toast } from "react-toastify";
import usePagination from "./usePagination";
import useLoading from "./useLoading";

const useGames = () => {
  const [games, setGames] = useState({
    games: [],
  });

  const { isLoading, loading, finishedLoading } = useLoading(true);
  const { pagination, setPage, setPageSize, setPaginationData } = usePagination(
    1,
    5
  );

  const loadGames = async () => {
    loading();
    const response = await GamesService.getPaged(
      pagination.page,
      pagination.pageSize
    );
    const paginationData = JSON.parse(response.headers["x-pagination"]);
    setGames(response.data);
    setPaginationData(paginationData);
    finishedLoading();
  };

  useEffect(() => {
    loadGames();
  }, [pagination.page, pagination.pageSize]);

  const deleteGame = async (game) => {
    const previousState = games;
    const newGames = games.filter((x) => x.id !== game.id);
    setGames(newGames);
    try {
      await GamesService.remove(game.id);
    } catch (ex) {
      if (!ex.response || ex.response.status !== 404) {
        toast.error("Sorry something has gone wrong");
        setGames(previousState);
        return;
      }
      toast("Looks like that game had already been deleted....");
    }

    if (games.length <= 1 && pagination.page > 1) {
      setPage(pagination.page - 1);
      return;
    }

    loadGames();
  };

  return {
    games,
    isLoading,
    pagination,
    handleSetPageSize: setPageSize,
    handleDeleteGame: deleteGame,
    setPage: setPage,
  };
};

export default useGames;
