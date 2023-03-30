using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movies.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Movies.DTO
{
    public class CrudDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleasedDate { get; set; }
        public string Genre { get; set; }
        public string Rating { get; set; }
        [JsonIgnore]
        public Guid ActorId { get; set; }
        public string ActorName { get; set; }
        [BindNever]
        public string Poster { get; set; }
    }

    public class Crud 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleasedDate { get; set; }
        public string Genre { get; set; }
        public string Rating { get; set; }
        public List<Guid> ActorId { get; set; }
        public string ActorName { get; set; }
        [BindNever]
        public string Poster { get; set; }
    }

    public class Create
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleasedDate { get; set; }
        public Genre Genre { get; set; }
        public Rating Rating { get; set; }
        public List<Guid> ActorId { get; set; }
        [IgnoreDataMember]
        public IFormFile Poster { get; set; }
    }

    public class AddActor
    {
        [Required]
        public string Actor { get; set; }
        public string Biography { get; set; }
    }
    public class SelectActor 
    {
        public string Actor { get; set; }
    }
}
