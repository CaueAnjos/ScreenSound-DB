using Microsoft.EntityFrameworkCore;
using ScreenSound.Banco.Dao;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class EntityContext : DbContext
{
#nullable disable
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;"
        );
    }
}

internal class MyDataBaseEntity : IDal
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
