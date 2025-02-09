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
Ezeket a parancsokat terminálban kell kiadni.
- [Backend](#backend)
- [Frontend](#frontend)

### Backend 
(Feltételezük hogy a backend mappa van megnyitva.)
```
dotnet tool install --global dotnet-ef
dotnet-ef migrations add <neve>
dotnet-ef database update
```
### Frontend
(Feltételezük hogy a frontend mappa van megnyitva.)
```
npm install
npm install @mui/material @emotion/react @emotion/styled
npm install clsx
npm install axios
```



# Adatbázis
![Adatabázis diagramm](assets/VIZSGAREMEK.png)
