Простой проект для примера
Clean architecture + DDD + CQRS.

Для запуска нужен LocalDB (Или изменить строку подключения),
а так-же сделать миграцию ef:

```bash
dotnet ef database update --project src/Infrastructure
```
