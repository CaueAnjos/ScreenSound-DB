using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record MusicaResponse(string name, DateTime dataLancamento, int? artistaId);

public static class MusicaResponseExtations
{
    public static MusicaResponse GetResponse(this Musica musica)
    {
        int? artistaId = musica.Artista?.Id ?? null;
        return new MusicaResponse(musica.Nome, musica.DataLancamento, artistaId);
    }
}
