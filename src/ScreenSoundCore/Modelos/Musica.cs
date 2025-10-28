namespace ScreenSoundCore.Modelos;

public class Music
{
    [Obsolete("Moving code away from models!")]
    public Music(string nome)
    {
        Name = nome;
        Genres = new List<Genre>();
    }

    public Music()
    {
    }

    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public virtual Artist? Artist { get; set; }
    public virtual ICollection<Genre>? Genres { get; set; }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Name}
        Artista: {Artist?.Name}
        Data de lançamento: {ReleaseDate}";
    }
}
