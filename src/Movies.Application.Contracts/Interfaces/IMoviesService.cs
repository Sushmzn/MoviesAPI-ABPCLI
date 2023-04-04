using Microsoft.AspNetCore.Http;
using Movies.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Movies.Interfaces
{
    public interface IMoviesService : IApplicationService
    {
        Task<List<Crud>> GetAllMovie();
        Task<Create> CreateMovie(Create create);
        Task<Update> UpdateMovie(Guid id, Update create);
        Task<CrudDTO> GetMovieById(Guid id);
        Task<CrudDTO> GetMovieByName(string Name);
        Task<List<Crud>> SearchByData(string data);
    }
}
