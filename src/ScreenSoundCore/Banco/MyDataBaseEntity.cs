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
        optionsBuilder
            .UseSqlServer(
                "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;"
            )
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Music>().HasMany(c => c.Genres).WithMany(c => c.Musics);
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
