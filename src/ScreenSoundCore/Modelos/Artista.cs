namespace ScreenSoundCore.Modelos;

[Obsolete]
public class Artista
{
    public virtual List<Musica> Musicas { get; set; } = [];

    public Artista(string nome, string bio)
    {
        Nome = nome;
        Bio = bio;
        FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    }

    public Artista()
    {
        Nome = string.Empty;
        Bio = string.Empty;
        FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
        Id = 0;
    }

    public string Nome { get; set; }
    public string FotoPerfil { get; set; }
    public string Bio { get; set; }
    public int Id { get; set; }

    [Obsolete("Moving code away from models!")]
    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
        musica.Artista = this;
    }

    [Obsolete("Moving code away from models!")]
    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Nome}");
        foreach (var musica in Musicas)
        {
            Console.WriteLine($"Música: {musica.Nome}");
        }
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Nome}
            Foto de Perfil: {FotoPerfil}
            Bio: {Bio}";
    }
}
