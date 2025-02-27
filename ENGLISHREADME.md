🌍: Elérhető nyelvek / Available languages:  
[🇬🇧:English](ENGLISHREADME.md)\
[🇭🇺:Magyar](README.md)

# Findgroup.gg
Team finder (Looking For Goup) app\
Made by:
  - Tamás Balogh
  - László Levente Papp

# Table of contents
- [Website](#website)
- [Functions](#functions)
- [Installation](#installation)
- [Database](#database)

# Website
The website of the application can be wisited: [HERE](https://example.com)
# Functions
The application's/website's functions in a nutshell:
- After logging in the users can inspect the available groups for the selected games.
- After that the users can decide if they want to join to an existing team of to create a new one.
  
### Joining to an existing one
- The user clicks on the team that he/she intreseted in.
- After that a new screen shows for the user where he/she can see the short description of the group and it's members.
- If the user decides to join the group they can do it by clicking on the **Join group** button.

### Createing a new group
- The user needs to click on the **Create a group** button.
- After that a new page appears before the user where he/she can create the team and can set the detils and the description for the group.
- If the creator of the group decides to close the team because it's full they need to click on the **Finish listing** button.
- If the user wants to  *DELETE*  the group they shall click on the **Delete group** button instead.

If the user is part of a group they can start chatting in it.\
If they click on a member of the group they can inspect it's profile where they can see the linked accounts of that user like for example their steam profile. If they click on a linked account they can copy the username of the linked account to the clipboard.

# Installation
1. Cloe the repo
`git clone https://github.com/Ordinary56/Findgroup.gg.git`
2. The `frontend` and the `backend` folder will be in the `Web/` folder
> [!IMPORTANT]
> The following commands need to be used in terminal
- [Backend](#backend)
- [Frontend](#frontend)

## Backend 

### From source
1. Migrate the database.
```
dotnet tool install --global dotnet-ef
dotnet-ef migrations add <neve>
dotnet-ef database update
```
2. Translate the programme.
`dotnet build`
3. Run the programme.

## Frontend

### From source
1. Install the dependencies with the `npm install` command.
> [!NOTE]
> If all the dependencies are not downloaded, run these as well:
```
npm install @mui/material @emotion/react @emotion/styled
npm install clsx
npm install axios
```
2. Run the programme with the `npm run dev` command.

# Database
![Database diagramm](assets/VIZSGAREMEK.png)
