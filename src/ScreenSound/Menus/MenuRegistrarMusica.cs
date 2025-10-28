using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(IDal dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Registro de músicas");

        Console.Write("Agora digite o título da música: ");
        string tituloDaMusica = Console.ReadLine()!;

        Console.Write("Agora digite o nome do autor desta música: ");
        string artistaNome = Console.ReadLine()!;

        Music musica = new Music(tituloDaMusica);
        dal.Musicas.Add(musica);

        Artist artista = dal.Artistas.GetSingle(a => a.Name == artistaNome);
        artista.AdicionarMusica(musica);
        dal.Artistas.Update(artista);

        Console.WriteLine($"A música {tituloDaMusica} foi registrada com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
