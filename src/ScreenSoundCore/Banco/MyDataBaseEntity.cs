using Microsoft.EntityFrameworkCore;
using ScreenSoundCore.Banco.Dao;
using ScreenSoundCore.Modelos;

namespace ScreenSoundCore.Banco;

public class EntityContext : DbContext
{
#nullable disable
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Genero { get; set; }

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
        modelBuilder.Entity<Musica>().HasMany(c => c.Generos).WithMany(c => c.Musicas);
    }
}

public class MyDataBaseEntity : IDal
{
    public MyDataBaseEntity(EntityContext context)
    {
        Context = context;
        Artistas = new ArtistaDao(context);
        Musicas = new MusicaDao(context);
    }

    public EntityContext Context { get; set; }

    public IDao<Artista> Artistas { get; init; }

    public IDao<Musica> Musicas { get; init; }
}
