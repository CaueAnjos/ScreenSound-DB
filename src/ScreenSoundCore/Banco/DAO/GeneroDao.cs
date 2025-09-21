using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

internal class GeneroDao(EntityContext context) : IDao<Genero>
{
    private EntityContext _context = context;

    public void Add(Genero genero)
    {
        _context.Generos.Add(genero);
        _context.SaveChanges();
    }

    public IEnumerable<Genero> GetAll()
    {
        return _context.Generos;
    }

    public Genero? GetSingle(Func<Genero, bool> condition)
    {
        return _context.Generos.SingleOrDefault(condition);
    }

    public Genero? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Genero genero)
    {
        _context.Generos.Remove(genero);
        _context.SaveChanges();
    }

    public void Update(Genero genero)
    {
        _context.Generos.Update(genero);
        _context.SaveChanges();
    }

    public IEnumerable<Genero> GetAllWith(Func<Genero, bool> condition)
    {
        return _context.Generos.Where(condition);
    }
}
