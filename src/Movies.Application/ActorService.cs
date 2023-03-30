using Microsoft.AspNetCore.Mvc;
using Movies.DTO;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Movies
{
    public class ActorService : MoviesAppService, IActorService
    {
        private readonly IRepository<Movies.Entities.Actor, Guid> _repository;

        public ActorService(IRepository<Actor, Guid> repository):base()
        {
            _repository = repository;
        }

        public async Task<AddActor> AddActor([FromForm] AddActor AddActor)
        {
            try
            {
                var actor = new Actor()
                {
                    Name = AddActor.Actor,
                    Biography = AddActor.Biography,
                };
                await _repository.InsertAsync(actor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
            
        }

        public async Task<AddActor> EditActor(Guid id, [FromForm] AddActor actor)
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
                var actor = from a in await _repository.GetQueryableAsync()
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
                return null;
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
            var actor = from a in await _repository.GetQueryableAsync()
                        select new GetActor()
                        {
                            Name = a.Name,
                            Biography = a.Biography
                        };
            return actor.ToList();
        }


    }
}
