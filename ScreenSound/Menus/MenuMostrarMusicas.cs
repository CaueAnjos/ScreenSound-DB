using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override void Executar(Banco.IDal dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Exibir todas as musicas");

        foreach (Musica musica in dal.Musicas.GetAll())
        {
            Console.WriteLine(musica);
        }
        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
