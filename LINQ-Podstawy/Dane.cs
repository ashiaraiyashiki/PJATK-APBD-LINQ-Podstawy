using LINQ_Podstawy.Domena;

namespace LINQ_Podstawy;

public static class Dane
{
    public static readonly IEnumerable<Student> Studenci;
    public static readonly IEnumerable<Przedmiot> Przedmioty;
    public static readonly IEnumerable<ZapisNaPrzedmiot> Zapisy;

    static Dane()
    {
        Studenci = new List<Student>
        {
            new() { Id = 1, Imie = "Anna", Nazwisko = "Kowalska", NumerIndeksu = "S123456", DataUrodzenia = DateOnly.Parse("2000-01-15"), Oceny = [2,2,3]},
            new() { Id = 2, Imie = "Jan", Nazwisko = "Nowak", NumerIndeksu = "S654321", DataUrodzenia = DateOnly.Parse("1998-05-20"), Oceny = [4,3,2]},
            new() { Id = 3, Imie = "Maria", Nazwisko = "Wiśniewska", NumerIndeksu = "S111222", DataUrodzenia = DateOnly.Parse("2001-08-10"), Oceny = [1,2,3]},
            new() { Id = 4, Imie = "Piotr", Nazwisko = "Kaczmarek", NumerIndeksu = "S333444", DataUrodzenia = DateOnly.Parse("1999-12-25"), Oceny = [3,3,2]},
            new() { Id = 5, Imie = "Ewa", Nazwisko = "Majewska", NumerIndeksu = "S777888", DataUrodzenia = DateOnly.Parse("2002-03-01"), Oceny = [3,2,3]}
        };

        Przedmioty = new List<Przedmiot>
        {
            new() { Id = 1, Nazwa = "Programowanie C#" },
            new() { Id = 2, Nazwa = "Analiza Matematyczna" },
            new() { Id = 3, Nazwa = "Teoria Grafów" },
            new() { Id = 4, Nazwa = "Bazy Danych" },
            new() { Id = 5, Nazwa = "Algorytmy i Struktury Danych" }
        };

        Zapisy = new List<ZapisNaPrzedmiot>
        {
            new() { IdStudenta = 1, IdPrzedmiotu = 1 },
            new() { IdStudenta = 2, IdPrzedmiotu = 2 },
            new() { IdStudenta = 3, IdPrzedmiotu = 3 },
            new() { IdStudenta = 4, IdPrzedmiotu = 4 },
            new() { IdStudenta = 5, IdPrzedmiotu = 5 },
            new() { IdStudenta = 1, IdPrzedmiotu = 2 },
            new() { IdStudenta = 2, IdPrzedmiotu = 3 },
            new() { IdStudenta = 3, IdPrzedmiotu = 4 },
            new() { IdStudenta = 4, IdPrzedmiotu = 5 },
            new() { IdStudenta = 5, IdPrzedmiotu = 1 }
        };
    }
}