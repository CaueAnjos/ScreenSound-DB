namespace ScreenSoundAPI.Request;

public record MusicaRequest(string Name, ICollection<GeneroRequest> Generos);
