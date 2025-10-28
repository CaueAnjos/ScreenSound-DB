using ScreenSoundCore.Banco.Dao;
using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
public interface IDal
{
    public IDao<Artist> Artistas { get; }
    public IDao<Music> Musicas { get; }
    public IDao<Genre> Generos { get; }
}
