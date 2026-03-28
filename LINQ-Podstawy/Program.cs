using LINQ_Podstawy;

/*
   * LINQ to mechanizm znany z platformy .NET,
   * który pozwala pisać zapytania do danych bezpośrednio w kodzie w sposób czytelny i deklaratywny — trochę jak SQL,
   * ale wbudowany w język. Używamy go, aby pracować na kolekcjach danych,
   * bez potrzeby ręcznego iterowania i wykonywania na nich operacji.
   * Zapytania LINQ najczęściej układamy w łańcuchy, które pozwalają na wykonanie skomplikowanych operacji na danych
   * w ramach jednego łańcucha operacji.
   *
   * Metody LINQ możemy uruchamiać na kolekcjach, które implementują interfejs IEnumerable,
   * czyli np. listach, tablicach czy wynikach innych zapytań LINQ.
   *
   * Ważne: LINQ jest leniwe (deferred execution) – najpierw buduje zapytanie, a wykonuje je dopiero
   * w momencie jego użycia (np. foreach, ToList, Count, FirstOrDefault).
   * /

// [Przykład zastosowania]:
/*
 * Pobierz studentów urodzonych w roku >= 2000
 */
{
    var rezultat = Dane.Studenci.Where(stud => stud.DataUrodzenia.Year >= 2000);
    Wypisz(rezultat);
}

// [Najważniejsze metody]:
// 1. Select - projekcja/mapowanie danych jednego typu na inny
{
    Console.WriteLine("\n[Select] -------------------------------------------------------");

    Console.WriteLine("Przemapowanie kolekcji studentów na kolekcję zawierającą numery indeksów studentów (kolekcję stringów)");
    var rezultat = Dane.Studenci.Select(stud => stud.NumerIndeksu);
    Wypisz(rezultat);
    
    Console.WriteLine();
    
    Console.WriteLine("Przemapowanie kolekcji studentów na kolekcję obiektów anonimowych z imionami oraz nazwiskami");
    var rezultat2 = Dane.Studenci
        .Select(stud => new {Imie = stud.Imie, Nazwisko = stud.Nazwisko});
    Wypisz(rezultat2);
}

// 2. Where - filtrowanie kolekcji
{
    Console.WriteLine("\n[Where] -------------------------------------------------------");
    
    Console.WriteLine("Pobranie studentów o imieniu zaczynającym się od litery 'A'");
    var rezultat = Dane.Studenci.Where(stud => stud.Imie.StartsWith('A'));
    Wypisz(rezultat);
}

// 3. OrderBy / ThenBy / OrderByDescending / ThenByDescending - sortowanie
{
    Console.WriteLine("\n[OrderBy] -------------------------------------------------------");
    
    Console.WriteLine("Posortowanie kolekcji rosnąco po imionach studentów, a następnie malejąco po nazwiskach");
    var rezultat = Dane.Studenci
        .OrderBy(stud => stud.Imie)
        .ThenByDescending(stud => stud.Nazwisko);
    Wypisz(rezultat);
}

// 4. FirstOrDefault - pobranie pojedynczego (pierwszego) elementu z kolekcji
{
    Console.WriteLine("\n[FirstOrDefault] -------------------------------------------------------");
    
    Console.WriteLine("Pobranie pierwszego studenta z domyślnej listy");
    var rezultat = Dane.Studenci.FirstOrDefault();
    Console.WriteLine(rezultat);
    
    Console.WriteLine();
    
    Console.WriteLine("Pobranie pierwszego studenta, którego rok urodzenia jest większy od 5000 - nie ma takiego, więc metoda zwraca wartość domyślną dla typu Student, czyli null");
    // FirstOrDefault pozwala na przekazanie warunku do przefiltrowania kolekcji. Jest to równorzędne z przypadkiem,
    // gdzie wywołalibyśmy najpierw metodę Where, a później pustą metodę FirstOrDefault.
    var rezultat2 = Dane.Studenci.FirstOrDefault(stud => stud.DataUrodzenia.Year >= 5000);
    Console.WriteLine(rezultat2?.ToString() ?? "Nic tu nie ma");
}

// 5. Any / All - sprawdzanie warunku dla całej kolekcji
{
    Console.WriteLine("\n[Any/All] -------------------------------------------------------");
    
    Console.WriteLine("Czy wszyscy studenci mają nazwiska kończące się literą 'a'?");
    var rezultat = Dane.Studenci.All(stud => stud.Nazwisko.EndsWith('a'));
    Console.WriteLine(rezultat);

    Console.WriteLine();

    Console.WriteLine("Czy jakikolwiek student ma nazwisko kończące się literą 'a'?");
    var rezultat2 = Dane.Studenci.Any(stud => stud.Nazwisko.EndsWith('a'));
    Console.WriteLine(rezultat2);
}

