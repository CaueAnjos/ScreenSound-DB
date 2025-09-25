using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record MusicaRequest(string Name, ICollection<GeneroRequest> Generos);

public static class MusicaRequestExtations
{
    public static Musica? TryGetObject(this MusicaRequest musica, IDal db)
    {
        return db.Musicas.GetSingle(a => a.Nome == musica.Name);
    }

    public static Musica ConvertToObject(this MusicaRequest musica, IDal db)
    {
        var obj = musica.TryGetObject(db);
        if (obj is not null)
            return obj;

        obj = new Musica()
        {
            Nome = musica.Name,
        };
        db.Musicas.Add(obj);

        ICollection<Genero> generos = [];
        if (musica.Generos.Count > 0)
        {
            generos = [.. musica.Generos.Select(g => g.ConvertToObject(db))];
        }

        obj.Generos = generos;

        return obj;
    }
}
