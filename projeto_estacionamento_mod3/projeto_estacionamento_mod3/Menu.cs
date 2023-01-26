using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_estacionamento_mod3.Models;

namespace projeto_estacionamento_mod3
{
    public class Menu
    {
        public void MostrarMenu(string nomeEstabelecimento, int qtdeVagasDisponiveis)
        {
            Console.WriteLine("--- MENU INICIAL ---");
            Console.WriteLine();

            // Mostrando as oções de ação para o usuário.
            Console.WriteLine("Digite 1 para estacionar um veiculo.");
            Console.WriteLine("Digite 2 para retirar um veiculo.");
            Console.WriteLine("Digite 3 para ver os veiculos estacionados.");
            Console.WriteLine("Digite 4 para cadastrar um veiculo.");
            Console.WriteLine("Digite 5 para ver todos os veiculos cadastrados.");
            Console.WriteLine("Digite 6 para Serviços Extras oferecidos.");
            Console.WriteLine("Digite 7 para sair.");

            Console.WriteLine();

            // Validando a opção do usuario
            int opcDoMenuEscolhida;
            do
            {
                Console.Write("Digite uma das opções acima: ");
                int.TryParse(Console.ReadLine(), out opcDoMenuEscolhida);

                if (opcDoMenuEscolhida < 1 || opcDoMenuEscolhida > 7)
                {
                    Console.WriteLine("Opção digitada inexistente\n");
                }

            } while (opcDoMenuEscolhida < 1 || opcDoMenuEscolhida > 7);

            Console.WriteLine();

            // Tomando uma ação baseada na resposta do menu do usuário.
            Estacionamento estacionamento = new Estacionamento(nomeEstabelecimento, qtdeVagasDisponiveis);

            switch (opcDoMenuEscolhida)
            {
                case 1:
                    estacionamento.Estacionar();
                    break;
                case 2:
                    estacionamento.RetirarVeiculo();
                    break;
                case 3:
                    estacionamento.ListarVeiculosEstacionados();
                    break;
                case 4:
                    estacionamento.CadastrarVeiculo();
                    break;
                case 5:
                    estacionamento.ListarTodosVeiculosCadastrados();
                    break;
                case 6:
                    estacionamento.AplicarServicosExtras();
                    break;
                case 7:
                    break;
            }
        }

    }
}
