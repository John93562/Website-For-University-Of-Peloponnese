using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.Models.Identity
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public string UserEmail { get; set; }
    }
}
