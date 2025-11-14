using System.Text;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultMusicRequest(
    string Name,
    DateTime? ReleaseDate,
    ICollection<DefaultGenreRequest>? Genres
)
{
    [Obsolete("Use ToMusic instead")]
    public static implicit operator Music(DefaultMusicRequest request)
    {
        return new Music
        {
            Name = request.Name,
            ReleaseDate = request.ReleaseDate,
            Genres = request.Genres?.Select(g => (Genre)g).ToList(),
        };
    }

    public Music ToMusic(MusicsContext db)
    {
        return new Music
        {
            Name = this.Name,
            ReleaseDate = this.ReleaseDate,
            Genres = this.Genres?.Select(g => g.ToGenre(db)).ToList(),
        };
    }

    public bool Validate(out string message)
    {
        var messageBuilder = new StringBuilder("Music request not valid:\n");
        bool isValid = true;

        if (Name is null)
        {
            messageBuilder.AppendLine("- Name need to be set");
            isValid = false;
        }

        message = isValid ? string.Empty : messageBuilder.ToString();
        return isValid;
    }
}

public record UpdateMusicRequest(
    string? Name,
    DateTime? ReleaseDate,
    ICollection<int>? GenresId,
    int? ArtistId = -1
);
