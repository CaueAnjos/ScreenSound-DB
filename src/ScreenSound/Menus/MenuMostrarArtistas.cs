using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Executar(IDal dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Exibindo todos os artistas registrados em nossa aplicação");

        foreach (Artist artista in dal.Artistas.GetAll())
        {
            Console.WriteLine($"Artista: ({artista.Id}) {artista.Name}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
