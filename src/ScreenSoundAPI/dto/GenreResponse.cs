using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultGenreResponse(string Name, string Description)
{
    public static implicit operator DefaultGenreResponse(Genre genre)
    {
        return new DefaultGenreResponse(genre.Name, genre.Description);
    }
}

public static class GeneroResponseExtations
{
    public static DefaultGenreResponse GetResponse(this Genre genero)
    {
        return new DefaultGenreResponse(genero.Name, genero.Description);
    }
}
