namespace ScreenSoundCore.Modelos;

public class Genero
{
    public Genero()
    {
        Nome = string.Empty;
        Descricao = string.Empty;
        Musicas = new List<Music>();
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Id { get; set; }
    public virtual ICollection<Music> Musicas { get; set; }
}
