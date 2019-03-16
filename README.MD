# Extending Movie projekt

Exclude RazorPages: Add, Detail, Edit og Delete

Rename ID property i Movie til MovieId

Lav ny Domain class Genre

Lav relation mellem Movie og Genre.

Rename DbSet property fra Movie til Movies

Tilret SeedData.cs

Build og lav nye migration og slet og opdater DB

Tilret Index pagen s�ledes at der kan vises en liste med tilh�rende Genre.

-----------------------------------------------
### Opret arkitektur og Service

Tilf�j DataLayer og ServiceLayer projekterne (.NET Standard)

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

Lad MovieDto klassen indeholde de properties fra b�de Movie og Genrer entiteterne, som skal vises i Index (alle undtagen navigation)

Definer metoden GetMovies(), som returnerer IQueryable typet til MovieDto.

Implementer GetMovies med DI fra Contexten og lave projektion fra Movie til MovieDto klassen.

Definer oprettelsen af MovieService i Startup.cs med korrekt lifecycle.

Benyt MovieService i Index pagen og ret den til.

Build og pr�v at debugge indtil de 4 film pr�senteres med tilh�rende genre. Filteret beh�ver ikke virke nu.

https://github.com/JonPSmith/EfCoreInAction/tree/Chapter10/ServiceLayer/BookServices

----------------------------------------------

### Genre Service

Udvid MovieService med GenreService og metoden GetGenres(). Lad den returnere en IEnumerable<Genre>.

I Index.cs oprettes et object af typen SelectList, som benytter GetGenres() og s�tter de to properties til GenreId og GenreText.

Test at Select-listen bliver befolket.


### Search

Udvid MovieService med GetMovies(string searchString, int genreId).

I GetMovies skal der f�rst laves en Projection til DTO-klassen, derefter testes p� searchString og genreId parametrene.
Til sidst kan den samlede query returneres. Men den eksekveres oppe i PageModel.

Nu skal man kunne s�ge b�de p� Genre og fritekst p� titlen.

-----------------------------------------------------------
