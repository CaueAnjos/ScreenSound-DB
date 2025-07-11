using ScreenSound.Banco.Dao;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal interface IDal
{
    IDao<Artista> Artistas { get; }
    IDao<Musica> Musicas { get; }
}
