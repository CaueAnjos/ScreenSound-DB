using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicasPorData : Menu
{
    public override void Executar(IDal db)
    {
        base.Executar(db);
        ExibirTituloDaOpcao("Mosstrar Músicas por Data de Lançamento");

        Console.WriteLine("Que data de lançamento você deseja buscar? (dd/MM/yyyy)");
        string? response = Console.ReadLine();

        if (DateTime.TryParse(response, out DateTime dataLancamento))
        {
            IEnumerable<Musica> musicas = db.Musicas.GetAllWith(musica =>
                musica.DataLancamento == dataLancamento
            );

            foreach (Musica musica in musicas)
            {
                Console.WriteLine(musica);
            }
        }
        else
        {
            Console.WriteLine("Não há musicas com essa data de lançamento.");
        }

        Console.WriteLine("Precione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
