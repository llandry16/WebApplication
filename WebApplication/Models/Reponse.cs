using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Reponse
    {
        public int SondageId { get; set; }
        public List<char> Reponses { get; set; }
    }
}
