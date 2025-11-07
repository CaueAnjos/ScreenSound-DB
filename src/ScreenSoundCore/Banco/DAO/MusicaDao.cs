using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
internal class MusicaDao(MusicsContext context) : IDao<Music>
{
    private MusicsContext _context = context;

    public void Add(Music musica)
    {
        _context.Musics.Add(musica);
        _context.SaveChanges();
    }

    public IEnumerable<Music> GetAll()
    {
        return _context.Musics;
    }

    public Music? GetSingle(Func<Music, bool> condition)
    {
        return _context.Musics.SingleOrDefault(condition);
    }

    public Music? GetById(int id)
    {
        return GetSingle(a => a.Id == id);
    }

    public void Remove(Music musica)
    {
        _context.Musics.Remove(musica);
        _context.SaveChanges();
    }

    public void Update(Music musica)
    {
        _context.Musics.Update(musica);
        _context.SaveChanges();
    }

    public IEnumerable<Music> GetAllWith(Func<Music, bool> condition)
    {
        return _context.Musics.Where(condition);
    }
}
