import React, { useState, useEffect } from "react";
import { apiService } from "../../../api/apiService";
import styles from "./creatorscreenafterlisting.module.css";
import BackToHomeButton from "../../Back_To_Home_Button/Back_to_Home";
import { User } from "../../../api/Models/User";

const CreatorScreenAfterListing: React.FC = () => {
  const [members, setMembers] = useState<User[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMembers = async () => {
      setLoading(true);
      setError(null);

      try {
        const data = await apiService.getMembers(1); // API hívás az új metódussal
        setMembers(data);
      } catch (err) {
        setError("Failed to fetch members. Please try again.");
        console.error("Error fetching members:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchMembers();
  }, []);

  if (loading) return <p>Loading members...</p>;
  if (error) return <p className={styles.error}>{error}</p>;

  return (
    <div>
      <div className={styles.memberContainer}>
        {members.length > 0 ? (
          members.map((member) => (
            <div key={member.id} className={styles.memberCard}>
              <p><strong>{member.userName}</strong></p>
              <p>Email: {member.email}</p>
              {member.phoneNumber && <p>Phone: {member.phoneNumber}</p>}
            </div>
          ))
        ) : (
          <p>No members found.</p>
        )}
        <div className={styles.Back_to_Home}>
        <BackToHomeButton/>
        </div>
      </div>


    
    </div>
  );
};

export default CreatorScreenAfterListing;
