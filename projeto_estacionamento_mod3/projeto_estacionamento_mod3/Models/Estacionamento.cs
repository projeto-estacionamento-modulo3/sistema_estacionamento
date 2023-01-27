using projeto_estacionamento_mod3.Models;
using projeto_estacionamento_mod3;
using System.Reflection;
using System.Text.RegularExpressions;

public class Estacionamento
{
    // Propriedades
    string nomeEstabelecimento { get; set; }
    public static List<Veiculo> ListaVeiculosCadastrados = new List<Veiculo>();
    public static List<Veiculo> ListaVeiculosEstacionados = new List<Veiculo>();
    public static List<string> ListaVagasIndisponiveis = new List<string>();

    int qtdeVagasDisponiveis { get; set; }
    int qtdeVagasOcupadas { get; set; }

    // Construtor
    public Estacionamento(string nomeEstabelecimento, int qtdeVagasDisponiveis)
    {
        this.nomeEstabelecimento = nomeEstabelecimento;
        this.qtdeVagasDisponiveis = qtdeVagasDisponiveis;
    }

    // Métodos
    public void Estacionar()
    {
        switch (qtdeVagasDisponiveis)
        {
            case 0:
                Console.WriteLine("Não há vagas disponiveis");
                break;

            default:
                bool localizouVeiculo = false;
                bool placaRepetida = false;
                bool vagarepetida = false;
                int contador = 0;

                //Lista veiculos que estão cadastrados no sistema.
                foreach (Veiculo veiculoDaLista in ListaVeiculosCadastrados)
                {
                    contador++;
                    Console.WriteLine($"{contador} - Placa:{veiculoDaLista.Placa}");
                }

                //Entra com a placa do veiculo que irá estacionar.
                Console.WriteLine($"Digite a placa do veiculo cadastrado que irá estacionar.");
                string selecaoDeVeiculo = Console.ReadLine().ToUpper();

                //Localiza se o veiculo informado, pela placa, está realmente cadastrado
                Console.WriteLine($"Localizando cadastro do veiculo... Aguarde um instante...");
                Thread.Sleep(3000);
                foreach (Veiculo veiculoDaLista in ListaVeiculosCadastrados)
                {
                    //se a placa informada corresponde com a lista de cadastrados.
                    if (selecaoDeVeiculo == veiculoDaLista.Placa)
                    {
                        //variavel de localização dispara.
                        localizouVeiculo = true;

                        // Verifica se o veiculo já esta estacionado
                        foreach (Veiculo veiculoEstacionado in ListaVeiculosEstacionados)
                        {
                            if (selecaoDeVeiculo == veiculoEstacionado.Placa)
                            {
                                placaRepetida = true;
                                break;
                            }
                        }

                        switch (placaRepetida)
                        {
                            case true:
                                Console.WriteLine("Este veiculo já esta estacionado");
                                break;

                            case false:
                                Console.WriteLine($"Cadastro do veiculo de placa {veiculoDaLista.Placa} localizado...");

                                Console.Write("\nQual vaga o veiculo sera estacionado: ");
                                string vaga = Console.ReadLine().ToUpper();

                                foreach (string vagaVeiculo in ListaVagasIndisponiveis)
                                {
                                    if (vagaVeiculo == vaga)
                                    {
                                        vagarepetida = true;
                                        break;
                                    }
                                }

                                switch (vagarepetida)
                                {
                                    case true:
                                        Console.WriteLine("Esta vaga ja esta sendo utilizada");
                                        break;

                                    case false:
                                        Console.WriteLine($"Estacionando Veículo na vaga {vaga}...");
                                        Thread.Sleep(2000);
                                        //determina a vaga do veidulo e seta a propriedade da classe veiculo do objeto.
                                        ListaVagasIndisponiveis.Add(vaga);
                                        veiculoDaLista.VagaEstacionada = vaga;
                                        veiculoDaLista.DeterminarVaga(vaga);
                                        Console.WriteLine($"Veículo estacionado");
                                        //Abre uma nova fatura como propriedade do objeto veiculo que foi estacionado.
                                        GerarNovaFatura(veiculoDaLista);
                                        //Adiciona o objeto veiculo a lista de veiculos estacionados.
                                        ListaVeiculosEstacionados.Add(veiculoDaLista);
                                        break;
                                }
                                break;

                        }

                        break;
                    }
                }

                switch (localizouVeiculo)
                {
                    //se a variavel for true de localizada, então ajusta a quantidade de vagas
                    case true:
                        if (!placaRepetida && !vagarepetida)
                        {
                            this.qtdeVagasDisponiveis -= 1;
                            this.qtdeVagasOcupadas += 1;
                        }
                        break;

                    //se a variavel nao setou como true, entao não houve estacionamento de veiculo.
                    case false:
                        Console.WriteLine($"Veículo não localizado na lista de cadastro, favor cadastrar veículo.");
                        break;
                }
                break;
        }

        // Voltando para o menu inicial.
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        Menu mtdAuxiliares = new Menu();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }

