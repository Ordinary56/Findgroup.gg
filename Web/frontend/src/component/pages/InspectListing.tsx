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
  const { topicId } = useParams<{ topicId?: string }>(); // Biztosítjuk, hogy topicId optional legyen
  const [topic, setTopic] = useState<Topic | null>(null);
  const [selectedTab, setSelectedTab] = useState<"details" | "members">("details");
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTopicDetails = async () => {
      if (!topicId) return; // Ha nincs topicId, ne próbáljon fetch-elni

      setLoading(true);
      setError(null);

      try {
        const topicData = await apiService.getTopicById(Number(topicId)); // Új API metódus
        setTopic(topicData);
      } catch (err) {
        setError("Error fetching topic details. Please try again.");
        console.error("Error fetching topic details:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchTopicDetails();
  }, [topicId]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p className={styles.error}>{error}</p>;
  if (!topic) return <p>No topic found.</p>;

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
          <div className={styles.tags}></div>
          <div className={styles.description}></div>
        </div>
      )}

      {selectedTab === "members" && (
        <div>
          {/* Itt később a tagok listáját lehet megjeleníteni */}
          <p>List of Members (to be implemented)</p>
        </div>
      )}
    </div>
  );
};

export default TopicDetails;
