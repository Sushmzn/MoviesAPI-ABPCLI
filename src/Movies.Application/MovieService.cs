using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTO;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Movies
{
    public class MovieService : MoviesAppService, IMoviesService
    {
        private readonly IRepository<Entities.Movies, Guid> _repository;
        private readonly IRepository<Entities.Actor, Guid> _ActorRepository;

        public MovieService(IRepository<Entities.Movies, Guid> repository, IRepository<Actor, Guid> actorRepository) : base()
        {
            _repository = repository;
            _ActorRepository = actorRepository;
        }

        public async Task<Create> CreateMovie([FromForm] Create create)
        {
            try
            {
                var fileName = "";

                if (create.Poster != null)
                {
                     fileName = $"{create.Name}-{Path.GetExtension(create.Poster.FileName)}";
                    var path = Path.Combine(@"D:\Amnil\MoviesApi\aspnet-core\src\Movies.Application\Posters\" + fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await create.Poster.CopyToAsync(stream);
                    }
                }
                var movie = new Entities.Movies()
                {
                    Name = create.Name,
                    Description = create.Description,
                    ReleasedDate = create.ReleasedDate,
                    ActorId = create.ActorId,
                    Genre = create.Genre,
                    Rating = create.Rating,
                    Poster = fileName
                };
                await _repository.InsertAsync(movie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<Create> EditMovie(Guid id, [FromForm] Create create)
        {
            try
            {
                var fileName = "";
                if (create.Poster != null)
                {
                    fileName = $"{create.Name}-{Path.GetExtension(create.Poster.FileName)}";
                    var path = Path.Combine(@"D:\Amnil\MoviesApi\aspnet-core\src\Movies.Application\Posters\" + fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await create.Poster.CopyToAsync(stream);
                    }
                }
                var Data = await _repository.FirstOrDefaultAsync(x => x.Id == id);
                Data.Name = create.Name;
                Data.Description = create.Description;
                Data.ReleasedDate = create.ReleasedDate;
                Data.Genre = create.Genre;
                Data.Rating = create.Rating;
                Data.Poster = fileName;
                await _repository.UpdateAsync(Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<List<Crud>> GetAllMovie()
        {
            var movie = await _repository.GetQueryableAsync();
            var actor = await _ActorRepository.GetQueryableAsync(); 


            var data = movie.Select(x => new Crud()
            {
                Name = x.Name,
                Description = x.Description,
                ReleasedDate = x.ReleasedDate,
                Genre = x.Genre.ToString(),
                Rating = x.Rating.ToString(),
                Poster = x.Poster,
                //No Actor Names showed!!!!!!!!!!!!!!!!!!!!!
            });
            
            return data.ToList();
        }

        public async Task<CrudDTO> GetMovieById(Guid id)
        {
            try
            {
                var movie = from x in await _repository.GetQueryableAsync()
                            where x.Id == id
                            select new CrudDTO()
                            {
                                Name = x.Name,
                                Description = x.Description,
                                ReleasedDate = x.ReleasedDate,
                                Genre = x.Genre.ToString(),
                                Rating = x.Rating.ToString(),
                                ActorName = x.Actor.Name,
                                Poster = x.Poster
                            };
                if (movie != null)
                {
                    return movie.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CrudDTO> GetMovieByName(string Name)
        {
            try
            {

                var actor = from x in await _repository.GetQueryableAsync()
                            where x.Name == Name
                            select new CrudDTO()
                            {
                                Name = x.Name,
                                Description = x.Description,
                                ReleasedDate = x.ReleasedDate,
                                Genre = x.Genre.ToString(),
                                Rating = x.Rating.ToString(),
                                ActorName = x.Actor.Name,
                                Poster = x.Poster
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

        public async Task<List<Crud>> SearchByData(string data)
        {
            try
            {
                var movie = await _repository.GetQueryableAsync();
                var data1 = movie.Select(x => new Crud()
                {
                    Name = x.Name,
                    Description = x.Description,
                    ReleasedDate = x.ReleasedDate,
                    Genre = x.Genre.ToString(),
                    Rating = x.Rating.ToString(),
                    Poster = x.Poster,
                    //No Actor Names showed!!!!!!!!!!!!!!!!!!!!!
                });
                if (data != null)
                {
                    data1 = data1.Where(c => c.Name!.Contains(data) || c.Description!.Contains(data));
                }
                return data1.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity.", ex);
            }
        }
    }
}
