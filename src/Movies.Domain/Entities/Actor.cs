using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Movies.Entities
{
    //fullaudited ma deletedTime included
    public class Actor : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
