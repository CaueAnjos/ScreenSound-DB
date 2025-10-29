using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultMusicRequest(string Name, ICollection<DefaultGenreRequest> Genres);
public record UpdateMusicaRequest(int Id, string Name, DateTime Date, ICollection<DefaultGenreRequest> Genres, int ArtistId = -1);

public static class MusicaRequestExtations
{
    public static bool TryUpdateObject(this UpdateMusicaRequest request, IDal db)
    {
        int id = request.Id;
        var musicToUpdate = db.Musicas.GetById(id);
        if (musicToUpdate is null)
            return false;

        var artista = db.Artistas.GetById(request.ArtistId);
        if (artista is not null)
        {
            musicToUpdate.Artist = artista;
            artista.AdicionarMusica(musicToUpdate);
        }
        else
        {
            musicToUpdate.Artist = musicToUpdate.Artist;
        }

        musicToUpdate.ReleaseDate = request.Date != DateTime.MinValue ? request.Date : musicToUpdate.ReleaseDate;
        musicToUpdate.Name = request.Name is not null ? request.Name : musicToUpdate.Name;
        musicToUpdate.Genres = [.. request.Genres.Select(g => g.ConvertToObject(db))];

        db.Musicas.Update(musicToUpdate);
        return true;
    }

    public static Music? TryGetObject(this DefaultMusicRequest musica, IDal db)
    {
        return db.Musicas.GetSingle(a => a.Name == musica.Name);
    }

    public static Music ConvertToObject(this DefaultMusicRequest musica, IDal db)
    {
        var obj = musica.TryGetObject(db);
        if (obj is not null)
            return obj;

        obj = new Music()
        {
            Name = musica.Name,
        };

        ICollection<Genre> generos = [];
        if (musica.Genres.Count > 0)
        {
            generos = [.. musica.Genres.Select(g => g.ConvertToObject(db))];
        }

        obj.Genres = generos;

        db.Musicas.Add(obj);
        return obj;
    }
}
