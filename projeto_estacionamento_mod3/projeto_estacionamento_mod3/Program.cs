namespace projeto_estacionamento_mod3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Pegando os dados do estacionamento do usuário.
            Console.Write("Digite o nome do seu estabelecimento: ");
            string nomeEstabelecimento = Console.ReadLine();

            bool sucesso;
            int qtdeVagasDisponiveis;
            do
            {
                Console.Write("Digite a quantidade de vagas totais: ");
                sucesso = int.TryParse(Console.ReadLine(), out qtdeVagasDisponiveis);

                if (!sucesso || qtdeVagasDisponiveis <= 0)
                {
                    Console.WriteLine("Por Favor, digite um numero de vagas valido utilizando apenas numeros\n");
                }

            } while (!sucesso || qtdeVagasDisponiveis <= 0);

            Console.Clear();
            Console.WriteLine("Meus parabéns, seu estacionamento foi cadastrado com sucesso! Dados do seu cadastro: ");
            Console.WriteLine();
            Console.WriteLine("Nome do seu estacionamento: " + nomeEstabelecimento + ".");
            Console.WriteLine("Quantidade de vagas disponiveis: " + qtdeVagasDisponiveis + ".");
            //Console.WriteLine("Quantidade de vagas ocupadas: " + qtdeVagasOcupadas + ".");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            // Oferecendo opções para o usuário.
            Menu mtdAuxiliares = new Menu();
            mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);

            // Fechando app
            Console.Write("\nFechando Sistema");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.Clear();
            Console.WriteLine("Sistema Fechado");

        }
    }
}