using System;

namespace exec04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Escreva um método que calcule a altura de uma árvore binária de pesquisa.

            ArvoreBinaria arvore = new ArvoreBinaria();
            arvore.Inserir(50); 
            arvore.Inserir(30);
            arvore.Inserir(70);
            arvore.Inserir(20);
            arvore.Inserir(40);
            arvore.Inserir(60);
            arvore.Inserir(80);
            arvore.Inserir(10);
            arvore.Inserir(15);
            //altura esperada: 4
            int altura = arvore.CalcularAltura(arvore.Raiz);
            Console.WriteLine($"A altura é: {altura}");
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
            int alturaEsq = 0; //calcular altura da esq
            int alturaDir = 0; //calcular altura dir
            if (i == null)
            {
                return 0;
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
            return (alturaDir > alturaEsq) ? alturaDir : alturaEsq; //para nao somar a mais, retornar qual a mais profunda
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