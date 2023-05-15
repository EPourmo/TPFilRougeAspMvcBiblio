using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Entities
{
    public class Reader : Entity
    {

        public string LastName { get; init; } = null!;
        public string FirstName { get; init; } = null!;

        public string FullName => $"{LastName},{FirstName}";
        public string Email { get; init; } = null!;
        public string Phone { get; init; } = null!;


    }
}
