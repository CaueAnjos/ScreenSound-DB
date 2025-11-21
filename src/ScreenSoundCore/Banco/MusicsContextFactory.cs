using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ScreenSoundCore.Banco
{
    public class MusicsContextFactory : IDesignTimeDbContextFactory<MusicsContext>
    {
        public MusicsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicsContext>();

            var connectionString =
                "Server=localhost,1433;Database=master;User Id=sa;Password=[Senha123];TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new MusicsContext(optionsBuilder.Options);
        }
    }
}
