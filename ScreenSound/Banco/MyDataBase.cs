using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal interface IMyDataBase
{
    IEnumerable<Artista> ObterArtistas();
    Artista? ObterArtistaPorId(int id);

    bool AdicionarArtista(Artista artista);

    bool RemoverArtista(int id);

    bool MudarArtista(int id, Artista artista);
    // bool MudarFotoPerfil(int id, string fotoPerfil);
    // bool MudarNome(int id, string nome);
    // bool MudarBio(int id, string bio);
}
