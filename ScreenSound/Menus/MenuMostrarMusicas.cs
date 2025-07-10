using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override void Executar(Banco.IMyDataBase artistasRegistrados)
    {
        base.Executar(artistasRegistrados);
        ExibirTituloDaOpcao("Exibir detalhes do artista");
        Console.Write("Digite o id do artista que deseja conhecer melhor: ");
        int nomeDoArtista = int.Parse(Console.ReadLine()!);

        Artista? artista = artistasRegistrados.ObterArtistaPorId(nomeDoArtista);
        if (artista is not null)
        {
            Console.WriteLine("\nDiscografia:");
            artista.ExibirDiscografia();
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
