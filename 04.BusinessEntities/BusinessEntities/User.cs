using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CORE_ENT = BusinessEntities.CoreEntity;


namespace BusinessEntities
{
    public class User : CORE_ENT.Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

    }
}
