using Microsoft.Data.SqlClient;

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
            Console.WriteLine("Conexão estabelecida com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
        }

        return conexao;
    }
}
