using System.Text;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultMusicRequest(string Name)
{
    public static implicit operator Music(DefaultMusicRequest request)
    {
        return new Music { Name = request.Name };
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
