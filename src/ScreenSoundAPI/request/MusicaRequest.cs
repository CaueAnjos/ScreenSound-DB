using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record MusicaRequest(string Name, ICollection<GeneroRequest> Generos);

// NOTE: this is use on UpdateMusica!
public record UpdateMusicaRequest(int Id, string Name, DateTime Date, ICollection<GeneroRequest> Generos, int ArtistaId = -1);

public static class MusicaRequestExtations
{
    public static bool TryUpdateObject(this UpdateMusicaRequest request, IDal db)
    {
        int id = request.Id;
        var musicToUpdate = db.Musicas.GetById(id);
        if (musicToUpdate is null)
            return false;

        var artista = db.Artistas.GetById(request.ArtistaId);
        musicToUpdate.Artista = artista is not null ? artista : musicToUpdate.Artista;

        musicToUpdate.DataLancamento = request.Date != DateTime.MinValue ? request.Date : musicToUpdate.DataLancamento;

        db.Musicas.Update(musicToUpdate);
        return true;
    }

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

        ICollection<Genero> generos = [];
        if (musica.Generos.Count > 0)
        {
            generos = [.. musica.Generos.Select(g => g.ConvertToObject(db))];
        }

        obj.Generos = generos;

        db.Musicas.Add(obj);
        return obj;
    }
}
