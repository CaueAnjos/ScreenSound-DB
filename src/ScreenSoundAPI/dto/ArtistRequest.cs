using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultArtistRequest(string Name, string Bio, string PerfilPhoto, ICollection<DefaultMusicRequest> Musics)
{
    public static implicit operator Artist(DefaultArtistRequest request)
    {
        return new Artist
        {
            Name = request.Name,
            Bio = request.Bio,
            PerfilPhoto = request.PerfilPhoto,
            Musics = [.. request.Musics.Select(r => (Music)r)],
        };
    }
}

public record UpdateArtistaRequest(int Id, string Name, string Bio, string PerfilPhoto, ICollection<DefaultMusicRequest> Musics);

[Obsolete("Use only the dto convertion methods")]
public static class ArtistaRequestExtations
{
    public static bool TryUpdateObject(this UpdateArtistaRequest request, IDal db)
    {
        int id = request.Id;
        var artistToUpdate = db.Artistas.GetById(id);
        if (artistToUpdate is null)
            return false;

        List<Music> musicas = [];
        if (request.Musics.Count > 0)
        {
            musicas = [.. request.Musics.Select(m => m.ConvertToObject(db))];
        }

        artistToUpdate.Musics = musicas.Count > 0 ? musicas : artistToUpdate.Musics;
        artistToUpdate.Name = request.Name is not null ? request.Name : artistToUpdate.Name;
        artistToUpdate.PerfilPhoto = request.PerfilPhoto is not null ? request.PerfilPhoto : artistToUpdate.PerfilPhoto;
        artistToUpdate.Bio = request.Bio is not null ? request.Bio : artistToUpdate.Bio;

        db.Artistas.Update(artistToUpdate);
        return true;
    }

    public static Artist? TryGetObject(this DefaultArtistRequest artista, IDal db)
    {
        return db.Artistas.GetSingle(a => a.Name == artista.Name);
    }

    public static Artist ConvertToObject(this DefaultArtistRequest artista, IDal db)
    {
        var obj = artista.TryGetObject(db);
        if (obj is not null)
            return obj;

        obj = new Artist()
        {
            Name = artista.Name,
            Bio = artista.Bio,
        };

        List<Music> musicas = [];
        if (artista.Musics.Count > 0)
        {
            musicas = [.. artista.Musics.Select(m => m.ConvertToObject(db))];
        }

        obj.Musics = musicas;

        db.Artistas.Add(obj);
        return obj;
    }
}
