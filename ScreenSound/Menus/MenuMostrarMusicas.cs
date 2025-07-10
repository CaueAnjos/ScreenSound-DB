using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override void Executar(Banco.IMyDataBase artistasRegistrados)
    {
        base.Executar(artistasRegistrados);
        ExibirTituloDaOpcao("Exibir detalhes do artista");

        Console.WriteLine("\nDiscografia:");
        foreach (var musica in artistasRegistrados.ObterMusicas())
        {
            Console.WriteLine(musica);
        }
        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
