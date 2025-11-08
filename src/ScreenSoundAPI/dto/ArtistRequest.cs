using System.Text;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultArtistRequest(
    string Name,
    string Bio,
    string? PerfilPhoto,
    ICollection<DefaultMusicRequest>? Musics
)
{
    public static implicit operator Artist(DefaultArtistRequest request)
    {
        return new Artist
        {
            Name = request.Name,
            Bio = request.Bio,
            PerfilPhoto = request.PerfilPhoto,
            Musics = request.Musics?.Select(r => (Music)r).ToList(),
        };
    }

    public bool Validate(out string message)
    {
        var messageBuilder = new StringBuilder("Artist request not valid:\n");
        bool isValid = true;

        if (Name is null)
        {
            messageBuilder.AppendLine("- Name need to be set");
            isValid = false;
        }

        if (Bio is null)
        {
            messageBuilder.AppendLine("- Bio need to be set");
            isValid = false;
        }

        if (Musics is not null)
        {
            var invalidMusics = Musics
                .Where(m => m.Validate(out string msg) == false)
                .Select(m =>
                {
                    m.Validate(out string msg);
                    return msg;
                });

            if (invalidMusics.Any())
            {
                isValid = false;
                foreach (var musicValidationMessage in invalidMusics)
                {
                    messageBuilder.Append(musicValidationMessage);
                }
            }
        }

        message = isValid ? string.Empty : messageBuilder.ToString();
        return isValid;
    }
}

public record UpdateArtistRequest(
    string? Name,
    string? Bio,
    string? PerfilPhoto,
    ICollection<int>? MusicsId
);
