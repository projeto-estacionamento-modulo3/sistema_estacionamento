using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_estacionamento_mod3.Interfaces;

namespace projeto_estacionamento_mod3.Models
{
    public class Veiculo : IServicosExtras
    {
        // Propriedades
        public string TipoVeiculo { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public string VagaEstacionada { get; set; }
        public Fatura FaturaEstacionamento { get; set; }
        public bool Lavagem { get; set; }
        public bool Revisão { get; set; }

        //Construtor
        public Veiculo(string tipoDeVeiculo, string modelo, string marca, string cor, string placa)
        {
            this.TipoVeiculo = tipoDeVeiculo;
            this.Modelo = modelo;
            this.Marca = marca;
            this.Cor = cor;
            this.Placa = placa;
            this.Lavagem = false;
            this.Revisão = false;
        }

        // Métodos
        public void DeterminarVaga(string vaga)
        {
            this.VagaEstacionada = vaga;
        }

        public void RemoverDaVaga()
        {
            this.VagaEstacionada = null;
        }

        public void LavarVeiculo()
        {
            this.Lavagem = true;
            Console.WriteLine($"Serviço de Lavagem atribuida ao veiculo de placa {this.Placa}");
        }
        public void FazerRevisão()
        {
            this.Revisão = true;
            Console.WriteLine($"Serviço de Revisão atribuida ao veiculo de placa {this.Placa}");
        }

    }
}
