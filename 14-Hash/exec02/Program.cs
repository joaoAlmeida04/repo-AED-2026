using System;

namespace exec02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tabela principal com 7 posições e área de reserva com 3 posições
            Hash tabelaHash = new Hash(areaReserva: 3, tamanhoTabela: 7);

            Console.WriteLine("=== 1. INSERÇÃO ===");
            // 10 % 7 = 3 (vai para índice 3)
            // 17 % 7 = 3 (colisão -> vai para reserva, índice 7)
            // 24 % 7 = 3 (colisão -> vai para reserva, índice 8)
            // 14 % 7 = 0 (vai para índice 0)
            // 5  % 7 = 5 (vai para índice 5)
            int[] valores = { 10, 17, 24, 14, 5 };

            foreach (int v in valores)
            {
                tabelaHash.Insercao(v);
                Console.WriteLine($"Inserido: {v} (Hash: {tabelaHash.CalcHash(v)})");
            }

            ImprimirTabela(tabelaHash);

            Console.WriteLine("\n=== 2. PESQUISA ===");
            Console.WriteLine($"Posição do 10 (principal): {tabelaHash.Pesquisar(10)}");
            Console.WriteLine($"Posição do 24 (reserva):   {tabelaHash.Pesquisar(24)}");
            Console.WriteLine($"Posição do 99 (inexistente): {tabelaHash.Pesquisar(99)}");

            Console.WriteLine("\n=== 3. REMOÇÃO NA ÁREA PRINCIPAL (COM COLISÃO NA RESERVA) ===");
            // Remove o 10 (índice 3). O 17 (que estava na reserva) deve assumir o índice 3
            int removido1 = tabelaHash.Remocao(10);
            Console.WriteLine($"Removido: {removido1}");
            ImprimirTabela(tabelaHash);

            // Garante que o 24 (que ainda está na reserva) continua sendo encontrado
            Console.WriteLine($"Pesquisando 24 após remoção do 10 -> Posição: {tabelaHash.Pesquisar(24)}");

            Console.WriteLine("\n=== 4. REMOÇÃO NA ÁREA DE RESERVA ===");
            int removido2 = tabelaHash.Remocao(24);
            Console.WriteLine($"Removido: {removido2}");
            ImprimirTabela(tabelaHash);

            Console.WriteLine("\n=== 5. TESTE DE EXCEÇÃO (RESERVA CHEIA) ===");
            try
            {
                tabelaHash.Insercao(31); // 31 % 7 = 3 (reserva 1)
                tabelaHash.Insercao(38); // 38 % 7 = 3 (reserva 2)
                tabelaHash.Insercao(45); // 45 % 7 = 3 (reserva 3)
                tabelaHash.Insercao(52); // Deve estourar a reserva
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção capturada com sucesso: {ex.Message}");
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
                int j = CalcHash(x), resp = NULO;
                if (tabela[j] == x)
                {
                    resp = j;
                }
                else if (tabela[j] != NULO)
                {
                    for (int i = 0; i < numeroDeReservas; i++)
                    {
                        if (tabela[tamanhoTabela + i] == x)
                        {
                            resp = tamanhoTabela + i;
                            i = numeroDeReservas;
                        }
                    }
                }
                return resp;
            }

            public int Remocao(int x)
            {
                int pos = Pesquisar(x);

                if (pos == NULO)
                {
                    throw new Exception("Erro: elemento não encontrado!");
                }

                int removido = tabela[pos];

                // Caso 1: O elemento está na área de reserva
                if (pos >= tamanhoTabela)
                {
                    RemoverDaReserva(pos);
                }
                // Caso 2: O elemento está na área principal
                else
                {
                    int posSubstituto = NULO;

                    // Procura na reserva o primeiro elemento com o mesmo hash
                    for (int i = 0; i < numeroDeReservas; i++)
                    {
                        if (CalcHash(tabela[tamanhoTabela + i]) == pos)
                        {
                            posSubstituto = tamanhoTabela + i;
                            break;
                        }
                    }

                    if (posSubstituto != NULO)
                    {
                        // Traz o substituto da reserva para a área principal
                        tabela[pos] = tabela[posSubstituto];
                        RemoverDaReserva(posSubstituto);
                    }
                    else
                    {
                        // Nenhuma colisão na reserva para este índice, pode esvaziar
                        tabela[pos] = NULO;
                    }
                }

                return removido;
            }

            // Método auxiliar para remover da reserva e deslocar os elementos seguintes para não deixar buracos
            private void RemoverDaReserva(int posReserva)
            {
                int fimReserva = tamanhoTabela + numeroDeReservas - 1;

                for (int i = posReserva; i < fimReserva; i++)
                {
                    tabela[i] = tabela[i + 1];
                }

                tabela[fimReserva] = NULO;
                numeroDeReservas--;
            }

            public int CalcHash(int x)
            {
                return x % tamanhoTabela;
            }
        }
    }
}