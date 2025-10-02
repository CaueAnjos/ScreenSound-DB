using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record ArtistaRequest(string Name, string Bio, ICollection<MusicaRequest> Musics);

// NOTE: this is used for UpdateArtista endpoint 
public record UpdateArtistaRequest(int Id, string Name, string Bio, string FotoPerfil, ICollection<MusicaRequest> Musics);

public static class ArtistaRequestExtations
{
    public static bool TryUpdateObject(this UpdateArtistaRequest request, IDal db)
    {
        int id = request.Id;
        var artistToUpdate = db.Artistas.GetById(id);
        if (artistToUpdate is null)
            return false;

        List<Musica> musicas = [];
        if (request.Musics.Count > 0)
        {
            musicas = [.. request.Musics.Select(m => m.ConvertToObject(db))];
        }

        artistToUpdate.Musicas = musicas;
        artistToUpdate.Nome = request.Name;
        artistToUpdate.FotoPerfil = request.FotoPerfil;
        artistToUpdate.Bio = request.Bio;

        db.Artistas.Update(artistToUpdate);
        return true;
    }

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

        List<Musica> musicas = [];
        if (artista.Musics.Count > 0)
        {
            musicas = [.. artista.Musics.Select(m => m.ConvertToObject(db))];
        }

        obj.Musicas = musicas;

        db.Artistas.Add(obj);
        return obj;
    }
}
