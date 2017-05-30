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

        public double Previsao(List<Item> lista, Item ItemAPrever, List<VariavelLinguistica> _ListaVariaveis)
        {
            ListaVariaveis = _ListaVariaveis;

            DefinicaoVariaveisLinguisticas(lista);

            InitFuzzyEngine(lista);

            double previsao = DoInference(ItemAPrever);

            return previsao;

        }

        void InitFuzzyEngine(List<Item> lista)
        {
            LinguisticVariable Pico = new LinguisticVariable(ListaVariaveis[0].NomeVariavel,
                Convert.ToSingle(ListaVariaveis[0].RangeInicial), Convert.ToSingle(ListaVariaveis[0].RangeFinal));

            for (int i = 0; i < ListaVariaveis[0].FuzzySet.Count; i++)
            {
                Pico.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[0].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[0].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[0].FuzzySet[i].PontoFinal))));
            }

            LinguisticVariable Area = new LinguisticVariable(ListaVariaveis[1].NomeVariavel,
               Convert.ToSingle(ListaVariaveis[1].RangeInicial), Convert.ToSingle(ListaVariaveis[1].RangeFinal));

            for (int i = 0; i < ListaVariaveis[1].FuzzySet.Count; i++)
            {
                Area.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[1].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[1].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[1].FuzzySet[i].PontoFinal))));
            }

            LinguisticVariable v1 = new LinguisticVariable(ListaVariaveis[2].NomeVariavel,
              Convert.ToSingle(ListaVariaveis[2].RangeInicial), Convert.ToSingle(ListaVariaveis[2].RangeFinal));

            for (int i = 0; i < ListaVariaveis[2].FuzzySet.Count; i++)
            {
                v1.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[2].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[2].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[2].FuzzySet[i].PontoFinal))));
            }

            LinguisticVariable v2 = new LinguisticVariable(ListaVariaveis[3].NomeVariavel,
           Convert.ToSingle(ListaVariaveis[3].RangeInicial), Convert.ToSingle(ListaVariaveis[3].RangeFinal));

            for (int i = 0; i < ListaVariaveis[3].FuzzySet.Count; i++)
            {
                v2.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[3].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[3].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[3].FuzzySet[i].PontoFinal))));
            }


            LinguisticVariable Corrente = new LinguisticVariable(ListaVariaveis[4].NomeVariavel,
               Convert.ToSingle(ListaVariaveis[4].RangeInicial), Convert.ToSingle(ListaVariaveis[4].RangeFinal));

            for (int i = 0; i < ListaVariaveis[4].FuzzySet.Count; i++)
            {
                Corrente.AddLabel(new FuzzySet(i.ToString(), new TrapezoidalFunction(
                    Convert.ToSingle(ListaVariaveis[4].FuzzySet[i].PontoInicial),
                    Convert.ToSingle(ListaVariaveis[4].FuzzySet[i].PontoMedio),
                    Convert.ToSingle(ListaVariaveis[4].FuzzySet[i].PontoFinal))));
            }

            // The database
            Database fuzzyDB = new Database();
            fuzzyDB.AddVariable(Pico);
            fuzzyDB.AddVariable(Area);
            fuzzyDB.AddVariable(v1);
            fuzzyDB.AddVariable(v2);
            fuzzyDB.AddVariable(Corrente);


            // Creating the inference system
            IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

            ClassRegras classRegras = new ClassRegras();
            List<string> Regras = classRegras.GerarRegras(lista, ListaVariaveis);

            for (int i = 0; i < Regras.Count; i++)
            {
                IS.NewRule("Rule " + i, Regras[i]);
            }

        }

        private double DoInference(Item ItemAPrever)
        {
            // Setting inputs
            IS.SetInput(ListaVariaveis[0].NomeVariavel, Convert.ToSingle(ItemAPrever.Pico));
            IS.SetInput(ListaVariaveis[1].NomeVariavel, Convert.ToSingle(ItemAPrever.Area));
            IS.SetInput(ListaVariaveis[2].NomeVariavel, Convert.ToSingle(ItemAPrever.V1));
            IS.SetInput(ListaVariaveis[3].NomeVariavel, Convert.ToSingle(ItemAPrever.V2));

            // Setting outputs
            try
            {
                double NovaCorrente = IS.Evaluate(ListaVariaveis[4].NomeVariavel);

                return NovaCorrente;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void DefinicaoVariaveisLinguisticas(List<Item> lista)
        {
            List<double> Pico = new List<double>();
            List<double> Area = new List<double>();
            List<double> V1 = new List<double>();
            List<double> V2 = new List<double>();
            List<double> Corrente = new List<double>();

            foreach (var item in lista)
            {
                Pico.Add(item.Pico);
                Area.Add(item.Area);
                V1.Add(item.V1);
                V2.Add(item.V2);
                Corrente.Add(item.Corrente);
            }

            //double espaco = (Altura.Max() - Altura.Min()) / (ListaVariaveis[0].QntMS - 1);

            ListaVariaveis[0].RangeInicial = Pico.Min() - ((Pico.Max() - Pico.Min()) / (ListaVariaveis[0].QntMS - 1));
            ListaVariaveis[0].RangeFinal = Pico.Max() + ((Pico.Max() - Pico.Min()) / (ListaVariaveis[0].QntMS - 1));
            ListaVariaveis[0].FuzzySet = GerarFuzzySet(Pico.Min(),
                Pico.Max(), ListaVariaveis[0].QntMS);


            ListaVariaveis[1].RangeInicial = Area.Min() - ((Area.Max() - Area.Min()) / (ListaVariaveis[1].QntMS - 1));
            ListaVariaveis[1].RangeFinal = Area.Max() + ((Area.Max() - Area.Min()) / (ListaVariaveis[1].QntMS - 1));
            ListaVariaveis[1].FuzzySet = GerarFuzzySet(Area.Min(),
                Area.Max(), ListaVariaveis[1].QntMS);

            ListaVariaveis[2].RangeInicial = V1.Min() - ((V1.Max() - V1.Min()) / (ListaVariaveis[2].QntMS - 1));
            ListaVariaveis[2].RangeFinal = V1.Max() + ((V1.Max() - V1.Min()) / (ListaVariaveis[2].QntMS - 1));
            ListaVariaveis[2].FuzzySet = GerarFuzzySet(V1.Min(),
                V1.Max(), ListaVariaveis[2].QntMS);

            ListaVariaveis[3].RangeInicial = V2.Min() - ((V2.Max() - V2.Min()) / (ListaVariaveis[3].QntMS - 1));
            ListaVariaveis[3].RangeFinal = V2.Max() + ((V2.Max() - V2.Min()) / (ListaVariaveis[3].QntMS - 1));
            ListaVariaveis[3].FuzzySet = GerarFuzzySet(V2.Min(),
                V2.Max(), ListaVariaveis[3].QntMS);

            ListaVariaveis[4].RangeInicial = Corrente.Min() - ((Corrente.Max() - Corrente.Min()) / (ListaVariaveis[4].QntMS - 1));
            ListaVariaveis[4].RangeFinal = Corrente.Max() + ((Corrente.Max() - Corrente.Min()) / (ListaVariaveis[4].QntMS - 1));
            ListaVariaveis[4].FuzzySet = GerarFuzzySet(Corrente.Min(),
                Corrente.Max(), ListaVariaveis[4].QntMS);

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
