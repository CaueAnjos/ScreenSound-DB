using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultMusicRequest(string Name, ICollection<DefaultGenreRequest> Genres)
{
    public static implicit operator Music(DefaultMusicRequest request)
    {
        return new Music
        {
            Name = request.Name,
            Genres = [.. request.Genres.Select(r => (Genre)r)],
        };
    }
}

public record UpdateMusicRequest(string? Name, DateTime? ReleaseDate, ICollection<int>? GenresId, int? ArtistId = -1);
