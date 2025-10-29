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
