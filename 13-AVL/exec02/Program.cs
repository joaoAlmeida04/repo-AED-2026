using System;

namespace exec02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Implemente uma função para converter uma árvore AVL em uma lista ordenada. Justifique 
            //por que o tempo de execução é O(n), onde n é o número de nós na árvore.

            //O tempo de execução seria O(n) por que para transformar a AVL em uma lista precisamos percorrer todos os elementos dessa AVL para inseri-los na lista 

            List<int> lista = new List<int>();
            ArvoreBinaria arvoreB = new ArvoreBinaria();

            arvoreB.Inserir(50);
            arvoreB.Inserir(30);
            arvoreB.Inserir(70);
            arvoreB.Inserir(20);
            arvoreB.Inserir(40);
            arvoreB.Inserir(60);
            arvoreB.Inserir(80);

            arvoreB.ConverterLista(arvoreB.Raiz, lista);
            MostrarUmaLista(lista);
        }

        static void MostrarUmaLista(List<int> l)
        {
            Console.WriteLine("Exibindo elementos da lista: ");
            int count = 0;
            foreach (int i in l)
            {
                Console.WriteLine($"Elemento {count}: {i}");
                count++;
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
                Console.Write(i.Elemento + " ");
                CaminharCentral(i.Dir);
            }
        }

        public void ConverterLista(No i, List<int> l)
        {

            if (i != null)
            {
                ConverterLista(i.Esq, l);
                l.Add(i.Elemento);
                ConverterLista(i.Dir, l);
            }
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