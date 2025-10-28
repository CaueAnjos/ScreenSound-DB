using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
internal class MusicaDao(EntityContext context) : IDao<Music>
{
    private EntityContext _context = context;

    public void Add(Music musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }

    public IEnumerable<Music> GetAll()
    {
        return _context.Musicas;
    }

    public Music? GetSingle(Func<Music, bool> condition)
    {
        return _context.Musicas.SingleOrDefault(condition);
    }

    public Music? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Music musica)
    {
        foreach (var genero in musica.Generos)
        {
            genero.Musicas.Remove(musica);
            _context.Generos.Update(genero);
        }

        if (musica.Artista is not null)
        {
            musica.Artista.Musics.Remove(musica);
            _context.Artistas.Update(musica.Artista);
        }

        _context.Musicas.Remove(musica);
        _context.SaveChanges();
    }

    public void Update(Music musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();
    }

    public IEnumerable<Music> GetAllWith(Func<Music, bool> condition)
    {
        return _context.Musicas.Where(condition);
    }
}
