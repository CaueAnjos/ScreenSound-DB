using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

internal class MusicaDao(EntityContext context) : IDao<Musica>
{
    private EntityContext _context = context;

    public void Add(Musica musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }

    public IEnumerable<Musica> GetAll()
    {
        return _context.Musicas;
    }

    public Musica? GetSingle(Func<Musica, bool> condition)
    {
        return _context.Musicas.SingleOrDefault(condition);
    }

    public Musica? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Musica musica)
    {
        _context.Musicas.Remove(musica);
        _context.SaveChanges();
    }

    public void Update(Musica musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();
    }

    public IEnumerable<Musica> GetAllWith(Func<Musica, bool> condition)
    {
        return _context.Musicas.Where(condition);
    }
}
