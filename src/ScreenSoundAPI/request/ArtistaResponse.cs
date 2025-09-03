using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record ArtistaResponse(int id, string name, string bio, string fotoPerfil);

public static class ArtistaResponseExtations
{
    public static ArtistaResponse GetResponse(this Artista artista)
    {
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }
}
