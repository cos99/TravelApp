using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class UserAdd
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public AppUser AddedUser { get; set; }
        public int AddedUserId { get; set; }
    }
}