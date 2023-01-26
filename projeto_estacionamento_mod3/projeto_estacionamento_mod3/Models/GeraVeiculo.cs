using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_estacionamento_mod3.Models
{
    public class GeraVeiculo
    {
        public static Veiculo GerarNovoVeiculo(string tipoVeiculo, string modeloVeiculo, string marcaVeiculo, string corVeiculo, string placaVeiculo)
        {
            Veiculo veiculoCadastrado = new Veiculo(tipoVeiculo, modeloVeiculo, marcaVeiculo, corVeiculo, placaVeiculo);
            return veiculoCadastrado;
        }
    }
}
