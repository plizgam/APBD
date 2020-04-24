using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class LoginRequestDto
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string? Salt { get; set; }
        public Guid? Token { get; set; }
    }
}
