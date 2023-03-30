using System.Threading.Tasks;

namespace Movies.Data;

public interface IMoviesDbSchemaMigrator
{
    Task MigrateAsync();
}
