namespace ScreenSoundCore.Modelos;

public class Artist
{
    [Obsolete("Moving code away from models!")]
    public Artist(string name, string bio)
    {
        Name = name;
        Bio = bio;
        PerfilPhoto = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    }

    public Artist()
    {
    }

    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Bio { get; set; }
    public string? PerfilPhoto { get; set; }
    public virtual ICollection<Music>? Musics { get; set; }

    [Obsolete("Moving code away from models!")]
    public void AdicionarMusica(Music musica)
    {
        Musics.Add(musica);
        musica.Artist = this;
    }

    [Obsolete("Moving code away from models!")]
    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Name}");
        foreach (var musica in Musics)
        {
            Console.WriteLine($"Música: {musica.Name}");
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
