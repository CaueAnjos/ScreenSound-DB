using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
internal class GeneroDao(EntityContext context) : IDao<Genre>
{
    private EntityContext _context = context;

    public void Add(Genre genero)
    {
        _context.Generos.Add(genero);
        _context.SaveChanges();
    }

    public IEnumerable<Genre> GetAll()
    {
        return _context.Generos;
    }

    public Genre? GetSingle(Func<Genre, bool> condition)
    {
        return _context.Generos.SingleOrDefault(condition);
    }

    public Genre? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Genre genero)
    {
        _context.Generos.Remove(genero);
        _context.SaveChanges();
    }

    public void Update(Genre genero)
    {
        _context.Generos.Update(genero);
        _context.SaveChanges();
    }

    public IEnumerable<Genre> GetAllWith(Func<Genre, bool> condition)
    {
        return _context.Generos.Where(condition);
    }
}
