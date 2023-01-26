using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_estacionamento_mod3.Models
{
    public class Fatura
    {
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaída { get; set; }
        public decimal ValorHora { get; set; }

        //public decimal TaxaNoturna { get; set; }
        public Veiculo VeiculoDaFatura { get; set; }
        public int NumeroDaFatura { get; set; }

        public Fatura(Veiculo veiculo, DateTime dataEntrada)
        {
            this.DataEntrada = dataEntrada;
            this.VeiculoDaFatura = veiculo;
            this.ValorHora = 15;
        }

        public void DeterminarValorHora(decimal valorHora)
        {

            this.ValorHora = valorHora;

        }

        public void CalcularValorTotal(bool lavagem, bool revisao)
        {
            if (lavagem == true || revisao == true)
            {
                DateTime entrada = this.DataEntrada;
                DateTime saida = this.DataSaída;

                TimeSpan intervalo = saida - entrada;

                decimal segundos = intervalo.Seconds;

                //Os segundos são considerados as horas para escopo de projeto (não ter que esperar horas para calcular um valor)
                Console.WriteLine($"Período estacionado: {segundos} horas");
                
                //Apenas para demonstrar que um segundo na aplicação corresponde a uma hora da vida real.
                Console.WriteLine($"intervalo de {segundos} segundos (1 segundo = 1 hora");

                //Os segundos são considerados como as horas, apenas para que possa ser utlizado no escopo do projeto.
                decimal valorTotal = segundos * ValorHora;
                Console.WriteLine($"Valor da hora: {this.ValorHora} reais/hora");
                Console.WriteLine($"Valor referente ao estacionamento {valorTotal} reais");
                if (lavagem == true && revisao == false)
                {
                    Console.WriteLine($"Valor referente lavegem: 20 reais");
                    Console.WriteLine($"Valor total a ser pago: {valorTotal + 20} reais");
                }
                else if (lavagem == false && revisao == true)
                {
                    Console.WriteLine($"Valor referente revisão: 40 reais");
                    Console.WriteLine($"Valor total a ser pago: {valorTotal + 40} reais");
                }
                else if (lavagem == true && revisao == true)
                {
                    Console.WriteLine($"Valor referente revisão: 40 reais");
                    Console.WriteLine($"Valor referente lavagem: 20 reais");
                    Console.WriteLine($"Valor total a ser pago: {valorTotal + 40 + 20} reais");
                }
            }
            else
            {
                DateTime entrada = this.DataEntrada;
                DateTime saida = this.DataSaída;

                TimeSpan intervalo = saida - entrada;

                decimal segundos = intervalo.Seconds;

                //Os segundos são considerados as horas para escopo de projeto (não ter que esperar horas para calcular um valor)
                Console.WriteLine($"Período estacionado: {segundos} horas");

                Console.WriteLine($"intervalo {segundos} segundos");

                //Os segundos são considerados como as horas, apenas para que possa ser utlizado no escopo do projeto.
                decimal valorTotal = segundos * ValorHora;

                Console.WriteLine($"Valor da hora: {this.ValorHora} reais/hora");
                Console.WriteLine($"Valor referente ao estacionamento {valorTotal} reais");
                Console.WriteLine($"Valor referente serviços extras: Não utilizado");
                Console.WriteLine($"Valor total a ser pago: {valorTotal} reais");
            }

        }

    }
}
