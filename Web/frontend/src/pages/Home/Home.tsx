import React, { use, useEffect, useState } from "react";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import clsx from "clsx";
import PostList from "../../component/PostList/PostList";
import homeStyles from "./home.module.css";
import useCategories from "../../hooks/useCategories";
import CreateButton from "../../component/Create_group_button/Create_group_button";
import { apiService } from "../../api/apiService";
import { UserInfo } from "../../api/Models/UserInfo";
import { Group } from "../../api/Models/Group";
import { Link } from "react-router-dom";

const Home: React.FC = () => {
  const { categories, loading, error } = useCategories();
  console.log(categories, loading, error);
  const [selectedCategory, setSelectedCategory] = useState<string>("");
  const [userInfo, setUserInfo] = useState<UserInfo>();
  const [groups, setGroups] = useState<Group[]>()
  useEffect(() => {
    if (categories && categories.length > 0) {
      setSelectedCategory(categories[0].categoryName);
    }
    const fetchUserAndGroups = async () => {
      try {
        const userInfo = await apiService.getUserInfo();
        setUserInfo(userInfo);
        const groups = await apiService.getGroups();
        console.log(groups);
        setGroups(groups.filter(x => x.creator?.id === userInfo.nameidentifier || x.users.some(x => x.id === userInfo.nameidentifier)));
      }
      catch (err) {
        console.log(error);
      }
    }
    fetchUserAndGroups();
  }, [categories]);

  const handleGameChange = (
    event: React.MouseEvent<HTMLElement>,
    newGame: string | null
  ) => {
    if (newGame) setSelectedCategory(newGame);
  };

  return (
    <div className={homeStyles.container}>
      <ToggleButtonGroup
        value={selectedCategory}
        exclusive
        onChange={handleGameChange}
        className={homeStyles.gamechooser}
      >
        {categories.map((category) => (
          <ToggleButton
            key={category.id}
            value={category.categoryName}
            className={clsx(
              homeStyles.gamechooserItem,
              selectedCategory !== category.categoryName && homeStyles.untoggled
            )}
          >
            {category.categoryName}
          </ToggleButton>
        ))}
      </ToggleButtonGroup>
      <div className={homeStyles.postListWrapper}>
        <PostList selectedGame={selectedCategory} />
      </div>

      <div className={homeStyles.YourGroups}>
        
        <h1>Your groups</h1>
        {/*  TODO: Improve upon this UI further*/}
        <div>
       
          
        {groups?.map(group => (
          <Link  to={`/group/${group.id}`}>{group.groupName}</Link>
        )) || "You didn't join any group yet"}
       
        </div>
      </div>
        <div className={homeStyles.createbutton}>
        <CreateButton/>
        </div>
    </div>
  );
};

export default Home;
