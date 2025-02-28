import styles from "./creategroup.module.css";
import BackToHomeButton from "../../Back_To_Home_Button/Back_to_Home";
import { useNavigate } from "react-router-dom";
import { ROUTES } from "../../../App";
import { useEffect, useState } from "react";
import { apiService } from "../../../api/apiService";
import { GroupDTO } from "../../../api/DTOs/GroupDTO";
import { PostDTO } from "../../../api/DTOs/PostDTO";
//TODO Fix this POS WHEN WE HAVE THE WILL FOR IT!!!!!!!!!!!!!!!!!!

const CreateGroup = () => {
  const [postName, setPostName] = useState<string>("");
  const [groupDesc, setGroupDesc] = useState<string>("");
  const [memberLimit, setmemberLimit] = useState<number | undefined>(undefined);
  const [creatorName, setCreatorName] = useState<string>("");

  const navigate = useNavigate();

  useEffect(() => {
    const fetchUserInfo = async () => {
      try {
        const userInfo = await apiService.getUserInfo();
        setCreatorName(userInfo["name"]);
      } catch (error) {
        console.error("Failed to fetch user info", error);
      }
    };

    fetchUserInfo();
  }, []);
  
  const handleCreateListingAndGroup = async () => {
    if (!postName || !groupDesc || !memberLimit) {
      alert("Please fill in all fields.");
      return;
    }
  
    try {
      const postDTO: PostDTO = {
        title: postName,
        content: groupDesc
      };
  
      const postResponse = await apiService.createPost(postDTO);
      if (!postResponse) {
        alert("Failed to create post.");
        return;
      }
      console.log("Post created:", postResponse);
  
      const groupDTO: GroupDTO = {
        name: postName,
        description: groupDesc,
        memberSize: memberLimit
      };
  
      const groupResponse = await apiService.createGroup(groupDTO);
      const createdGroup = groupResponse.data;
  
      if (createdGroup) {
        console.log("Group created:", createdGroup);
        alert("Group and Post created successfully!");
        navigate(ROUTES.aftercreate.path);
      } else {
        alert("Failed to create group.");
      }
    } catch (error) {
      console.error("Failed to create listing", error);
      alert("Something went wrong while creating the listing.");
    }
  };
  

  return (
    <>
      <div className={styles.Back_to_Home}>
        <BackToHomeButton />
      </div>
      <div className={styles.main}>
        <div className={styles.very_basic_details}>
          <div className={styles.details_inputs}>
            <form onSubmit={(e) => e.preventDefault()}>
              <input
                type="text"
                placeholder="Group's name..."
                value={postName}
                onChange={(e) => setPostName(e.target.value)}
              />
              <input
                type="text"
                placeholder="Group Description"
                value={groupDesc}
                onChange={(e) => setGroupDesc(e.target.value)}
              />
              <input
                type="number"
                placeholder="Member Limit"
                value={memberLimit ?? ""}
                onChange={(e) => setmemberLimit(e.target.value ? parseInt(e.target.value) : undefined)}
              />
            </form>
          </div>
        </div>
        <div className={styles.preview}>
          <div className={styles.title_wrapper}>
            <h1 className={styles.title}>{postName}</h1>
          </div>

          <div className={styles.title_and_team_size}>
            <h3 className={styles.creatorname}>{creatorName}</h3>
            <span className={styles.team_size}>Team size: {memberLimit}</span>
          </div>

          <p className={styles.description}>{groupDesc}</p>
        </div>

        <button className={styles.create_listing} onClick={handleCreateListingAndGroup}>
          Create Listing
        </button>
      </div>
    </>
  );
};

export default CreateGroup;
