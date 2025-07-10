using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class EntityContext : DbContext
{
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;"
        );
    }
}

internal class MyDataBaseEntity : IMyDataBase
{
    public MyDataBaseEntity(EntityContext context)
    {
        Context = context;
    }

    public EntityContext Context { get; set; }

    public bool AdicionarArtista(Artista artista)
    {
        Context.Artistas.Add(artista);
        Context.Musicas.AddRange(artista.Musicas);
        return Context.SaveChanges() > 0;
    }

    public bool AdicionarMusica(Musica musica)
    {
        Context.Musicas.Add(musica);
        return Context.SaveChanges() > 0;
    }

    public bool MudarArtista(int id, Artista artista)
    {
        var a = Context.Artistas.Find(id);
        if (a is not null)
        {
            a.Nome = artista.Nome;
            a.Bio = artista.Bio;
            a.FotoPerfil = artista.FotoPerfil;
        }
        return Context.SaveChanges() > 0;
    }

    public Artista? ObterArtistaPorId(int id)
    {
        return Context.Artistas.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Artista> ObterArtistas()
    {
        return Context.Artistas;
    }

    public IEnumerable<Musica> ObterMusicas()
    {
        return Context.Musicas;
    }

    public bool RemoverArtista(int id)
    {
        var a = ObterArtistaPorId(id);
        if (a != null)
            Context.Artistas.Remove(a);

        return Context.SaveChanges() > 0;
    }
}
