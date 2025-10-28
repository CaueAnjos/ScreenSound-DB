using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
internal class ArtistaDao(MusicsContext context) : IDao<Artist>
{
    private readonly MusicsContext _context = context;

    public void Add(Artist artista)
    {
        _context.Artists.Add(artista);
        _context.SaveChanges();
    }

    public IEnumerable<Artist> GetAll()
    {
        return _context.Artists;
    }

    public Artist? GetSingle(Func<Artist, bool> condition)
    {
        return _context.Artists.SingleOrDefault(condition);
    }

    public Artist? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Artist artista)
    {
        foreach (var musica in artista.Musics)
        {
            musica.Artist = null;
            _context.Musics.Update(musica);
        }

        _context.Artists.Remove(artista);
        _context.SaveChanges();
    }

    public void Update(Artist artista)
    {
        _context.Artists.Update(artista);
        _context.SaveChanges();
    }

    public IEnumerable<Artist> GetAllWith(Func<Artist, bool> condition)
    {
        return _context.Artists.Where(condition);
    }
}
