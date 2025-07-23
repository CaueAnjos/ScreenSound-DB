using ScreenSound.Modelos;

namespace ScreenSound.Banco.Dao;

internal class ArtistaDao(EntityContext context) : IDao<Artista>
{
    private readonly EntityContext _context = context;

    public void Add(Artista artista)
    {
        _context.Artistas.Add(artista);
        _context.SaveChanges();
    }

    public IEnumerable<Artista> GetAll()
    {
        return _context.Artistas;
    }

    public Artista GetSingle(Func<Artista, bool> condition)
    {
        return _context.Artistas.Single(condition);
    }

    public Artista GetById(int id)
    {
        return _context.Artistas.Single(a => a.Id == id);
    }

    public void Remove(Artista artista)
    {
        _context.Artistas.Remove(artista);
        _context.SaveChanges();
    }

    public void Update(Artista artista)
    {
        _context.Artistas.Update(artista);
        _context.SaveChanges();
    }

    public IEnumerable<Artista> GetAllWith(Func<Artista, bool> condition)
    {
        return _context.Artistas.Where(condition);
    }
}
