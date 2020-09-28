using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_tracker_backend.Domain
{
    public class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
