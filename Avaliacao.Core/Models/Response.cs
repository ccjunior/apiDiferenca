using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.Core.Model
{
    public class Response
    {
        public Guid Id { get; set; }
        public bool erro { get; set; }
        public string resultado { get; set; }
    }
}
