using AutoMapper;
using Movies.DTO;
using Movies.Entities;

namespace Movies;

public class MoviesApplicationAutoMapperProfile : Profile
{
    public MoviesApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        //CreateMap<Actor, GetAllDTO>();
        CreateMap<Actor, GetActor>();
        CreateMap<AddActor, Actor>();
        CreateMap<Actor, AddActor>();
        CreateMap<ImportActor, Actor>();
        CreateMap<Actor, ImportActor>();


    }
}
