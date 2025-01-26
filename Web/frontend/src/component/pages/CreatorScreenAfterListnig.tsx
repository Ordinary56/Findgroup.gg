import React, { useState, useEffect } from "react";
import { apiService } from "../../api/apiService";
import styles from "./module.css/creatorscreenafterlisting.module.css"
import BackToHomeButton from "../Back_To_Home_Button/Back_to_Home";

type Member = {
  id: string;
  userName: string;
  email: string;
  phoneNumber?: string;
};

const CreatorScreenAfterListing: React.FC = () => {
  const [members, setMembers] = useState<Member[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMembers = async () => {
      try {
        setLoading(true);
        const data = await apiService.fetchMembers();
        setMembers(data);
      } catch (err) {
        setError("Failed to fetch members. Please try again.");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchMembers();
  }, []);

  if (loading) {
    return <p>Loading members...</p>;
  }

  if (error) {
    return <p className={styles.error}>{error}</p>;
  }

  return (
    <div>
      <div className={styles.memberContainer}>
        {members.map((member) => (
          <div key={member.id} className={styles.memberCard}>
            <p><strong>{member.userName}</strong></p>
            <p>Email: {member.email}</p>
            {member.phoneNumber && <p>Phone: {member.phoneNumber}</p>}
          </div>
        ))}
      </div>

      <div>
        <BackToHomeButton />
      </div>
      <div>Finish Listing</div>
    </div>
  );
};

export default CreatorScreenAfterListing;
