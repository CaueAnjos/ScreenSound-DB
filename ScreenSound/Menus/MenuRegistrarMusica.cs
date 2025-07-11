using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(Banco.IDal dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Registro de músicas");

        Console.Write("Agora digite o título da música: ");
        string tituloDaMusica = Console.ReadLine()!;
        dal.Musicas.Add(new Musica(tituloDaMusica));
        Console.WriteLine($"A música {tituloDaMusica} foi registrada com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
