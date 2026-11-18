using System;

namespace exec01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Faça um método que, ao receber uma árvore binária qualquer, defina se é uma árvore AVL 
            //(lembrando-se das restrições de altura).

            ArvoreBinaria arvoreB = new ArvoreBinaria();
            arvoreB.Inserir(50);
            arvoreB.Inserir(30);
            arvoreB.Inserir(70);
            arvoreB.Inserir(20);
            arvoreB.Inserir(40);
            arvoreB.Inserir(60);
            arvoreB.Inserir(80);

            bool resultado = arvoreB.VerificarAVL(arvoreB.Raiz);
            if (resultado)
            {
                Console.WriteLine("A árvore é AVL e está balanceada");
            }
            else
            {
                Console.WriteLine("A árvore não é AVL e não está balanceada");
            }
        }
    }

    class ArvoreBinaria
    {
        private No raiz;
        public ArvoreBinaria()
        {
            raiz = null;
        }
        public No Raiz { get => raiz; set => raiz = value; }
        public void Inserir(int x)
        {
            raiz = Inserir(x, raiz);
        }
        private No Inserir(int x, No i)
        {
            if (i == null)
            {
                i = new No(x);
            }
            else if (x < i.Elemento)
            {
                i.Esq = Inserir(x, i.Esq);
            }
            else if (x > i.Elemento)
            {
                i.Dir = Inserir(x, i.Dir);
            }
            else
            {
                throw new Exception("Erro!");
            }
            return i;
        }

        public bool PEsquisar(int x)
        {
            return PEsquisar(x, raiz);
        }
        private bool PEsquisar(int x, No i)
        {
            bool resp;
            if (i == null)
            {
                resp = false;
            }
            else if (x == i.Elemento)
            {
                resp = true;
            }
            else if (x < i.Elemento)
            {
                resp = PEsquisar(x, i.Esq);
            }
            else
            {
                resp = PEsquisar(x, i.Dir);
            }
            return resp;
        }

        public void CaminharCentral(No i)
        {

            if (i != null)
            {
                CaminharCentral(i.Esq);
                Console.Write(i.Elemento + "");
                CaminharCentral(i.Dir);
            }
        }

        public int ContarNosInternos(No i)
        {
            if (i == null)
            {
                return 0;
            }
            else if (i.Esq == null && i.Dir == null)
            {
                return 0;
            }
            else
            {
                return 1 + ContarNosInternos(i.Esq) + ContarNosInternos(i.Dir);
            }
        }

        public int EncontrarNoMaior(int x, No i)
        {
            int maior = x;
            if (i == null)
            {
                return maior;
            }
            else if (x <= i.Elemento)
            {
                maior = i.Elemento;
                maior = EncontrarNoMaior(maior, i.Dir);
            }
            else
            {
                maior = EncontrarNoMaior(maior, i.Esq);
            }
            return maior;
        }

        public int EncontrarNoMenor(int x, No i)
        {
            int menor = x;
            if (i == null)
            {
                return menor;
            }
            else if (x >= i.Elemento)
            {
                menor = i.Elemento;
                menor = EncontrarNoMenor(menor, i.Esq);
            }
            else
            {
                menor = EncontrarNoMenor(menor, i.Dir);
            }
            return menor;
        }

        public int CalcularAltura(No i)
        {
            int alturaEsq = 0;
            int alturaDir = 0;
            if (i == null)
            {
                return -1;
            }
            if (i.Esq != null)
            {
                alturaEsq = CalcularAltura(i.Esq);
                ++alturaEsq;
            }
            if (i.Dir != null)
            {
                alturaDir = CalcularAltura(i.Dir);
                ++alturaDir;
            }
            return (alturaDir > alturaEsq) ? alturaDir : alturaEsq;
        }

        public int FatorBalanceamento(No i)
        {
            if (i == null)
            {
                return 0;
            }
            return CalcularAltura(i.Dir) - CalcularAltura(i.Esq);
        }

        public bool VerificarAVL(No i)
        {
            bool result = false;
            int fator = FatorBalanceamento(i);
            if (i == null) //caso base para parada do metodo, indica que percorremos a arvore toda
            {
                return true;
            }
            else if (fator < -1 || fator > 1) //verifica se o fator de balanceamento e valido se nao for retorna falso
            {
                return false;
            }
            result = VerificarAVL(i.Esq) && VerificarAVL(i.Dir);
            return result;
        }
    }

    class No
    {
        private int elemento;
        private No esq;
        private No dir;
        public No(int elemento)
        {
            this.elemento = elemento;
            esq = null;
            dir = null;
        }
        public No(int elemento, No esq, No dir)
        {
            this.elemento = elemento;
            this.esq = esq;
            this.dir = dir;
        }

        public int Elemento { get => elemento; set => elemento = value; }
        public No Esq { get => esq; set => esq = value; }
        public No Dir { get => dir; set => dir = value; }

    }

}
