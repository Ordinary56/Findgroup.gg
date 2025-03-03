import React, { useEffect, useState } from "react";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import clsx from "clsx";
import PostList from "../../PostList/PostList";
import homeStyles from "./home.module.css";
import useCategories from "../../../hooks/useCategories";

const Home: React.FC = () => {
  const { categories, loading, error } = useCategories();
  console.log(categories, loading, error);
  const [selectedCategory, setSelectedCategory] = useState<string>("");
  useEffect(() => {
    if (categories && categories.length > 0) {
      setSelectedCategory(categories[0].categoryName);
    }
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
    </div>
  );
};

export default Home;
