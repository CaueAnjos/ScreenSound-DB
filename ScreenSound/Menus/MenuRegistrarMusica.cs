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

        Console.Write("Agora digite o nome do autor desta música: ");
        string artistaNome = Console.ReadLine()!;

        Musica musica = new Musica(tituloDaMusica);
        dal.Musicas.Add(musica);

        Artista artista = dal.Artistas.Get(a => a.Nome == artistaNome);
        artista.AdicionarMusica(musica);
        dal.Artistas.Update(artista);

        Console.WriteLine($"A música {tituloDaMusica} foi registrada com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
