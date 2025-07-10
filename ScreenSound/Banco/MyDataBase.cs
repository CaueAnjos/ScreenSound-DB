using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal interface IDao<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}

internal interface IMyDataBase
{
    IDao<Artista> Artistas { get; }
    IDao<Musica> Musicas { get; }

    [Obsolete("Use Artistas or Musicas properties instead.")]
    IEnumerable<Artista> ObterArtistas();

    [Obsolete("Use Artistas or Musicas properties instead.")]
    Artista? ObterArtistaPorId(int id);

    [Obsolete("Use Artistas or Musicas properties instead.")]
    bool AdicionarArtista(Artista artista);

    [Obsolete("Use Artistas or Musicas properties instead.")]
    bool RemoverArtista(int id);

    [Obsolete("Use Artistas or Musicas properties instead.")]
    bool MudarArtista(int id, Artista artista);

    // bool MudarFotoPerfil(int id, string fotoPerfil);
    // bool MudarNome(int id, string nome);
    // bool MudarBio(int id, string bio);

    [Obsolete("Use Artistas or Musicas properties instead.")]
    bool AdicionarMusica(Musica musica);

    [Obsolete("Use Artistas or Musicas properties instead.")]
    IEnumerable<Musica> ObterMusicas();
}
