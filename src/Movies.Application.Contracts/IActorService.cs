using Movies.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Movies
{
    public interface IActorService : IApplicationService
    {
        Task<List<GetActor>> GetAllActor();
        Task<AddActor> AddActor(AddActor addActor);
        Task<GetActor> GetActorById(Guid id);
        Task<AddActor> EditActor(Guid id, AddActor actor);
        Task<GetActor> GetActorByName(string Name);
    }
}
