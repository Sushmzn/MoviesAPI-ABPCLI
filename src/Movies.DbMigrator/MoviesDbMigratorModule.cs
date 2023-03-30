using Movies.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Movies.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MoviesEntityFrameworkCoreModule),
    typeof(MoviesApplicationContractsModule)
    )]
public class MoviesDbMigratorModule : AbpModule
{

}
