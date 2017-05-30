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
                List<string> RegrasPico = new List<string>();
                List<string> RegrasArea = new List<string>();
                List<string> RegrasV1 = new List<string>();
                List<string> RegrasV2 = new List<string>();
                List<string> RegrasCorrente = new List<string>();


                for (int i = 0; i < ListaVariaveis[0].FuzzySet.Count; i++)
                {
                    if (item.Pico > ListaVariaveis[0].FuzzySet[i].PontoInicial &&
                        item.Pico < ListaVariaveis[0].FuzzySet[i].PontoFinal)
                    {

                        //RegrasAltura.Add("if " + ListaVariaveis[0].NomeVariavel + " is " + i + " * " + CalculaPertinencia(item.Altura, 
                        //    ListaVariaveis[0].FuzzySet[i].PontoInicial, ListaVariaveis[0].FuzzySet[i].PontoFinal));

                        RegrasPico.Add("if " + ListaVariaveis[0].NomeVariavel + " is " + i);
                    }
                }


                for (int i = 0; i < ListaVariaveis[1].FuzzySet.Count; i++)
                {
                    if (item.Area > ListaVariaveis[1].FuzzySet[i].PontoInicial &&
                        item.Area < ListaVariaveis[1].FuzzySet[i].PontoFinal)
                    {
                        //RegrasArea.Add(" and " + ListaVariaveis[1].NomeVariavel + " is " + i + " * " + CalculaPertinencia(item.Area,
                        //    ListaVariaveis[1].FuzzySet[i].PontoInicial, ListaVariaveis[1].FuzzySet[i].PontoFinal));

                        RegrasArea.Add(" and " + ListaVariaveis[1].NomeVariavel + " is " + i);
                    }
                }

                for (int i = 0; i < ListaVariaveis[2].FuzzySet.Count; i++)
                {
                    if (item.Area > ListaVariaveis[2].FuzzySet[i].PontoInicial &&
                        item.Area < ListaVariaveis[2].FuzzySet[i].PontoFinal)
                    {
                        //RegrasArea.Add(" and " + ListaVariaveis[1].NomeVariavel + " is " + i + " * " + CalculaPertinencia(item.Area,
                        //    ListaVariaveis[1].FuzzySet[i].PontoInicial, ListaVariaveis[1].FuzzySet[i].PontoFinal));

                        RegrasArea.Add(" and " + ListaVariaveis[2].NomeVariavel + " is " + i);
                    }
                }

                for (int i = 0; i < ListaVariaveis[3].FuzzySet.Count; i++)
                {
                    if (item.Area > ListaVariaveis[3].FuzzySet[i].PontoInicial &&
                        item.Area < ListaVariaveis[3].FuzzySet[i].PontoFinal)
                    {
                        //RegrasArea.Add(" and " + ListaVariaveis[1].NomeVariavel + " is " + i + " * " + CalculaPertinencia(item.Area,
                        //    ListaVariaveis[1].FuzzySet[i].PontoInicial, ListaVariaveis[1].FuzzySet[i].PontoFinal));

                        RegrasArea.Add(" and " + ListaVariaveis[3].NomeVariavel + " is " + i);
                    }
                }

                for (int i = 0; i < ListaVariaveis[4].FuzzySet.Count; i++)
                {
                    if (item.Corrente > ListaVariaveis[4].FuzzySet[i].PontoInicial &&
                        item.Corrente < ListaVariaveis[4].FuzzySet[i].PontoFinal)
                    {
                        //RegrasCorrente.Add(" then "+ ListaVariaveis[2].NomeVariavel +" is " + i + " * " + CalculaPertinencia(item.Corrente,
                        //    ListaVariaveis[2].FuzzySet[i].PontoInicial, ListaVariaveis[2].FuzzySet[i].PontoFinal));

                        RegrasCorrente.Add(" then " + ListaVariaveis[4].NomeVariavel + " is " + i);
                    }
                }


                List<string> regras = AgruparRegras(RegrasPico, RegrasArea, RegrasV1, RegrasV2, RegrasCorrente);

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

        private List<string> AgruparRegras(List<string> Altura, List<string> Area, List<string> V1, List<string> V2, List<string> Corrente)
        {
            List<string> Regras = new List<string>();

            foreach (var itemAltura in Altura)
            {
                foreach (var itemArea in Area)
                {
                    foreach (var itemV1 in V1)
                    {
                        foreach (var itemV2 in V2)
                        {
                            foreach (var itemCorrente in Corrente)
                            {

                                Regras.Add(itemAltura + itemArea + itemV1 + itemV2 + itemCorrente);


                                //string[] altura = itemAltura.Split('*');
                                //string[] area = itemArea.Split('*');
                                //string[] corrente = itemCorrente.Split('*');

                                //List<double> Pertinencia = new List<double>();
                                //Pertinencia.Add(Convert.ToDouble(altura[1]));
                                //Pertinencia.Add(Convert.ToDouble(area[1]));
                                //Pertinencia.Add(Convert.ToDouble(corrente[1]));


                                //Regras.Add(altura[0] + area[0] + corrente[0]);


                                //Regras.Add(altura[0] + area[0] + corrente[0] + Pertinencia.Min());

                                //   Regras.Add(altura[0] + area[0] + corrente[0] + "0,8");
                            }
                        }
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
