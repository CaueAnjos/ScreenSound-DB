using Microsoft.EntityFrameworkCore;
using ScreenSoundCore.Banco.Dao;
using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco;

public class MusicsContext : DbContext
{
#nullable disable
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Music> Musics { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Music>().HasMany(c => c.Genres).WithMany(c => c.Musics);

        modelBuilder
            .Entity<Artist>()
            .HasMany(c => c.Musics)
            .WithOne(c => c.Artist)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
public class MyDataBaseEntity : IDal
{
    public MyDataBaseEntity(MusicsContext context)
    {
        Context = context;
        Artistas = new ArtistaDao(context);
        Musicas = new MusicaDao(context);
        Generos = new GeneroDao(context);
    }

    public MusicsContext Context { get; set; }

    public IDao<Artist> Artistas { get; init; }
    public IDao<Music> Musicas { get; init; }
    public IDao<Genre> Generos { get; init; }
}
