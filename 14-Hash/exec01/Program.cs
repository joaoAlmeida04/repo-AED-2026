using System;

namespace exec01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Cria uma tabela de tamanho 5 com 3 espaços na área de reserva
            Hash tabelaHash = new Hash(areaReserva: 3, tamanhoTabela: 5);
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n--- MENU TABELA HASH ---");
                Console.WriteLine("1 - Inserir elemento");
                Console.WriteLine("2 - Remover elemento");
                Console.WriteLine("3 - Verificar estado da tabela");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! Digite um número.");
                    continue;
                }

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            Console.Write("Digite o número inteiro positivo para inserir: ");
                            int valInserir = int.Parse(Console.ReadLine());
                            tabelaHash.Insercao(valInserir);
                            Console.WriteLine($"Elemento {valInserir} inserido!");
                            break;

                        case 2:
                            Console.Write("Digite o número para remover: ");
                            int valRemover = int.Parse(Console.ReadLine());
                            tabelaHash.Remocao(valRemover);
                            Console.WriteLine($"Elemento {valRemover} removido!");
                            break;

                        case 3:
                            ImprimirTabela(tabelaHash);
                            break;

                        case 0:
                            Console.WriteLine("Encerrando...");
                            break;

                        default:
                            Console.WriteLine("Opção inexistente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Aviso]: {ex.Message}");
                }
            }

            static void ImprimirTabela(Hash h)
            {
                Console.WriteLine("\nEstado atual do vetor:");
                for (int i = 0; i < h.tabela.Length; i++)
                {
                    string area = i < h.tamanhoTabela ? "Principal" : "Reserva  ";
                    string valor = h.tabela[i] == Hash.NULO ? "VAZIO" : h.tabela[i].ToString();
                    Console.WriteLine($"  [{i}] ({area}): {valor}");
                }
            }
        }
    }

    class Hash
    {
        public const int NULO = -1;
        public int[] tabela;
        public int areaReserva;
        public int tamanhoTabela;
        public int numeroDeReservas;

        public Hash(int areaReserva, int tamanhoTabela)
        {
            this.areaReserva = areaReserva;
            this.tamanhoTabela = tamanhoTabela;
            tabela = new int[tamanhoTabela + areaReserva];
            for (int i = 0; i < tabela.Length; i++)
            {
                tabela[i] = NULO; // Preencher com -1 para indicar que está vazio
            }
            numeroDeReservas = 0;
        }

        public void Insercao(int x)
        {
            int i = CalcHash(x);
            if (x < 0) // Evitar inserção de número negativo
            {
                throw new Exception("Erro: número negativo!");
            }
            else if (tabela[i] == NULO) // Significa que está vazio
            {
                tabela[i] = x;
            }
            else if (numeroDeReservas < areaReserva) // Inserir na reserva
            {
                tabela[tamanhoTabela + numeroDeReservas] = x;
                numeroDeReservas++;
            }
            else // Não tem espaço
            {
                throw new Exception("Erro: tabela e reserva cheias!");
            }
        }

        public int Pesquisar(int x)
        {
            int pos = CalcHash(x);
            int resp = 0;
            if (tabela[pos] == x) // verifica se esta no hash e retorna a posicao dele
            {
                resp = pos;
            }
            else if (tabela[pos] != -1)//verifica se o hash dele esta preenchido, se sim significa que pode estar na reserva
            {
                for (int i = tamanhoTabela; i < tamanhoTabela + areaReserva; i++)
                {
                    if (tabela[i] == x) resp = i; // caso encontre resp recebe a posicao                    
                }
            }
            return resp;
        }

        public int Remocao(int x)
        {
            //pesquisar se numero esta na tabela
            int pos = Pesquisar(x);
            if (pos == -1)
            {
                throw new Exception("Erro!");
            }
            else
            {
                tabela[pos] = -1; //remove logicamente
            }
            return pos;
        }


        public int CalcHash(int x)
        {
            return x % tamanhoTabela;
        }
    }
}
