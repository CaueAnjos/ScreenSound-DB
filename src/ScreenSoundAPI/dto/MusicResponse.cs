using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultMusicResponse(
    int Id,
    string Name,
    DateTime? ReleaseDate,
    int? ArtistId,
    ICollection<DefaultGenreResponse>? Genres
)
{
    public static implicit operator DefaultMusicResponse(Music music)
    {
        return new DefaultMusicResponse(
            music.Id,
            music.Name,
            music.ReleaseDate,
            music.Artist?.Id,
            music.Genres?.Select(g => (DefaultGenreResponse)g).ToArray()
        );
    }
}

public static class MusicaResponseExtations
{
    public static DefaultMusicResponse GetResponse(this Music musica)
    {
        int? artistaId = musica.Artist?.Id ?? null;
        var generos = musica.Genres?.Select(g => g.GetResponse()).ToList();
        return new DefaultMusicResponse(
            musica.Id,
            musica.Name,
            musica.ReleaseDate,
            artistaId,
            generos
        );
    }
}
