using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record MusicaResponse(int Id, string Name, DateTime DataLancamento, int? ArtistaId, ICollection<GeneroResponse> Generos);

public static class MusicaResponseExtations
{
    public static MusicaResponse GetResponse(this Music musica)
    {
        int? artistaId = musica.Artista?.Id ?? null;
        var generos = musica.Generos.Select(g => g.GetResponse()).ToList();
        return new MusicaResponse(musica.Id, musica.Nome, musica.DataLancamento, artistaId, generos);
    }
}