// 6. Count - zliczanie elementów
{
    Console.WriteLine("\n[Count] -------------------------------------------------------");

    Console.WriteLine("Ilu studentów ma nazwisko kończące się literą 'a'?");
    var rezultat = Dane.Studenci.Count(stud => stud.Nazwisko.EndsWith('a'));
    Console.WriteLine(rezultat);

    Console.WriteLine();

    Console.WriteLine("Ilu studentów ma imię zaczynające się od 'E'?");
    var rezultat2 = Dane.Studenci.Where(stud => stud.Imie.StartsWith('E')).Count();
    Console.WriteLine(rezultat2);
    
}

// 7. Distinct - usuwanie duplikatów
{
    Console.WriteLine("\n[Distinct] -------------------------------------------------------");

    var list = new List<int> { 1, 1, 2, 2, 3, 4, 5 };
    
    Console.WriteLine("Bez użycia distinct:");
    Console.WriteLine(string.Join(", ", list));

    Console.WriteLine("\nZ użyciem distinct:");
    Console.WriteLine(string.Join(", ", list.Distinct()));

    Console.WriteLine();
}

// 8. Skip i Take - pomijanie i pobieranie konkretnej liczby rekordów
{
    Console.WriteLine("\n[Skip i Take] -------------------------------------------------------");

    Console.WriteLine("Pominięcie dwóch pierwszych studentów z kolekcji i pobranie dwóch kolejnych");
    var rezultat = Dane.Studenci.Skip(2).Take(2);
    Wypisz(rezultat);
}

// 9. Join - łączenie danych z dwóch kolekcji na podstawie pary kluczów (Inner join z SQL'a)
{
    Console.WriteLine("\n[Join] -------------------------------------------------------");

    // Wywołanie metody join na kolekcji (source), wymaga 4 parametrów:
    /*
     * - Kolekcji (target), do której próbujemy się połączyć
     * - wskazania klucza z kolekcji source
     * - wskazania klucza z kolekcji target
     * - opisania mapowania połączonych wartości na nowy obiekt (coś jak wcześniej wspomniany Select)
     */
    
    Console.WriteLine("Połączenie studentów i przedmiotów, do których są zapisani");
    var rezultat2 = Dane.Studenci
        .Join(
            Dane.Zapisy,
            student => student.Id,
            zapis => zapis.IdStudenta,
            (stud, zapis) => new { Student = stud, IdPrzedmiotu = zapis.IdPrzedmiotu }
        )
        .Join(
            Dane.Przedmioty,
            studentPrzedmiot => studentPrzedmiot.IdPrzedmiotu,
            przedmiot => przedmiot.Id,
            (studentPrzedmiot, przedmiot) => new { Student = studentPrzedmiot.Student, Przedmiot = przedmiot }
        );
    Wypisz(rezultat2);
}

// 10. GroupBy - grupowanie danych
{
    Console.WriteLine("\n[GroupBy] -------------------------------------------------------");

    Console.WriteLine("Pogrupowanie studentów ze względu na ostatnią literę nazwiska");
    var rezultat = Dane.Studenci
        .GroupBy(stud => stud.Nazwisko.Last())
        .Select(grupy => new { OstatniaLitera = grupy.Key, Studenci = grupy.ToList() });

    foreach (var grupa in rezultat)
    {
        Console.WriteLine($"Litera: {grupa.OstatniaLitera}, Studenci: {string.Join(", ", grupa.Studenci)}");
    }
}

// 11. SelectMany - spłaszczanie danych
{
    Console.WriteLine("\n[SelectMany] -------------------------------------------------------");

    // SelectMany służy do "spłaszczenia" relacji, gdzie obiekt zawiera kolekcję jakichś danych.
    // np. Student z kolekcją zawierającą 3 oceny => 3 razy oddzielny rekord studenta z osobnymi ocenami
    
    var rezultat = Dane.Studenci
        .SelectMany(
            student => student.Oceny,
            (student, ocena) => new { Student = student.NumerIndeksu, Ocena = ocena }
        );
    Wypisz(rezultat);
}

// 12. Average / Max - podstawowe agregacje
{
    Console.WriteLine("\n[Average / Max] -------------------------------------------------------");

    Console.WriteLine("Średnia ocen każdego studenta:");
    var rezultat = Dane.Studenci.Select(e => new { Indeks = e.NumerIndeksu, SredniaOcen = e.Oceny.Average() });
    Wypisz(rezultat);

    Console.WriteLine();
    
    Console.WriteLine("Najwyższa ocena każdego studenta:");
    var rezultat2 = Dane.Studenci.Select(e => new { Indeks = e.NumerIndeksu, NajwyzszaOcena = e.Oceny.Max() });
    Wypisz(rezultat2);
}


void Wypisz<T>(IEnumerable<T> kolekcja)
{
    foreach (var rekord in kolekcja)
    {
        Console.WriteLine(rekord);
    }
}