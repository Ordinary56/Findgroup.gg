🌍: Elérhető nyelvek / Available languages:  
[🇬🇧:English](ENGLISHREADME.md)\
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
- Ezután egy olyan képernyő fogadja ahol beállíthatja a csoport tulajdonságait és annak leírását.
- Ha a csoport létrehozója úgy dönt hogy lezárja a jelentkezést a csapatba akkor megnyomja az erre megfelelő gombot.
- Ha **TÖRÖLNI** szeretné a csapatot akkor az annak megfelelő gombot nyomja meg.

Ha a felhasználó egy csoport része akkor eltud kezdeni szövegesen beszélgetni a tagokkal.\
Ha rákattint egy felhasználóra akkor annak megjelenik a profilja amelyen megtekintheti a csatlakoztatott alkalmazások mint például a steames fiókuk. Ha rákattint az egyik csatlakoztatott alkalmazásra akkor vágólapra másolja a csatlakoztatott alkalmazáshoz tartozó nevét az adott profil tulajdonosának.

# Telepítés
1. Klónozd le a repot
`git clone https://github.com/Ordinary56/Findgroup.gg.git`
2. A `Web/` mappában lesz a frontend és a backend mappa
> [!IMPORTANT]
> Ezeket a parancsokat terminálban kell kiadni.
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
> [!NOTE]
> Ha nem jönne le az összes függőség akkor ezeket is futtasd le:
```
npm install @mui/material @emotion/react @emotion/styled
npm install clsx
npm install axios
```
2. Futtasd le a programot az `npm run dev` paranccsal

# Adatbázis
![Adatabázis diagramm](assets/VIZSGAREMEK.png)
