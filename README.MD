
Exclude RazorPages: Add, Detail, Edit og Delete

Rename ID property i Movie til MovieId

Lav ny Domain class Genre

Lav relation mellem Movie og Genre.

Rename DbSet property fra Movie til Movies

Tilret SeedData.cs

Build og lav nye migration og slet og opdater DB

Tilret Index pagen således at der kan vises en liste med tilhørende Genre.

-----------------------------------------------

Tilføj DataLayer og ServiceLayer projekterne (.NET Standard)

Lav referencer mellem dem

Installer NuGet: 

DataLayer
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools

ServiceLayer
* Microsoft.EntityFrameworkCore

Flyt Models folderen og Context ned i DataLayer.

I ServiceLayer laves folderen MovieServices med disse 3 klasser:
* MovieDto.cs
* IMovieService.cs
* MovieService.cs

Lad MovieDto klassen indeholde de properties fra både Movie og Genrer entiteterne, som skal vises i Index

Definer metoden GetMovies, som returnerer IQueryable typet til MovieDto.

Implementer GetMovies med DI fra Contexten og lave projektion fra Movie til MovieDto klassen.

Definer oprettelsen af MovieService i Startup.cs med korrekt lifecycle.

Benyt MovieService i Index pagen og ret den til.

Build og prøv at debugge indtil de 4 film præsenteres med tilhørende genre. Filteret behøver ikke virke nu.

https://github.com/JonPSmith/EfCoreInAction/tree/Chapter10/ServiceLayer/BookServices