using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CORE_ENT = BusinessEntities.CoreEntity;


namespace BusinessEntities
{
    public class Book : CORE_ENT.Entity
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public List<string> Authors { get; set; }
    }
}
