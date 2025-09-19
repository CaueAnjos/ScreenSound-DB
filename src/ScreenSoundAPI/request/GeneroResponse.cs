using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record GeneroResponse(string Name, string Description);

public static class GeneroResponseExtations
{
    public static GeneroResponse GetResponse(this Genero genero)
    {
        return new GeneroResponse(genero.Nome, genero.Descricao);
    }
}
