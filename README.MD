# Extending Movie projekt

https://github.com/aspnet/Docs/tree/master/aspnetcore/tutorials/razor-pages/razor-pages-start/2.2-stage-samples/RPmovieSearch

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

Flyt seeding over i Seeding Data i Contexten.

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

### Details Page

Opret metoden MovieDto GetMovieById(int id) i MovieService og implementer den.

Tilpas PageModel til DI af MovieService og kald af GetMovieById().

### Edit Page

Opret metoden MovieDto UpdateMovie(MovieDto movie) i MovieService og implementer den.
Lav ogs� helper-metoden bool MovieExists(int id) og implementer den.

Tilpas PageModel til DI af MovieService og kald af UpdateMovie(). Tilpas ogs� Get-delen. Tilf�j hidden felt til GenreId.

### Delete Page

Opret metoden DeleteMovie(Movie movie) i MovieService og implementer den.
Tilpas PageModel til DI af MovieService og kald af DeleteMovie(). Tilpas ogs� Get-delen.

### Create Page

Opret metoden CreateMovie(MovieDto movie) i MovieService og implementer den.
Tilpas PageModel til DI af MovieService og kald af CreateMovie().


## AutoMapper
Tilf�j Automapper Nuget til ServiceLayer.

Tilf�j Automapper.Extensions.Microsoft.DependencyInjection Nuget til WebApi:
Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection

Tilf�j f�lgende til StartUp.cs:
services.AddAutoMapper();

Tilf�j f�lgende klasse til filen Startup.cs:
```c#
public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}
```

