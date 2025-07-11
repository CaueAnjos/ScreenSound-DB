using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Banco.IDal banco)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
