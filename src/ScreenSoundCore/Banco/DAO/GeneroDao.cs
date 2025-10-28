using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
internal class GeneroDao(MusicsContext context) : IDao<Genre>
{
    private MusicsContext _context = context;

    public void Add(Genre genero)
    {
        _context.Genres.Add(genero);
        _context.SaveChanges();
    }

    public IEnumerable<Genre> GetAll()
    {
        return _context.Genres;
    }

    public Genre? GetSingle(Func<Genre, bool> condition)
    {
        return _context.Genres.SingleOrDefault(condition);
    }

    public Genre? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Genre genero)
    {
        _context.Genres.Remove(genero);
        _context.SaveChanges();
    }

    public void Update(Genre genero)
    {
        _context.Genres.Update(genero);
        _context.SaveChanges();
    }

    public IEnumerable<Genre> GetAllWith(Func<Genre, bool> condition)
    {
        return _context.Genres.Where(condition);
    }
}
