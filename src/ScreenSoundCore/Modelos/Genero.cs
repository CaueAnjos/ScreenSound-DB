namespace ScreenSoundCore.Modelos;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public virtual ICollection<Music>? Musics { get; set; }
}
