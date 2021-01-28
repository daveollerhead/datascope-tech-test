import React, { Fragment } from "react";
import PaginationComponent from "../common/pagination";
import useGames from "../../hooks/useGames";
import Loader from "react-loader-spinner";
import GamesHeader from "./gamesHeader";
import GamesTable from "./gamesTable";
import SelectList from "../common/selectList";

function Games() {
  const {
    games,
    isLoading,
    pagination,
    handleSetPageSize,
    handleDeleteGame,
    setPage,
  } = useGames();

  if (isLoading) {
    return (
      <Fragment>
        <GamesHeader />
        <Loader
          className="text-center"
          type="Puff"
          color="#00BFFF"
          height={100}
          width={100}
          timeout={3000}
        />
      </Fragment>
    );
  }

  if (!games || games.length < 1) {
    return (
      <Fragment>
        <GamesHeader />
        <p>Currently there are no rated games, why not add one</p>
      </Fragment>
    );
  }

  return (
    <Fragment>
      <GamesHeader />
      <GamesTable games={games} onDelete={handleDeleteGame} />
      <div className="row">
        <div className="col-md-3">
          <PaginationComponent
            pagination={pagination}
            onPageChange={(page) => setPage(page)}
          />
        </div>
        <div className="offset-md-6">
          <SelectList
            id="pageSize"
            label="page size"
            value={pagination.pageSize}
            onChange={handleSetPageSize}
            items={[
              { value: 5, label: 5 },
              { value: 10, label: 10 },
              { value: 25, label: 25 },
            ]}
          />
        </div>
      </div>
    </Fragment>
  );
}

export default Games;
