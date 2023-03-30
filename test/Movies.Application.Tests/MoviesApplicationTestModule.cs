using Volo.Abp.Modularity;

namespace Movies;

[DependsOn(
    typeof(MoviesApplicationModule),
    typeof(MoviesDomainTestModule)
    )]
public class MoviesApplicationTestModule : AbpModule
{

}
