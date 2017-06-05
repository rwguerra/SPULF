using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjetoTransferencia;

namespace Negocios
{
    public class ClassRegras
    {
        public List<string> GerarRegras(List<Item> lista, List<VariavelLinguistica> ListaVariaveis)
        {
            List<string> Regras = RetornaRegras(lista, ListaVariaveis);

            Regras = LimparRegrasRepetidas(Regras);

            return Regras;
        }

        private List<string> RetornaRegras(List<Item> lista, List<VariavelLinguistica> ListaVariaveis)
        {

            List<string> Regras = new List<string>();


            foreach (var item in lista)
            {
                List<string> RegrasVariavel0 = new List<string>();
                List<string> RegrasVariavel1 = new List<string>();
                List<string> RegrasVariavel2 = new List<string>();



                for (int i = 0; i < ListaVariaveis[0].FuzzySet.Count; i++)
                {
                    if (item.Variavel0 > ListaVariaveis[0].FuzzySet[i].PontoInicial &&
                        item.Variavel0 < ListaVariaveis[0].FuzzySet[i].PontoFinal)
                    {

                        RegrasVariavel0.Add("if " + ListaVariaveis[0].NomeVariavel + " is " + i);
                    }
                }


                for (int i = 0; i < ListaVariaveis[1].FuzzySet.Count; i++)
                {
                    if (item.Variavel1 > ListaVariaveis[1].FuzzySet[i].PontoInicial &&
                        item.Variavel1 < ListaVariaveis[1].FuzzySet[i].PontoFinal)
                    {
                  

                        RegrasVariavel1.Add(" and " + ListaVariaveis[1].NomeVariavel + " is " + i);
                    }
                }

                for (int i = 0; i < ListaVariaveis[2].FuzzySet.Count; i++)
                {
                    if (item.Variavel2 > ListaVariaveis[2].FuzzySet[i].PontoInicial &&
                        item.Variavel2 < ListaVariaveis[2].FuzzySet[i].PontoFinal)
                    {


                        RegrasVariavel2.Add(" then " + ListaVariaveis[2].NomeVariavel + " is " + i);
                    }
                }


                List<string> regras = AgruparRegras(RegrasVariavel0, RegrasVariavel1, RegrasVariavel2);

                foreach (var itemregras in regras)
                {
                    Regras.Add(itemregras);
                }

            }

            return Regras;

        }

        private Double CalculaPertinencia(double Dado, double valorInicial, double valorFinal)
        {

            double valorMedio = valorInicial + ((Math.Abs(valorInicial) + Math.Abs(valorFinal)) / 2);
            double Pertinencia;

            if (Dado <= valorMedio)
            {
                Pertinencia = (Dado - valorInicial) / (valorMedio - valorInicial);
            }
            else
            {
                Pertinencia = (valorFinal - Dado) / (valorFinal - valorMedio);
            }

            return Pertinencia;
        }

        private List<string> AgruparRegras(List<string> RegrasVariavel0, List<string> RegrasVariavel1, List<string> RegrasVariavel2)
        {
            List<string> Regras = new List<string>();

            foreach (var item0 in RegrasVariavel0)
            {
                foreach (var item1 in RegrasVariavel1)
                {
                    foreach (var item2 in RegrasVariavel2)
                    {
                                Regras.Add(item0 + item1 + item2);


                            }
                        }
                    }
   
            return Regras;
        }


        private List<string> LimparRegrasRepetidas(List<string> Regras)
        {

            for (int j = 0; j < Regras.Count - 1; j++)
            {

                for (int i = j + 1; i < Regras.Count; i++)
                {
                    if (Regras[j] == Regras[i])
                    {
                        Regras.RemoveAt(i);
                    }
                }

            }

            return Regras;

        }




    }
}
