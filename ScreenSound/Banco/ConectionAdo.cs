using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class MyDataBaseADO : IMyDataBase
{
    private readonly string connectionString =
        "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;";

    private SqlConnection Conectar()
    {
        SqlConnection conexao = new(connectionString);

        try
        {
            conexao.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }

        return conexao;
    }

    public IEnumerable<Artista> ObterArtistas()
    {
        using SqlConnection conexao = Conectar();

        string query = "SELECT Id, Nome, Bio, FotoPerfil FROM Artistas";
        SqlCommand comando = new(query, conexao);
        using SqlDataReader leitor = comando.ExecuteReader();

        while (leitor.Read())
        {
            string nome = leitor["Nome"].ToString()!;
            string bio = leitor["Bio"].ToString()!;
            string fotoPerfil = leitor["FotoPerfil"].ToString()!;
            int id = (int)leitor["Id"];

            yield return new Artista(nome, bio) { Id = id, FotoPerfil = fotoPerfil };
        }
    }

    public bool AdicionarArtista(Artista artista)
    {
        using SqlConnection conexao = Conectar();

        string sql =
            "INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES (@Nome, @Bio, @FotoPerfil)";

        SqlCommand comando = new(sql, conexao);
        comando.Parameters.AddWithValue("@Nome", artista.Nome);
        comando.Parameters.AddWithValue("@Bio", artista.Bio);
        comando.Parameters.AddWithValue("@FotoPerfil", artista.FotoPerfil);

        int rowsEffected = comando.ExecuteNonQuery();
        return rowsEffected > 0;
    }

    public bool RemoverArtista(int id)
    {
        using SqlConnection conexao = Conectar();

        string sql = "DELETE FROM Artistas WHERE Id = @Id";
        SqlCommand comando = new(sql, conexao);
        comando.Parameters.AddWithValue("@Id", id);

        int rowsEffected = comando.ExecuteNonQuery();
        return rowsEffected > 0;
    }

    public Artista? ObterArtistaPorId(int id)
    {
        using SqlConnection conexao = Conectar();

        string sql = "SELECT Id, Nome, Bio, FotoPerfil FROM Artistas WHERE Id = @Id";
        SqlCommand comando = new(sql, conexao);
        comando.Parameters.AddWithValue("@Id", id);

        using SqlDataReader leitor = comando.ExecuteReader();
        if (leitor.Read())
        {
            string nome = leitor["Nome"].ToString()!;
            string bio = leitor["Bio"].ToString()!;
            string fotoPerfil = leitor["FotoPerfil"].ToString()!;
            return new Artista(nome, bio) { Id = id, FotoPerfil = fotoPerfil };
        }

        return null;
    }

    public bool MudarArtista(int id, Artista artista)
    {
        using SqlConnection conexao = Conectar();

        string sql =
            "UPDATE Artistas SET Nome = @Nome, Bio = @Bio, FotoPerfil = @FotoPerfil WHERE Id = @Id";

        SqlCommand comando = new(sql, conexao);
        comando.Parameters.AddWithValue("@Id", id);
        comando.Parameters.AddWithValue("@Nome", artista.Nome);
        comando.Parameters.AddWithValue("@Bio", artista.Bio);
        comando.Parameters.AddWithValue("@FotoPerfil", artista.FotoPerfil);

        int rowsEffected = comando.ExecuteNonQuery();
        return rowsEffected > 0;
    }
}
