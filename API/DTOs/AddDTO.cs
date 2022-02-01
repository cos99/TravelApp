using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AddDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Alias { get; set; }
        public string PhotoUrl { get; set; }
        public string Country { get; set; }
    }
}