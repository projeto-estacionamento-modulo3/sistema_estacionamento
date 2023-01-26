using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_estacionamento_mod3.Interfaces
{
    public interface IServicosExtras
    {
        public bool Lavagem { get; set; }
        public bool Revisão { get; set; }

        public void LavarVeiculo();
        public void FazerRevisão();
    }
}
