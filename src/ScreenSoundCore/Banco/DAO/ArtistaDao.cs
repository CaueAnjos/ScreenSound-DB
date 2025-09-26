using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

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

    public Artista? GetSingle(Func<Artista, bool> condition)
    {
        return _context.Artistas.SingleOrDefault(condition);
    }

    public Artista? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Artista artista)
    {
        //NOTE: this is used to remove all references of this artista
        foreach (var musica in artista.Musicas)
        {
            musica.Artista = null;
            _context.Musicas.Update(musica);
        }

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
