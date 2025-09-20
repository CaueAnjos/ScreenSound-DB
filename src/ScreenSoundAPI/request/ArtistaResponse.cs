using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record ArtistaResponse(int Id, string Name, string Bio, string FotoPerfil, ICollection<MusicaResponse> Musics);

public static class ArtistaResponseExtations
{
    public static ArtistaResponse GetResponse(this Artista artista)
    {
        var musics = artista.Musicas.Select(m => m.GetResponse()).ToArray();
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil, musics);
    }
}
