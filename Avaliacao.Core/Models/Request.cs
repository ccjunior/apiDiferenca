using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.Core.Model
{
    public class Request
    {
        public Guid Id { get; set; }
        public object Dados   { get; set; }
        public DateTime DataEnvio { get; set; }

        //public Request(Guid id, object Data)
        //{
        //    Id = id;
        //    Dados = Data;
        //    DataEnvio = DateTime.Now;
        //}

    }
}
