import * as React from 'react';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import styles from "./module.css/home.module.css"
import clsx from 'clsx';
export default function Home() {
  const [game, setgame] = React.useState('League of legends');

  const handleChange = (
    event: React.MouseEvent<HTMLElement>,
    newgame: string,
  ) => {
    setgame(newgame);
  };

  return (
    <ToggleButtonGroup
    className={clsx(styles.gamechooser)}
      value={game}
        onChange={handleChange}>
      <ToggleButton  value="League of legends" className={clsx(styles.gamechooserItem)}>League of legends</ToggleButton>
      <ToggleButton value="Apex legends" className={clsx(styles.gamechooserItem)} >Apex legends</ToggleButton>

    </ToggleButtonGroup>
  );
}
