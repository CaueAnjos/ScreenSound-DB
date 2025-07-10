using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(Banco.IMyDataBase artistasRegistrados)
    {
        base.Executar(artistasRegistrados);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o id do artista cuja música deseja registrar: ");
        int nomeDoArtista = int.Parse(Console.ReadLine()!);

        Artista? artista = artistasRegistrados.ObterArtistaPorId(nomeDoArtista);
        if (artista is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            artista.AdicionarMusica(new Musica(tituloDaMusica));
            Console.WriteLine(
                $"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!"
            );
            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
