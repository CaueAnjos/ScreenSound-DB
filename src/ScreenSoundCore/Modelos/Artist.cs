namespace ScreenSoundCore.Modelos;

public class Artist
{
    public virtual ICollection<Musica> Musics { get; set; } = [];

    public Artist(string name, string bio)
    {
        Name = name;
        Bio = bio;
        PerfilPhoto = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    }

    public Artist()
    {
        Name = string.Empty;
        Bio = string.Empty;
        PerfilPhoto = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
        Id = 0;
    }

    public string Name { get; set; }
    public string PerfilPhoto { get; set; }
    public string Bio { get; set; }
    public int Id { get; set; }

    [Obsolete("Moving code away from models!")]
    public void AdicionarMusica(Musica musica)
    {
        Musics.Add(musica);
        musica.Artista = this;
    }

    [Obsolete("Moving code away from models!")]
    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Name}");
        foreach (var musica in Musics)
        {
            Console.WriteLine($"Música: {musica.Nome}");
        }
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Name}
            Foto de Perfil: {PerfilPhoto}
            Bio: {Bio}";
    }
}
