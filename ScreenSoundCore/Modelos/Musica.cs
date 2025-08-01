namespace ScreenSoundCore.Modelos;

public class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public virtual Artista? Artista { get; set; }
    public DateTime DataLancamento { get; set; } = DateTime.Today;

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}
        Artista: {Artista?.Nome}
        Data de lançamento: {DataLancamento}";
    }
}
