# CetStudentBook

ASP.NET MVC + Entity Framework Code First project for managing students and books.

---

## Assignment: Books CRUD

### What I implemented

| Feature | Route | Description |
|---|---|---|
| List | `/Books` | Table of all books with Edit & Delete actions |
| Create | `/Books/Create` | Form to add a new book with full validation |
| Edit | `/Books/Edit/{id}` | Pre-filled form to update a book, shows errors on failure |
| Delete | `/Books/Delete/{id}` | Confirmation screen showing book details before deleting |

**Key implementation details:**
- `Book` model in `Models/Book.cs` with Data Annotations (`[Required]`, `[StringLength]`, `[Range]`, `[DataType]`)
- `BooksController` in `Controllers/BookController.cs` — all actions written by hand (no scaffolding)
- Views in `Views/Books/` — 4 hand-written Razor views (Index, Create, Edit, Delete)
- `DbSet<Book> Books` added to `ApplicationDbContext`
- EF Code First migration (`InitialCreate`) creates the `Books` table in SQLite
- "Books" navigation link added to `Views/Shared/_Layout.cshtml`
- Client-side + server-side validation via jQuery Unobtrusive Validation + Data Annotations

---

### How to run the project locally

**Prerequisites:**
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQLite (included via NuGet — no install needed)

**Steps:**

```bash
# 1. Clone the repository
git clone https://github.com/SemihMutlu07/CetStudentBook.git
cd CetStudentBook

# 2. Switch to the assignment branch
git checkout assignment-books

# 3. Go into the project folder
cd CetStudentBook

# 4. Apply database migrations (creates app.db with all tables)
dotnet ef database update

# 5. Run the app
dotnet run
```

Then open your browser at `http://localhost:5176` (or the port shown in the terminal).

---

### Database / Migration steps

The project uses **SQLite** with Entity Framework Code First.

```bash
# Apply existing migrations (creates the DB from scratch)
dotnet ef database update

# If you want to see the current migration status
dotnet ef migrations list

# If you need to add a new migration after changing a model
dotnet ef migrations add YourMigrationName

# Roll back to a specific migration
dotnet ef database update MigrationName
```

The migration file is at:
```
CetStudentBook/Data/Migrations/*_InitialCreate.cs
```

It creates all tables including `Books`:
```sql
CREATE TABLE "Books" (
    "Id"           INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Name"         TEXT    NOT NULL,
    "Author"       TEXT    NOT NULL,
    "PublishDate"  TEXT    NOT NULL,
    "PageCount"    INTEGER NOT NULL,
    "IsSecondHand" INTEGER NOT NULL
);
```

---

### Screenshots

> Add screenshots here after running the app locally.
> Suggested: Books list page, Create page with a validation error, Delete confirmation page.

| Page | Description |
|---|---|
| `/Books` | List of all books with Edit/Delete buttons |
| `/Books/Create` | Create form — try submitting empty to see validation errors |
| `/Books/Edit/{id}` | Edit form pre-filled with current values |
| `/Books/Delete/{id}` | Confirmation card before deletion |
