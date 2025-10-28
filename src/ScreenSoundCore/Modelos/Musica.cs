namespace ScreenSoundCore.Modelos;

public class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
        Generos = new List<Genero>();
    }

    public Musica()
    {
        Nome = string.Empty;
        this.Artista = null;
        Id = 0;
        Generos = new List<Genero>();
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public virtual Artist? Artista { get; set; }
    public DateTime DataLancamento { get; set; } = DateTime.Today;
    public virtual ICollection<Genero> Generos { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}
        Artista: {Artista?.Name}
        Data de lançamento: {DataLancamento}";
    }
}
