using ScreenSoundCore.Banco.Dao;
using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
public interface IDal
{
    public IDao<Artist> Artistas { get; }
    public IDao<Musica> Musicas { get; }
    public IDao<Genero> Generos { get; }
}
