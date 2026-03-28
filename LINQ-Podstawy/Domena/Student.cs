namespace LINQ_Podstawy.Domena;

public record Student
{
    public int Id { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string NumerIndeksu { get; set; }
    public DateOnly DataUrodzenia { get; set; }
    public required List<int> Oceny { get; set; }

    public override string ToString()
    {
        return
            $"Student {{ Id = {Id}, Imie = {Imie}, Nazwisko = {Nazwisko}, NumerIndeksu = {NumerIndeksu}, DataUrodzenia = {DataUrodzenia}, Oceny = [{string.Join(", ", Oceny)}] }}";
    }
}