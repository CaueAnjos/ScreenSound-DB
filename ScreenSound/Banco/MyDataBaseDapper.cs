using Dapper;
using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class MyDataBaseDapper : IMyDataBase
{
    private readonly string connectionString =
        "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;";

    public MyDataBaseDapper(SqlConnection connection)
    {
        Connection = connection;
    }

    public SqlConnection Connection { get; set; }

    public bool AdicionarArtista(Artista artista)
    {
        string sql =
            "INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES (@Nome, @Bio, @FotoPerfil)";
        return Connection.Execute(sql, artista) > 0;
    }

    public bool MudarArtista(int id, Artista artista)
    {
        string sql =
            "UPDATE Artistas SET Nome = @Nome, Bio = @Bio, FotoPerfil = @FotoPerfil WHERE Id = @Id";
        return Connection.Execute(sql, artista) > 0;
    }

    public Artista? ObterArtistaPorId(int id)
    {
        string sql = "SELECT ID, Nome, Bio, FotoPerfil FROM Artistas WHERE Id = @Id";
        return Connection.QueryFirstOrDefault<Artista>(sql, new { Id = id });
    }

    public IEnumerable<Artista> ObterArtistas()
    {
        string sql = "SELECT ID, Nome, Bio, FotoPerfil FROM Artistas";
        return Connection.Query<Artista>(sql);
    }

    public bool RemoverArtista(int id)
    {
        string sql = "DELETE FROM Artistas WHERE Id = @Id";
        return Connection.Execute(sql, new { Id = id }) > 0;
    }
}
