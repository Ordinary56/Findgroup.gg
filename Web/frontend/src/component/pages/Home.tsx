import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import TopicList from "../List/List";
import { apiService } from "../../api/apiService";
import homeStyles from "../pages/module.css/home.module.css"
import clsx from "clsx";

type Topic = {
  id: number;
  title: string;
  createdate: string;
  user_id: number;
  category_id: number;
};

const categoryMapping: Record<string, number> = {
  "League of legends": 1,
  "Apex legends": 2,
};

const Home: React.FC = () => {
  const [selectedGame, setSelectedGame] = useState<string>("League of legends");
  const [topics, setTopics] = useState<Topic[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const handleGameChange = async (
    event: React.MouseEvent<HTMLElement>,
    newGame: string
  ) => {
    if (newGame) setSelectedGame(newGame);
  };

  const handleTopicClick = (topicId: number) => navigate(`/topics/${topicId}`);

  useEffect(() => {
    const fetchTopics = async () => {
      setLoading(true);
      try {
        const categoryId = categoryMapping[selectedGame];
        if (categoryId) {
          const topicsData = await apiService.fetchTopicsByCategory(categoryId);
          setTopics(topicsData);
        } else {
          setTopics([]); // Ha nincs megfelelő kategória, ürítsük a listát
        }
      } catch (error) {
        console.error("Error fetching topics:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchTopics();
  }, [selectedGame]);

  return (
    <div className={clsx(homeStyles.container)}>
     <ToggleButtonGroup
      className={clsx(homeStyles.gamechooser)}
      value={selectedGame}
      exclusive
      onChange={handleGameChange}
    >
      {Object.keys(categoryMapping).map((game) => (
        <ToggleButton
          key={game}
          value={game}
          className={clsx(
            homeStyles.gamechooserItem,
            selectedGame !== game && homeStyles.untoggled // feltételes osztály
          )}
        >
          {game}
        </ToggleButton>
      ))}
    </ToggleButtonGroup>

      <TopicList topics={topics} loading={loading} onTopicClick={handleTopicClick} />
    
    </div>
  );
};

export default Home;
