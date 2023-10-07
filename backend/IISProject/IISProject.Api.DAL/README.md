## Migrace
- https://learn.microsoft.com/cs-cz/ef/core/cli/dotnet#update-the-tools
- Vytvoření nové migrace
```bash
dotnet ef migrations add [jmeno migrace]
```
- Smazání poslední migrace
```bash
dotnet ef migrations remove
```
- Aplikace migrace na databázi
```bash
dotnet ef database update
```
- Smazání databáze
```bash
dotnet ef database drop
```