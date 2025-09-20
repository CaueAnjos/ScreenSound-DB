using ScreenSoundCore.Banco;
using ScreenSoundCore.Modelos;

namespace ScreenSoundAPI.Request;

public record GeneroRequest(string Name, string Description);

public static class GeneroRequestExtations
{
    public static Genero? TryGetObject(this GeneroRequest genero, IDal db)
    {
        return db.Generos.GetSingle(g => g.Nome == genero.Name);
    }

    public static Genero ConvertToObject(this GeneroRequest genero, IDal db)
    {
        var obj = genero.TryGetObject(db);
        if (obj is not null)
            return obj;

        obj = new Genero()
        {
            Nome = genero.Name,
            Descricao = genero.Description
        };

        db.Generos.Add(obj);
        return obj;
    }
}
