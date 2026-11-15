using System;

namespace exe01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Escreva um método que percorra uma árvore binária de pesquisa e conte o número de nós 
folhas. Ao final, retorne essa informação.*/

            ArvoreBinaria arvore = new ArvoreBinaria();
            arvore.Inserir(50);
            arvore.Inserir(30);
            arvore.Inserir(70);
            arvore.Inserir(20);
            arvore.Inserir(40);
            arvore.Inserir(60);
            arvore.Inserir(80);

            int numNosFolha = arvore.ContarNosFolha(arvore.Raiz);
            Console.WriteLine("Número de nós folhas: " + numNosFolha);
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

        public bool Pesquisar(int x)
        {
            return Pesquisar(x, raiz);
        }
        private bool Pesquisar(int x, No i)
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
                resp = Pesquisar(x, i.Esq);
            }
            else
            {
                resp = Pesquisar(x, i.Dir);
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

        public int ContarNosFolha(No i)
        {
            if (i == null)//percorremos toda a arvore
            {
                return 0;
            }else if(i.Esq == null && i.Dir == null)
            {
                return 1; // caso seja folha soma +1
            }
            else{
                return 0 + ContarNosFolha(i.Esq) + ContarNosFolha(i.Dir); //recursividade para percorrer toda a arvore
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