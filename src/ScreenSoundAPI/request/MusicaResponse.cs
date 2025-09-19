using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record MusicaResponse(string Name, DateTime DataLancamento, int? ArtistaId, ICollection<GeneroResponse> Generos);

public static class MusicaResponseExtations
{
    public static MusicaResponse GetResponse(this Musica musica)
    {
        int? artistaId = musica.Artista?.Id ?? null;
        var generos = musica.Generos.Select(g => g.GetResponse()).ToList();
        return new MusicaResponse(musica.Nome, musica.DataLancamento, artistaId, generos);
    }
}
