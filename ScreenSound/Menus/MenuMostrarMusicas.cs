using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override void Executar(IDal dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Exibir todas as musicas");

        foreach (Musica musica in dal.Musicas.GetAll().ToList())
        {
            Console.WriteLine(musica);
        }
        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
