using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record ArtistaResponse(int Id, string Name, string Bio, string FotoPerfil, ICollection<MusicaResponse> Musics);

public static class ArtistaResponseExtations
{
    public static ArtistaResponse GetResponse(this Artist artista)
    {
        var musics = artista.Musics.Select(m => m.GetResponse()).ToArray();
        return new ArtistaResponse(artista.Id, artista.Name, artista.Bio, artista.PerfilPhoto, musics);
    }
}