    public void RetirarVeiculo()
    {
        //Mesmo principio do método estacionar veiculo, porém está com problema no List.Remove da lista de veiculos estacionados.
        bool localizouVeiculo = false;
        int contador = 0;

        foreach (Veiculo veiculoDaLista in ListaVeiculosEstacionados)
        {
            contador++;
            Console.WriteLine($"{contador} - Placa:{veiculoDaLista.Placa} | Vaga: {veiculoDaLista.VagaEstacionada}");
        }

        Console.WriteLine($"Digite a placa do veiculo que será retirado.");

        string selecaoDeVeiculo = Console.ReadLine().ToUpper();

        int indiceVeiculo = 0;
        int indiceVaga = 0;
        Console.WriteLine($"Localizando veículo para retirada... Aguarde um instante...");
        foreach (Veiculo veiculoDaLista in ListaVeiculosEstacionados)
        {
            if (selecaoDeVeiculo == veiculoDaLista.Placa)
            {

                localizouVeiculo = true;
                Thread.Sleep(3000);
                Console.WriteLine($"Retirando veículo da vaga {veiculoDaLista.VagaEstacionada}...");

                Thread.Sleep(2000);
                Console.WriteLine($"Veículo retirado");
                veiculoDaLista.FaturaEstacionamento.DataSaída = DateTime.Now;

                //faz fechamento da fatura do veiculo
                FecharFatura(veiculoDaLista);

                //Localiza o indice do objeto a ser removido na lista.
                indiceVeiculo = ListaVeiculosEstacionados.IndexOf(veiculoDaLista);
                indiceVaga = ListaVagasIndisponiveis.IndexOf(veiculoDaLista.VagaEstacionada);
                veiculoDaLista.RemoverDaVaga();

                break;
            }
        }

        switch (localizouVeiculo)

        {
            case true:

                //Removo o objeto veiculo da lista de estacionados e o objeto vaga da lista de vagas indisponiveis de acordo com seu indice.
                ListaVeiculosEstacionados.Remove(ListaVeiculosEstacionados[indiceVeiculo]);
                ListaVagasIndisponiveis.Remove(ListaVagasIndisponiveis[indiceVaga]);

                foreach (string vagaa in ListaVagasIndisponiveis)
                {
                    Console.WriteLine(vagaa);
                }

                this.qtdeVagasDisponiveis += 1;
                this.qtdeVagasOcupadas -= 1;
                break;

            case false:
                Console.WriteLine("Este Veiculo não esta estacionado");
                break;
        }

        // Voltando para o menu inicial.
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        Menu mtdAuxiliares = new Menu();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }

    public void VerVagasDisponiveis()
    {
        Console.WriteLine($"No momento há vagas {this.qtdeVagasDisponiveis} disponíveis");

        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        Menu mtdAuxiliares = new Menu();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }

    public void ListarVeiculosEstacionados()
    {
        Console.WriteLine($"- Veículos Estacionados -");
        int contador = 0;
        //lista os veiculos da lista de veiculos estacionados.
        foreach (Veiculo veiculoDaLista in ListaVeiculosEstacionados)
        {
            contador++;
            Console.WriteLine($"{contador} - Placa:{veiculoDaLista.Placa} | Vaga: {veiculoDaLista.VagaEstacionada}");
        }

        // Voltando para o menu inicial.
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        Menu mtdAuxiliares = new Menu();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }

    public void CadastrarVeiculo()
    {
        Console.WriteLine("- Cadastro De Veiculos -");

        Console.Write("Digite o tipo do veículo: ");
        string tipoVeiculo = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(tipoVeiculo))
        {
            Console.WriteLine("Informe o tipo de veículo:");
            tipoVeiculo = Console.ReadLine();
        }

