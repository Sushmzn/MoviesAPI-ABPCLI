using System;
using System.Collections.Generic;
using System.Text;
using Movies.Localization;
using Volo.Abp.Application.Services;

namespace Movies;

/* Inherit your application services from this class.
 */
public abstract class MoviesAppService : ApplicationService
{
    protected MoviesAppService()
    {
        LocalizationResource = typeof(MoviesResource);
    }
}
