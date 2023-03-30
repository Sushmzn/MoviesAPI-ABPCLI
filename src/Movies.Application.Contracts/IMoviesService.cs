using Microsoft.AspNetCore.Http;
using Movies.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Movies
{
    public interface IMoviesService : IApplicationService
    {
        Task<List<Crud>> GetAllMovie();
        Task<Create> CreateMovie(Create create);
        Task<Create> EditMovie(Guid id, Create create);
        Task<CrudDTO> GetMovieById(Guid id);
        Task<CrudDTO> GetMovieByName(string Name);
        //Task<CrudDTO> Search(string method);
        Task<List<Crud>> SearchByData( string data);
    }
}
