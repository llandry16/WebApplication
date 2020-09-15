using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Sondage
    {
        public long Id { get; set; }
        public List<Question> Questions { get; set; }
    }
}
