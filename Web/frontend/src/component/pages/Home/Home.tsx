import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import clsx from "clsx";
import PostList from "../../PostList/PostList";
import homeStyles from "./home.module.css";

const games = [
  "League of Legends",
  "Valorant",
  "Counter-Strike 2",
  "Dota 2",
];

const Home: React.FC = () => {
  const [selectedGame, setSelectedGame] = useState<string>(games[0]);
  const navigate = useNavigate();

  const handleGameChange = (
    event: React.MouseEvent<HTMLElement>,
    newGame: string | null
  ) => {
    if (newGame) setSelectedGame(newGame);
  };

  return (
    <div className={homeStyles.container}>
      <ToggleButtonGroup
        value={selectedGame}
        exclusive
        onChange={handleGameChange}
        className={homeStyles.gamechooser}
      >
        {games.map((game) => (
          <ToggleButton
            key={game}
            value={game}
            className={clsx(
              homeStyles.gamechooserItem,
              selectedGame !== game && homeStyles.untoggled
            )}
          >
            {game}
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
      <div className={homeStyles.postListWrapper}>
        <h2 className={homeStyles.selectedGameTitle}>{selectedGame} Posts</h2>
        <PostList selectedGame={selectedGame} />
      </div>
    </div>
  );
};

export default Home;
