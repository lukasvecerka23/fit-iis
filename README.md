# FIT IIS Projekt
## Zadání
### IoT Správa zařízení
Úkolem zadání je vytvořit jednoduchý informační systém pro správu chytrých zařízení a vyhodnocování snímaných hodnot. Každé zařízení má nějaké unikátní označení, pomocí kterého ho uživatelé budou moci vhodně odlišit, typ (např. teploměr) a další atributy (např. popis, uživatelský alias apod.). Typ zařízení je reprezentován množinou parametrů. Každý parametr je definován názvem a množinou hodnot, kterých mohou zařízení nabývat (uvažujte pouze číselné hodnoty). Zařízení je možné shlukovat do skupin (tzv. systémů), přičemž každý systém má nějaký název, popis, svého správce a uživatele, kteří monitorují stavy zařízení daného systému. Správce definuje tzv. klíčové identifikátory výkonu (KPI), což jsou funkce jejichž vstupem je hodnota zvoleného parametru a výstupem logická hodnota: v pořádku/chyba (zvolte vhodnou množinu typů funkcí - např. teplota senzoru je/není větší než 20°C -> v pořádku/chyba apod.). Uživatelé pak monitorují, zda jsou pro dané KPI všechna zařízení v pořádku, případně některé z nich/všechny jsou ve stavu chyby. Uživatelé budou moci dále informační systém používat následujícím způsobem:

- `administrátor`
    - spravuje uživatele
    - má práva všech následujících rolí
- `registrovaný uživatel`
    - zakládá systémy - stává se správcem systému
    - registruje nová zařízení
    - definuje KPI
    - sdílí systém s jinými uživateli
    - posílá žádosti o sdílení systému - po nasdílení se stává uživatelem systému
- `uživatelem systému`
    - monitoruje stavy zařízení a KPI
    - prochází zařízení systému
- `neregistrovaný uživatel`
    - prochází systémy - vidí základní metadata systému
- `broker`:
    - uživatel, který bude moci aktualizovat hodnoty zařízení
představuje simulaci aktualizace hodnot zařízení

## Lokální vývoj
### Prerekvizity
- nainstalovaný Docker https://docs.docker.com/engine/install/

### Spuštění MS SQL
- v root složce spustit `docker-compose up -d`

### Přidání connection stringu do User secrets
- Connection string
```
"Server=<host>,<port>;Database=<db_name>;User Id=<user>;Password=<passwd>;TrustServerCertificate=True;"
```
- Default connection
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<connection string>"
```
- Test connection
```bash
# Přidání testovacího connection stringu do User secrets
dotnet user-secrets set "ConnectionStrings:TestConnection" "<connection string>"
# V connection stringu je potřeba oproti DefaultConnection vynechat "Database=..."
# Doplňuje se automaticky při testování, aby se předešlo kolizi za sebou vykonávaných testů
```

### Aplikace migrace
```bash
dotnet ef database update
```