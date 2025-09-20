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
            Musicas = [.. artista.Musics.Select(m => m.ConvertToObject(db))]
        };

        db.Artistas.Add(obj);
        return obj;
    }
}
