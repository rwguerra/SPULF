using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Fuzzy;
using ObjetoTransferencia;

namespace Negocios
{
    public class PrevisaoFuzzy
    {
        private InferenceSystem IS;
        List<VariavelLinguistica> ListaVariaveis;

        public double Previsao(List<Item> listaDados, List<VariavelLinguistica> _ListaVariaveis)
        {
            ListaVariaveis = _ListaVariaveis;

            listaDados = quebraDados(listaDados);

            DefinicaoVariaveisLinguisticas(listaDados);

            InitFuzzyEngine(listaDados);

            double previsao = DoInference(listaDados[listaDados.Count-1].Variavel1, listaDados[listaDados.Count - 1].Variavel2);

            return previsao;

        }

        private List<Item> quebraDados(List<Item> listaDados)
        {
            List<Item> DadosAtualizados = new List<Item>();

            for (int i = 0; i < listaDados.Count - 2; i++)
            {
                Item item = new Item();

                item.Variavel0 = listaDados[i].DadosOriginais;
                item.Variavel1 = listaDados[i+1].DadosOriginais;
                item.Variavel2 = listaDados[i+2].DadosOriginais;


                DadosAtualizados.Add(item);
            }

            return DadosAtualizados;

        }



        void InitFuzzyEngine(List<Item> lista)
        {
            LinguisticVariable Variavel0 = new LinguisticVariable(ListaVariaveis[0].NomeVariavel,
                Convert.ToSingle(ListaVariaveis[0].RangeInicial), Convert.ToSingle(ListaVariaveis[0].RangeFinal));

            for (int i = 0; i < ListaVariaveis[0].FuzzySet.Count; i++)
            {
                Variavel0.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[0].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[0].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[0].FuzzySet[i].PontoFinal))));
            }

            LinguisticVariable Variavel1 = new LinguisticVariable(ListaVariaveis[1].NomeVariavel,
                Convert.ToSingle(ListaVariaveis[1].RangeInicial), Convert.ToSingle(ListaVariaveis[1].RangeFinal));

            for (int i = 0; i < ListaVariaveis[1].FuzzySet.Count; i++)
            {
                Variavel1.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[1].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[1].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[1].FuzzySet[i].PontoFinal))));
            }

            LinguisticVariable Variavel2 = new LinguisticVariable(ListaVariaveis[2].NomeVariavel,
                Convert.ToSingle(ListaVariaveis[2].RangeInicial), Convert.ToSingle(ListaVariaveis[2].RangeFinal));

            for (int i = 0; i < ListaVariaveis[2].FuzzySet.Count; i++)
            {
                Variavel2.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[2].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[2].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[2].FuzzySet[i].PontoFinal))));
            }

            // The database
            Database fuzzyDB = new Database();
            fuzzyDB.AddVariable(Variavel0);
            fuzzyDB.AddVariable(Variavel1);
            fuzzyDB.AddVariable(Variavel2);


            // Creating the inference system
            IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

            ClassRegras classRegras = new ClassRegras();
            List<string> Regras = classRegras.GerarRegras(lista, ListaVariaveis);

            for (int i = 0; i < Regras.Count; i++)
            {
                IS.NewRule("Rule " + i, Regras[i]);
            }

        }

        private double DoInference(double V1, double V2)
        {
            // Setting inputs
            IS.SetInput(ListaVariaveis[0].NomeVariavel, Convert.ToSingle(V1));
            IS.SetInput(ListaVariaveis[1].NomeVariavel, Convert.ToSingle(V2));

            // Setting outputs
            try
            {
                double NewDado = IS.Evaluate(ListaVariaveis[2].NomeVariavel);

                return NewDado;
            }
            catch (Exception)
            {
                return V2;
            }
        }

        private void DefinicaoVariaveisLinguisticas(List<Item> listaDados)
        {
            List<double> V0 = new List<double>();
            List<double> V1 = new List<double>();
            List<double> V2 = new List<double>();

            foreach (var item in listaDados)
            {
                V0.Add(item.Variavel0);
                V1.Add(item.Variavel1);
                V2.Add(item.Variavel2);
            }

            //double espaco = (Altura.Max() - Altura.Min()) / (ListaVariaveis[0].QntMS - 1);

            ListaVariaveis[0].RangeInicial = V0.Min() - ((V0.Max() - V0.Min()) / (ListaVariaveis[0].QntMS - 1));
            ListaVariaveis[0].RangeFinal = V0.Max() + ((V0.Max() - V0.Min()) / (ListaVariaveis[0].QntMS - 1));
            ListaVariaveis[0].FuzzySet = GerarFuzzySet(V0.Min(),
                V0.Max(), ListaVariaveis[0].QntMS);


            ListaVariaveis[1].RangeInicial = V1.Min() - ((V1.Max() - V1.Min()) / (ListaVariaveis[1].QntMS - 1));
            ListaVariaveis[1].RangeFinal = V1.Max() + ((V1.Max() - V1.Min()) / (ListaVariaveis[1].QntMS - 1));
            ListaVariaveis[1].FuzzySet = GerarFuzzySet(V1.Min(),
                V1.Max(), ListaVariaveis[1].QntMS);

            ListaVariaveis[2].RangeInicial = V2.Min() - ((V2.Max() - V2.Min()) / (ListaVariaveis[1].QntMS - 1));
            ListaVariaveis[2].RangeFinal = V2.Max() + ((V2.Max() - V2.Min()) / (ListaVariaveis[1].QntMS - 1));
            ListaVariaveis[2].FuzzySet = GerarFuzzySet(V2.Min(),
                V2.Max(), ListaVariaveis[1].QntMS);



        }

        List<Ponto> GerarFuzzySet(double RangeInicial, double RangeFinal, int QntMS)
        {
            List<Ponto> pontos = new List<Ponto>();

            double espaco = (RangeFinal - RangeInicial) / (QntMS - 1);

            Ponto ponto = new Ponto();
            ponto.PontoInicial = RangeInicial - espaco;
            ponto.PontoFinal = RangeInicial + espaco;
            ponto.PontoMedio = (RangeInicial + RangeFinal) / 2;
            pontos.Add(ponto);



            for (int i = 0; i < (QntMS - 2); i++)
            {
                ponto = new Ponto();
                ponto.PontoInicial = (i * espaco) + RangeInicial;
                ponto.PontoFinal = ponto.PontoInicial + 2 * espaco;
                ponto.PontoMedio = (ponto.PontoInicial + ponto.PontoFinal) / 2;
                pontos.Add(ponto);
            }

            ponto = new Ponto();
            ponto.PontoInicial = RangeFinal - espaco;
            ponto.PontoFinal = RangeFinal + espaco;
            ponto.PontoMedio = RangeFinal;
            pontos.Add(ponto);

            return pontos;
        }





    }
}
