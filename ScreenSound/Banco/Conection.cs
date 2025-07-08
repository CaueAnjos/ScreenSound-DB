using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class Conection
{
    private static readonly string connectionString =
        "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;";

    public static SqlConnection Conectar()
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

    public static IEnumerable<Artista> ObterArtistas()
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
}
