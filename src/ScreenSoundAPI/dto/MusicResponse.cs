using ScreenSoundAPI.dto;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record MusicaResponse(int Id, string Name, DateTime? DataLancamento, int? ArtistaId, ICollection<DefaultGenreResponse> Generos);

public static class MusicaResponseExtations
{
    public static MusicaResponse GetResponse(this Music musica)
    {
        int? artistaId = musica.Artist?.Id ?? null;
        var generos = musica.Genres.Select(g => g.GetResponse()).ToList();
        return new MusicaResponse(musica.Id, musica.Name, musica.ReleaseDate, artistaId, generos);
    }
}
