using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(IDal banco)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
