using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record ArtistaResponse(int Id, string Name, string Bio, string FotoPerfil);

public static class ArtistaResponseExtations
{
    public static ArtistaResponse GetResponse(this Artista artista)
    {
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }
}
