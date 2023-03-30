using Movies.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Movies.Entities
{
    public class Movies : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedDate { get; set; }
        public Genre Genre { get; set; }
        public List<Guid> ActorId { get; set; }
        public virtual Actor? Actor { get; set; }
        public Rating Rating { get; set; }
        public string Poster { get; set; }
    }
}
