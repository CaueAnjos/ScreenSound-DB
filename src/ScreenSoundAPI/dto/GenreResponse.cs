using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultGenreResponse(string Name, string Description);

public static class GeneroResponseExtations
{
    public static DefaultGenreResponse GetResponse(this Genre genero)
    {
        return new DefaultGenreResponse(genero.Name, genero.Description);
    }
}
