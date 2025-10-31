using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.dto;

public record DefaultArtistRequest(string Name, string Bio, string PerfilPhoto, ICollection<DefaultMusicRequest> Musics)
{
    public static implicit operator Artist(DefaultArtistRequest request)
    {
        return new Artist
        {
            Name = request.Name,
            Bio = request.Bio,
            PerfilPhoto = request.PerfilPhoto,
            Musics = [.. request.Musics.Select(r => (Music)r)],
        };
    }
}

public record UpdateArtistRequest(string? Name, string? Bio, string? PerfilPhoto, ICollection<int>? MusicsId);
