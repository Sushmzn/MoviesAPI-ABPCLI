using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTO;
using Movies.Entities;
using Movies.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Movies
{
    //[AllowAnonymous] to show for all without logging in.
    [Authorize]
    public class ActorService : MoviesAppService, IActorService
    {
        private readonly IRepository<Movies.Entities.Actor, Guid> _repository;

        public ActorService(IRepository<Actor, Guid> repository) : base()
        {
            _repository = repository;
        }

        public async Task<AddActor> AddActor([FromForm] AddActor addActor)
        {
            try
            {
                /*var actor = ObjectMapper.Map<AddActor, Actor>(addActor);
                await _repository.InsertAsync(actor);*/

                var actor = new Actor()
                {
                    Name = addActor.Actor,
                    Biography = addActor.Biography,
                };
                await _repository.InsertAsync(actor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }

        public async Task<AddActor> UpdateActor(Guid id, [FromForm] AddActor actor)
        {
            try
            {
                var Data = await _repository.FirstOrDefaultAsync(x => x.Id == id);
                Data.Name = actor.Actor;
                Data.Biography = actor.Biography;
                await _repository.UpdateAsync(Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<GetActor> GetActorById(Guid id)
        {
            try
            {
                //Automapper Mapping needed
                var actor = await _repository.GetAsync(id);
                var getActor = ObjectMapper.Map<Actor, GetActor>(actor);
                return getActor;


                /*var actor = from a in await _repository.GetQueryableAsync()
                            where a.Id == id
                            select new GetActor()
                            {
                                Name = a.Name,
                                Biography = a.Biography
                            };
                if (actor != null)
                {
                    return actor.FirstOrDefault();
                }
                return null;*/
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<GetActor> GetActorByName(string Name)
        {
            try
            {
                var actor = from a in await _repository.GetQueryableAsync()
                            where a.Name == Name
                            select new GetActor()
                            {
                                Name = a.Name,
                                Biography = a.Biography
                            };
                if (actor != null)
                {
                    return actor.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetActor>> GetAllActor()
        {

            /*var actors = await _repository.GetQueryableAsync();
            var data = actors.Select(actor => ObjectMapper.Map<Actor, GetActor>(actor)).ToList();
            return data;*/

            var actor = from a in await _repository.GetQueryableAsync()
                        select new GetActor()
                        {
                            Name = a.Name,
                            Biography = a.Biography
                        };
            return actor.ToList();
        }


        public async Task<List<ImportActor>> ImportCsv(Microsoft.AspNetCore.Http.IFormFile file)
        {
            try
            {
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    await streamReader.ReadLineAsync();

                    //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var importedData = new List<Actor>();
                    while (!streamReader.EndOfStream)
                    {
                        var line = await streamReader.ReadLineAsync();
                        var values = line.Split(',');
                        var excelData = new ImportActor()
                        {
                            Name = values[0],
                            Biography = values[1]
                        };
                        var maps = ObjectMapper.Map<ImportActor, Actor>(excelData);

                        importedData.Add(maps);
                    }
                    await _repository.InsertManyAsync(importedData);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<List<ImportActor>> ImportExcel(IFormFile excelfile)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    await excelfile.CopyToAsync(stream);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        reader.Read();
                        var importedData = new List<Actor>();
                        while (reader.Read())
                        {
                            var excelData = new ImportActor()
                            {
                                Name = reader.GetString(0),
                                Biography = reader.GetString(1)
                            };
                            var maps = ObjectMapper.Map<ImportActor, Actor>(excelData);
                            importedData.Add(maps);
                        }
                        await _repository.InsertManyAsync(importedData);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        public async Task<GetActor> ExportExcel()
        {
            try
            {
                var data = await _repository.GetQueryableAsync();

                var selectedData = data.Select(x => new
                {
                    Name = x.Name,
                    Biograpgy = x.Biography
                });

                using (var ws = new XLWorkbook())
                {
                    var worksheet = ws.Worksheets.Add("MyEntities");
                    worksheet.Cell(1, 1).InsertTable(selectedData);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        string name = DateTime.Now.ToString("yyyy-MM-dd");
                        var fileName = name + ".xlsx";
                        ws.SaveAs(@"D:\Amnil\MoviesApi\aspnet-core\src\Movies.Application\Excel\" + fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<GetActor> ExportCsv()
        {
            try
            {
                var data = await _repository.GetQueryableAsync();
                var maps = data.Select(Actor => ObjectMapper.Map<Actor, GetActor>(Actor)).ToList();
                string delimiter = ",";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Name,Biography");
                foreach (var map in maps)
                {
                    sb.AppendLine($"{map.Name}{delimiter}{map.Biography}{delimiter}");
                }
                string filename = DateTime.Now.ToString("yyyy-mm-dd") + ".csv";
                using (StreamWriter stream = new StreamWriter(@"D:\Amnil\MoviesApi\aspnet-core\src\Movies.Application\CSV\" + filename))
                {
                    stream.WriteLine(sb.ToString());
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

