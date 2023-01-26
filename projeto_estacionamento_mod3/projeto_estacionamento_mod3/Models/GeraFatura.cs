using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_estacionamento_mod3.Models
{
    public class GeraFatura
    {
        //Propriedades
        public static List<Veiculo> ListaVeiculosEstacionados2 = new List<Veiculo>();
        //Classe para instancia do objeto fatura, principio de responsabilidade unica solid.
        public static void AbrirNovaFatura(Veiculo veiculoEstacionado)
        {
            //Metodo para abrir nova fatura
            //Salva a hora que foi chamado.
            DateTime dataVeiculoEstacionado = DateTime.Now;
            //Instancia um novo objeto do tipo Fatura
            Fatura novaFatura = new Fatura(veiculoEstacionado, dataVeiculoEstacionado);
            //Armazena o objeto do tipo fatura na propriedade do objeto veiculo que chamou o metodo e foi passado por parametro.
            veiculoEstacionado.FaturaEstacionamento = novaFatura;

            ListaVeiculosEstacionados2.Add(veiculoEstacionado);
            //Define uma numeração para a fatura.
            novaFatura.NumeroDaFatura = (ListaVeiculosEstacionados2.Count());
            novaFatura.DataEntrada = dataVeiculoEstacionado;

            Console.WriteLine($"Nova fatura de número {novaFatura.NumeroDaFatura} aberta para o veiculo {veiculoEstacionado.Placa}.");
        }
    }
}
