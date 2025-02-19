import React, { useState, useEffect, Suspense } from "react";
import { useNavigate } from "react-router-dom";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import TopicList from "../List/List";
import { apiService } from "../../api/apiService";
import homeStyles from "../pages/module.css/home.module.css"
import clsx from "clsx";
import PostList from "../PostList/PostList";
import { dividerClasses } from "@mui/material";

// TODO: Rework this component
const Home: React.FC = () => {
  const [selectedGame, setSelectedGame] = useState<string>("League of legends");
  const navigate = useNavigate();

  const handleGameChange = async (
    event: React.MouseEvent<HTMLElement>,
    newGame: string
  ) => {
    if (newGame) setSelectedGame(newGame);
  };



  return (
    <div className={clsx(homeStyles.container)}>
     {/* TODO: rework ToggleButton so that it make this POS less clustered*/}
    
      <PostList/>
    </div>
  );
};

export default Home;
