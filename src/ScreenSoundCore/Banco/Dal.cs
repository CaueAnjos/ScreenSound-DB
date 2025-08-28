using ScreenSoundCore.Banco.Dao;
using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco;

public interface IDal
{
    public IDao<Artista> Artistas { get; }
    public IDao<Musica> Musicas { get; }
}
