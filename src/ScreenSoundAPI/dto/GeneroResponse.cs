using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record GeneroResponse(string Name, string Description);

public static class GeneroResponseExtations
{
    public static GeneroResponse GetResponse(this Genre genero)
    {
        return new GeneroResponse(genero.Name, genero.Descricao);
    }
}
