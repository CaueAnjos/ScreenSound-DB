using System.Text;
using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultGenreRequest(string Name, string Description)
{
    public static implicit operator Genre(DefaultGenreRequest request)
    {
        return new Genre
        {
            Name = request.Name,
            Description = request.Description,
        };
    }

    public bool Validate(out string message)
    {
        var messageBuilder = new StringBuilder("Genre request not valid:\n");
        bool isValid = true;

        if (Name is null)
        {
            messageBuilder.AppendLine("- Name need to be set");
            isValid = false;
        }

        if (Description is null)
        {
            messageBuilder.AppendLine("- Description need to be set");
            isValid = false;
        }

        message = isValid ? string.Empty : messageBuilder.ToString();
        return isValid;
    }
}

[Obsolete("Use only the dto convertion methods")]
public static class GeneroRequestExtations
{
    public static Genre? TryGetObject(this DefaultGenreRequest genero, IDal db)
    {
        return db.Generos.GetSingle(g => g.Name == genero.Name);
    }

    public static Genre ConvertToObject(this DefaultGenreRequest genero, IDal db)
    {
        var obj = genero.TryGetObject(db);
        if (obj is not null)
            return obj;

        obj = new Genre()
        {
            Name = genero.Name,
            Description = genero.Description
        };

        db.Generos.Add(obj);
        return obj;
    }
}
