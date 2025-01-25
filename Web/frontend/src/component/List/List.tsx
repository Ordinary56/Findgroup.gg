import React from "react";
import styles from "./list.module.css";
import clsx from "clsx";

type Topic = {
  id: number;
  title: string;
  createdate: string;
};

type TopicListProps = {
  topics: Topic[];
  loading: boolean;
  onTopicClick: (topicId: number) => void; // Típus hozzáadása
};

const TopicList: React.FC<TopicListProps> = ({ topics, loading, onTopicClick }) => {
  if (loading) {
    return <p>Betöltés...</p>;
  }

  if (topics.length === 0) {
    return <p>Nincsenek elérhető témák.</p>;
  }

  return (
    <div className={clsx(styles.topicList)}>
      {topics.map((topic) => (
        <div
          key={topic.id}
          className={clsx(styles.topicItem)}
          onClick={() => onTopicClick(topic.id)} // Kattintási esemény hozzárendelése
          style={{ cursor: "pointer" }}
        >
          <h3>{topic.title}</h3>
          <p>Létrehozva: {new Date(topic.createdate).toLocaleDateString()}</p>
        </div>
      ))}
    </div>
  );
};

export default TopicList;
