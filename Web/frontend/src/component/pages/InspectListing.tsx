import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { apiService } from "../../api/apiService";
import BackToHomeButton from "../Back_To_Home_Button/Back_to_Home";
import styles from "./module.css/inspectlisting.module.css";

type Topic = {
  id: number;
  title: string;
  createdate: string;
  user_id: number;
  category_id: number;
};

const TopicDetails: React.FC = () => {
  const { topicId } = useParams<{ topicId: string }>();
  const [topic, setTopic] = useState<Topic | null>(null);
  const [selectedTab, setSelectedTab] = useState<"details" | "members">("details"); // Új állapot a tabok kezelésére

  useEffect(() => {
    const fetchTopicDetails = async () => {
      try {
        const topicData = await apiService.fetchTopicById(Number(topicId));
        setTopic(topicData);
      } catch (error) {
        console.error("Error fetching topic details:", error);
      }
    };

    fetchTopicDetails();
  }, [topicId]);

  if (!topic) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <BackToHomeButton />
      <h1>{topic.title}</h1>
      
      <div>
        {/* Gombok, amik váltják a tabokat */}
        <button onClick={() => setSelectedTab("details")}>Details</button>
        <button onClick={() => setSelectedTab("members")}>Members</button>
      </div>

      {/* A kiválasztott tab alapján jelennek meg a tartalmak */}
      {selectedTab === "details" && (
        <div>
          <div>
          <div className={styles.tags}></div>
          <div className={styles.description}></div>
          </div>
        </div>
      )}

      {selectedTab === "members" && (
        <div>
          <div>
          {/* Itt később a tagok listáját lehet megjeleníteni */}
          <p>List of Members (to be implemented)</p>
          </div>
        </div>
      )}
    </div>
  );
};

export default TopicDetails;
