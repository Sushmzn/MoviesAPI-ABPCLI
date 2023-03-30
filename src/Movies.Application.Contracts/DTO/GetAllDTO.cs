using Movies.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Movies.DTO
{
    public class GetAllDTO : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedDate { get; set; }
        public Genre Genre { get; set; }
        public Rating Rating { get; set; }
        public string Actor { get; set; }
        public string Biography { get; set; }
    }
    public class GetActor
    {
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
