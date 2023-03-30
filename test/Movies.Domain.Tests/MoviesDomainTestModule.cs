using Movies.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Movies;

[DependsOn(
    typeof(MoviesEntityFrameworkCoreTestModule)
    )]
public class MoviesDomainTestModule : AbpModule
{

}
