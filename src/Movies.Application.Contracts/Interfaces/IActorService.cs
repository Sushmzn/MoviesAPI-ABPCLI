using Microsoft.AspNetCore.Http;
using Movies.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Movies.Interfaces
{
    public interface IActorService : IApplicationService
    {
        Task<List<GetActor>> GetAllActor();
        Task<AddActor> AddActor(AddActor addActor);
        Task<GetActor> GetActorById(Guid id);
        Task<AddActor> UpdateActor(Guid id, AddActor actor);
        Task<GetActor> GetActorByName(string Name);
        Task<List<ImportActor>> ImportCsv(IFormFile file);
        Task<List<ImportActor>> ImportExcel(IFormFile excelfile);
        Task<GetActor> ExportExcel();
        Task<GetActor> ExportCsv();



    }
}
