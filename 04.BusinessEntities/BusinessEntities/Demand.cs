using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CORE_ENT = BusinessEntities.CoreEntity;

namespace BusinessEntities
{
    public class Demand : CORE_ENT.Entity
    {
        public Book Book { get; set; }
        public User User { get; set; }

    }
}
