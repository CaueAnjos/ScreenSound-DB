using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Banco.IMyDataBase banco)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
