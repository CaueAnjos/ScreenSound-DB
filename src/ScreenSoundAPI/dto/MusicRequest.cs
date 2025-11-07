using System.Text;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultMusicRequest(string Name, ICollection<DefaultGenreRequest>? Genres)
{
    public static implicit operator Music(DefaultMusicRequest request)
    {
        return new Music
        {
            Name = request.Name,
            Genres = request.Genres?.Select(r => (Genre)r).ToList(),
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

        if (Genres is not null)
        {
            var invalidGenres = Genres
                .Where(g => g.Validate(out string msg) == false)
                .Select(g =>
                        {
                            g.Validate(out string msg);
                            return msg;
                        });

            if (invalidGenres.Any())
            {
                isValid = false;
                foreach (var genreValidationMessage in invalidGenres)
                {
                    messageBuilder.Append(genreValidationMessage);
                }
            }
        }

        message = isValid ? string.Empty : messageBuilder.ToString();
        return isValid;
    }
}

public record UpdateMusicRequest(string? Name, DateTime? ReleaseDate, ICollection<int>? GenresId, int? ArtistId = -1);
