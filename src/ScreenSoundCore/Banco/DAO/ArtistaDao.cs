using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
internal class ArtistaDao(EntityContext context) : IDao<Artist>
{
    private readonly EntityContext _context = context;

    public void Add(Artist artista)
    {
        _context.Artistas.Add(artista);
        _context.SaveChanges();
    }

    public IEnumerable<Artist> GetAll()
    {
        return _context.Artistas;
    }

    public Artist? GetSingle(Func<Artist, bool> condition)
    {
        return _context.Artistas.SingleOrDefault(condition);
    }

    public Artist? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Artist artista)
    {
        foreach (var musica in artista.Musics)
        {
            musica.Artista = null;
            _context.Musicas.Update(musica);
        }

        _context.Artistas.Remove(artista);
        _context.SaveChanges();
    }

    public void Update(Artist artista)
    {
        _context.Artistas.Update(artista);
        _context.SaveChanges();
    }

    public IEnumerable<Artist> GetAllWith(Func<Artist, bool> condition)
    {
        return _context.Artistas.Where(condition);
    }
}
