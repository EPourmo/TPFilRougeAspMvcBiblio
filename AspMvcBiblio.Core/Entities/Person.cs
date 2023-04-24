using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Entities
{
    public class Person : Entity
    {

        
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;



    }
}
