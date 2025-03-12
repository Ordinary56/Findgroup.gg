import styles from "./creategroup.module.css";
import BackToHomeButton from "../../component/Back_To_Home_Button/Back_to_Home";
import { Link, useAsyncError, useNavigate } from "react-router-dom";
import { ROUTES } from "../../App";
import { useEffect, useState } from "react";
import { apiService } from "../../api/apiService";
import { Category } from "../../api/Models/Category";
import { PostDTO } from "../../api/DTOs/PostDTO";
import { GroupDTO } from "../../api/DTOs/GroupDTO";

const CreateGroup = () => {
  const [postName, setPostName] = useState<string>("");
  const [postContent, setPostContent] = useState<string>("");
  const [groupName, setGroupName] = useState<string>("");
  const [groupDesc, setGroupDesc] = useState<string>("");
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<number>(1);
  const [memberLimit, setmemberLimit] = useState<number>(1);
  const [creatorName, setCreatorName] = useState<string>("");
  const [creatorId, setCreatorId] = useState<string>("")


  useEffect(() => {
    const fetchUserInfo = async () => {
      try {
        const userInfo = await apiService.getUserInfo();
        setCreatorName(userInfo.name);
        setCreatorId(userInfo.nameidentifier);
      } catch (error) {
        console.error("Failed to fetch user info", error);
      }
    };
    const fetchCategories = async () => {
      const categories = await apiService.getCategories();
      setCategories(categories);
    }
    fetchUserInfo();
    fetchCategories();
  }, []);
  const CreateGroupAndPost = async () => {
    const dto: PostDTO = {
      title: postName,
      content: postContent,
      categoryId: selectedCategory,
      userId: creatorId
    };
    // NOTE: CreatedAtAction returns the WHOLE post, not just the id
    const post = await apiService.createPost(dto);
    if (!post) {
      alert("something went wrong when creating a post");
      return;
    }
    const group: GroupDTO = {
      groupName: groupName,
      Description: groupDesc,
      memberLimit: memberLimit,
      postId: post!.id,
      userId: creatorId
    };
    const createdGroup = await apiService.createGroup(group);
    if (!createdGroup) {
      alert("something went wrong when creating a post");
      return;
    }
    alert("Successfully created group and post!");
    const navigate = useNavigate();
    navigate("/");
  };
  return (
    <>
      <div className={styles.Back_to_Home}>
        <BackToHomeButton />
      </div>
      <div className={styles.main}>
        <div className={styles.very_basic_details}>
          <div className={styles.details_inputs}>
            <form>
              <input
                type="text"
                name="postName"
                placeholder="Post's name..."
                maxLength={14}
                onInput={(e) => {
                  const value = (e.target as HTMLInputElement).value;
                  setPostName(value);
                }} />
              <input
                type="text"
                name="groupname"
                placeholder="Group's name..."
                maxLength={14}
                onInput={(e) => {
                  const value = (e.target as HTMLInputElement).value;
                  setGroupName(value);
                }}


              />
              <input
                type="text"
                placeholder="Group Description"
                name="groupDesc"
                onInput={(e) => {
                  const value = (e.target as HTMLInputElement).value;
                  setGroupDesc(value);
                  
                }}
              />
                 <input
                type="text"
                placeholder="Post Description"
                name="postcontent"
                onInput={(e) => {
                  const value = (e.target as HTMLInputElement).value;
               
                  setPostContent(value);
                }}
              />
              <input
                type="number"
                placeholder="Member Limit"
                name="memberLimit"
                onInput={(e) =>
                  setmemberLimit(parseInt((e.target as HTMLInputElement).value))
                }
              />
              <select>
                {categories.map((category) => (
                  <option key={category.id} value={category.categoryName} onClick={_ => setSelectedCategory(category.id)}>
                    {category.categoryName}
                  </option>
                ))}
              </select>
            </form>
          </div>
        </div>
        <div className={styles.preview}>
          <div className={styles.title_wrapper}>
            <input
              type="text"
              maxLength={14}
              value={postName}
              className={styles.title}
              placeholder="Enter the Post's name here"
              onInput={e => setPostName(e.target.value)} />
          </div>


          <div className={styles.title_and_team_size}>
            <h3 className={styles.creatorname}>{creatorName}</h3>
            <span className={styles.team_size}>Member Limit: {memberLimit}</span>
          </div>

          <textarea
            className={styles.description}
            placeholder="Enter the Group's description here"
            value={postContent}
            onInput={e => setPostContent(e.target.value)}></textarea >

        </div>

        <button
          className={styles.create_listing}
          onClick={(e) => CreateGroupAndPost()}
        >
          Create Listing
        </button>
      </div>
    </>
  );
};

export default CreateGroup;
