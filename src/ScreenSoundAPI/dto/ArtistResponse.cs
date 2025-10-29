using ScreenSoundAPI.Request;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultArtistResponse(int Id, string Name, string Bio, string PerfilPhoto, ICollection<MusicaResponse> Musics);

public static class ArtistaResponseExtations
{
    public static DefaultArtistResponse GetResponse(this Artist artista)
    {
        var musics = artista.Musics.Select(m => m.GetResponse()).ToArray();
        return new DefaultArtistResponse(artista.Id, artista.Name, artista.Bio, artista.PerfilPhoto, musics);
    }
}
