using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PropertyDto> Properties { get; set; }
    }
}
