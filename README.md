🌍: Elérhető nyelvek / Available languages:  
[🇬🇧:English](docs/ENGLISHREADME.md)\
[🇭🇺:Magyar](README.md)

# Findgroup.gg
Csapatkereső (Looking For Goup) alkalmazás\
Készítette:
  - Balogh Tamás
  - Papp László Levente

# Tartalomjegyzék
- [Weboldal](#weboldal)
- [Funkciók](#funkciók)
- [Telepítés](#telepítés)
- [Adatbázis](#adatbázis)

# Weboldal
Az alkalmazás weboldala meglátogatható az alábbi linken: [LINK IDE](https://example.com)
# Funkciók
Az alkalmazás/oldal főbb funkciói röviden:
- A felhasználók bejelentkezés után meg tekinthetik a kiválasztott játékhoz tartozó már létrehozott csoportokat.
- Ez után eldönthetik hogy létre akarnak-e hozni egyet vagy éppen csatlakozni egy meglévőhöz.
  
### Csatlakozás egy meglévőhöz
- A felhasználó rákattint a csoportra amibe csatlakozni akar
- Ezután egy olyan képernyő fogadja ahol látja a csapat rövid leírását és tagjait annak.
- Ha ezután eldöntötte hogy csatlakozik akkor rányom a **Join group** gombra.

### Csoport létrehozása
- A felhasználó rákattint a **Create a group** gombra
- Ezután egy olyan képernyő fogadja ahol látja a csapat rövid leírását és tagjait annak.
- Ha ezután eldöntötte hogy csatlakozik akkor rányom a csatlakozás gombra.

# Telepítés
1. Klónozd le a repot
`git clone https://github.com/Ordinary56/Findgroup.gg.git`
2. A `Web/` mappában lesz a frontend és a backend mappa
Ezeket a parancsokat terminálban kell kiadni.
- [Backend](#backend)
- [Frontend](#frontend)

## Backend 

### Forrásból
1. Migráld az adatbázist.
```
dotnet tool install --global dotnet-ef
dotnet-ef migrations add <neve>
dotnet-ef database update
```
2. Fordíts le a programot.
`dotnet build`
3. Futtasd a programot.

## Frontend

### Forrásból
1. Telepítsd a függőségeket az `npm install` parancsal.\
Ha nem jönne le az összes függőség akkor ezeket is futtasd le:
```
npm install @mui/material @emotion/react @emotion/styled
npm install clsx
npm install axios
```
2. Futtasd le a programot az `npm run dev` paranccsal

# Adatbázis
![Adatabázis diagramm](assets/VIZSGAREMEK.png)