        Console.Write("Digite a marca do veículo: ");
        string marcaVeiculo = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(marcaVeiculo))
        {
            Console.WriteLine("Informe a marca do veículo:");
            marcaVeiculo = Console.ReadLine();
        }

        Console.Write("Digite o modelo do veículo: ");
        string modeloVeiculo = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(modeloVeiculo))
        {
            Console.WriteLine("Informe o modelo do veículo:");
            modeloVeiculo = Console.ReadLine();
        }

        Console.Write("Digita a cor do veículo: ");
        string corVeiculo = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(corVeiculo))
        {
            Console.WriteLine("Informe a cor do veículo:");
            corVeiculo = Console.ReadLine();
        }

        // Placa do Veiculo
        string placaVeiculo;
        bool placaInvalida;
        do
        {
            placaInvalida = false;

            Console.Write("Digite a placa do veículo: ");
            placaVeiculo = Console.ReadLine().ToUpper();

            // Valida se a placa segue o modelo Mercosul
            Regex regex = new Regex(@"^[A-Z]{3}\d{4}$");
            if (!regex.IsMatch(placaVeiculo))
            {
                Console.WriteLine("Por Favor, informe a placa do veiculo de acordo com o seguinte modelo ABC1234\n");
                placaInvalida = true;
                continue;
            }

            // Valida se a placa já foi cadastrada
            foreach (Veiculo veiculoDaLista in ListaVeiculosCadastrados)
            {
                if (placaVeiculo == veiculoDaLista.Placa)
                {
                    placaInvalida = true;
                    Console.WriteLine("Esta placa já foi cadastrada no sistema, verifique a placa digitada e tente novamente\n");
                    break;
                }
            }

        } while (placaInvalida);



        //Instancia um novo objeto veiculo baseado nas variaves informadas.
        Veiculo veiculoCadastrado = GeraVeiculo.GerarNovoVeiculo(tipoVeiculo, modeloVeiculo, marcaVeiculo, corVeiculo, placaVeiculo);

        //Adiciona o veiculo na lista de veiculos cadastrados no sistema.
        ListaVeiculosCadastrados.Add(veiculoCadastrado);
        Console.WriteLine($"Veiculo de placa {veiculoCadastrado.Placa} cadastrado com sucesso!");

        // Voltando para o menu inicial.
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        Menu mtdAuxiliares = new Menu();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }

    public void ListarTodosVeiculosCadastrados()
    {
        //Lista todos os veiculos cadastrados no sistema do estacionamento.
        Console.WriteLine($"- Veiculos Cadastrados -");
        int contador = 0;

        foreach (Veiculo veiculoDaLista in ListaVeiculosCadastrados)
        {
            contador++;
            Console.WriteLine($"{contador} - Placa:{veiculoDaLista.Placa}");
        }

        // Voltando para o menu inicial.
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        Menu mtdAuxiliares = new Menu();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }

    private void GerarNovaFatura(Veiculo veiculoEstacionado)
    {
        //Metodo para principio da responsabilidade unica.
        GeraFatura.AbrirNovaFatura(veiculoEstacionado);
    }

    //Mostrar o valor total da fatura 
    private void FecharFatura(Veiculo veiculoEstacionado)
    {

        veiculoEstacionado.FaturaEstacionamento.CalcularValorTotal(veiculoEstacionado.Lavagem, veiculoEstacionado.Revisão);


        //Retorna o tipo do objeto por reflection.
        Type tipoDoObjeto = veiculoEstacionado.GetType();

        //Seta as prorpiedades do objeto para false por reflection.
        PropertyInfo PropriedadeLavagem = tipoDoObjeto.GetProperty("Lavagem");
        PropriedadeLavagem.SetValue(veiculoEstacionado, false);
        PropertyInfo PropriedadeRevisao = tipoDoObjeto.GetProperty("Revisão");
        PropriedadeRevisao.SetValue(veiculoEstacionado, false);


    }

    public void AplicarServicosExtras()
    {
        //bool localizouVeiculo = false;
        int contador = 0;
        Menu mtdAuxiliares = new Menu();

        //Lista veiculos que estão estacionados.
        foreach (Veiculo veiculoDaLista in ListaVeiculosEstacionados)
        {
            contador++;
            Console.WriteLine($"{contador} - Placa:{veiculoDaLista.Placa}");
        }

        if (ListaVeiculosEstacionados.Count == 0)
        {
            Console.WriteLine("Não há veículos estacionados...");
            Console.WriteLine("Só é possível solicitar serviços para carros estacionados.");
            Console.WriteLine();
            Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
            Console.ReadKey();
            Console.Clear();
            mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
        }
        //Entra com a placa do veiculo que usar dos serviços.
        Console.WriteLine($"Digite a placa do veiculo estacionado que irá utilizar algum serviço extra.");
        string selecaoDeVeiculo = Console.ReadLine().ToUpper();

        //Localiza se o veiculo informado, pela placa, está realmente estacionado
        Console.WriteLine($"Localizando veiculo... Aguarde um instante...");
        foreach (Veiculo veiculoDaLista in ListaVeiculosEstacionados)
        {
            //se a placa informada corresponde com a lista de estacionados.
            if (selecaoDeVeiculo == veiculoDaLista.Placa)
            {
                Console.WriteLine("Que tipo de serviço deseja para seu veiculo?");
                Console.WriteLine("1 - Lavagem do veiculo");
                Console.WriteLine("2 - Revisão do veiculo");
                int.TryParse(Console.ReadLine(), out int escolha);

                switch (escolha)
                {
                    case 1:
                        veiculoDaLista.LavarVeiculo();
                        break;
                    case 2:
                        veiculoDaLista.FazerRevisão();
                        break;
                    default:
                        Console.WriteLine("Opção digitada inexistente");
                        break;
                }
            }

        }
        // Voltando para o menu inicial.
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar ao menu inicial: ");
        Console.ReadKey();
        Console.Clear();
        mtdAuxiliares.MostrarMenu(nomeEstabelecimento, qtdeVagasDisponiveis);
    }
}