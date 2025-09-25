using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record ArtistaRequest(string Name, string Bio, ICollection<MusicaRequest> Musics);

public static class ArtistaRequestExtations
{
    public static Artista? TryGetObject(this ArtistaRequest artista, IDal db)
    {
        return db.Artistas.GetSingle(a => a.Nome == artista.Name);
    }

    public static Artista ConvertToObject(this ArtistaRequest artista, IDal db)
    {
        var obj = artista.TryGetObject(db);
        if (obj is not null)
            return obj;

        obj = new Artista()
        {
            Nome = artista.Name,
            Bio = artista.Bio,
        };
        db.Artistas.Add(obj);

        List<Musica> musicas = [];
        if (artista.Musics.Count > 0)
        {
            musicas = [.. artista.Musics.Select(m => m.ConvertToObject(db))];
        }

        obj.Musicas = musicas;

        return obj;
    }
}
