import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import TopicList from "../List/List";
import { apiService } from "../../api/apiService";
import styles from "./module.css/home.module.css";
import clsx from "clsx";

type Topic = {
  id: number;
  title: string;
  createdate: string;
  user_id: number;
  category_id: number;
};

export default function Home() {
  const [selectedGame, setSelectedGame] = useState<string>("League of legends");
  const [topics, setTopics] = useState<Topic[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const handleGameChange = async (
    event: React.MouseEvent<HTMLElement>,
    newGame: string
  ) => {
    if (newGame) {
      setSelectedGame(newGame);
    }
  };

  const handleTopicClick = (topicId: number) => {
    navigate(`/topics/${topicId}`);
  };

  useEffect(() => {
    const fetchTopics = async () => {
      setLoading(true);
      try {
        const categoryMapping: Record<string, number> = {
          "League of legends": 1,
          "Apex legends": 2,
        };

        const categoryId = categoryMapping[selectedGame];
        const topicsData = await apiService.fetchTopicsByCategory(categoryId);
        setTopics(topicsData);
      } catch (error) {
        console.error("An error occurred while fetching groups:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchTopics();
  }, [selectedGame]);

  return (
    <div>
      <ToggleButtonGroup
        className={clsx(styles.gamechooser)}
        value={selectedGame}
        exclusive
        onChange={handleGameChange}
      >
        <ToggleButton
          value="League of legends"
          className={clsx(styles.gamechooserItem)}
        >
          League of legends
        </ToggleButton>
        <ToggleButton
          value="Apex legends"
          className={clsx(styles.gamechooserItem)}
        >
          Apex legends
        </ToggleButton>
      </ToggleButtonGroup>

      <TopicList topics={topics} loading={loading} onTopicClick={handleTopicClick} />
    </div>
  );
}
