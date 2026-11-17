using System;

namespace exec03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Escreva um método para encontrar o maior e o menor elemento em uma árvore binária de 
            //pesquisa. 

            ArvoreBinaria arvore = new ArvoreBinaria();
            arvore.Inserir(50); //interno
            arvore.Inserir(30);//interno
            arvore.Inserir(70);//interno
            arvore.Inserir(20);//externo
            arvore.Inserir(40);//externo
            arvore.Inserir(60);//externo
            arvore.Inserir(80);//externo

            int maior = arvore.EncontrarNoMaior(0, arvore.Raiz ); //resultado esperado: 80
            int menor = arvore.EncontrarNoMenor(arvore.Raiz.Elemento, arvore.Raiz ); // resultado esperado: 20. Testado tambem com valores minimos de 10 e max de 300, resultados sairam como esperado
            Console.WriteLine($"O maior valor é: {maior}, e o menor é: {menor}");
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
            } else if (x <= i.Elemento)
            {
                maior = i.Elemento;
                maior = EncontrarNoMaior(maior, i.Dir);
            }else
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
            } else if (x >= i.Elemento)
            {
                menor = i.Elemento;
                menor = EncontrarNoMenor(menor, i.Esq);
            }else
            {
                menor = EncontrarNoMenor(menor, i.Dir);
            }
            return menor;
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