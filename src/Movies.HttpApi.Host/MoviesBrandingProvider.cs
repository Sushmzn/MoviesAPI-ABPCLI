using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Movies;

[Dependency(ReplaceServices = true)]
public class MoviesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Movies";
}
