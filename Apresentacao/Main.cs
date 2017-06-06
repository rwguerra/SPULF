using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ObjetoTransferencia;
using Negocios;
using System.IO;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Extensions;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;

namespace Apresentacao
{
    public partial class Main : Form
    {
        Login logado;
        List<Item> ItensAtualizados;

        public Main()
        {
            
            InitializeComponent();
            var myModel = new PlotModel { Title = "Exemplo" };
            myModel.Series.Add(new LineSeries());
                   
            this.plot.Model = myModel;
        }

        private void zeraGraficoIntervalo()
        {
            PlotModel myModell = new PlotModel();

            myModell.Title = "";
            var linearAxis1 = new LinearAxis();
            myModell.Axes.Add(linearAxis1);
            var linearAxis2 = new LinearAxis();
            linearAxis2.Position = AxisPosition.Bottom;
            myModell.Axes.Add(linearAxis2);


            //criação do modelo e configuração do gráfico
            myModell.PlotType = PlotType.XY;
            myModell.Background = OxyColor.FromRgb(255, 255, 255);
            myModell.TextColor = OxyColor.FromRgb(0, 0, 0);
            myModell.IsLegendVisible = true;

            myModell.LegendBorder = OxyColors.Black;
            myModell.LegendPlacement = LegendPlacement.Outside;
            myModell.LegendPosition = LegendPosition.BottomLeft;
            myModell.LegendOrientation = LegendOrientation.Horizontal;


            linearAxis2.Title = "";

            //titulo dos eixos
            linearAxis1.Title = "";

            // Adição do modelo à janela  do gráfico
            plot.Model = myModell;

            //Limpa informações anteriores caso existam, do gráfico
            if (plot.Model.Series != null) plot.Model.Series.Clear();

        }

        private void AtualizaPlot()
        {
            //Limpa informações anteriores caso existam, do gráfico
            if (plot.Model.Series != null) plot.Model.Series.Clear();

            LineSeries variavel = new LineSeries();
            variavel = new LineSeries();
            variavel.StrokeThickness = 3.0;
            variavel.LineStyle = LineStyle.Solid;
            variavel.MarkerType = MarkerType.Circle;
            variavel.MarkerStrokeThickness = 5;
            variavel.MarkerResolution = 4;

            for (int i = 0; i < ItensAtualizados.Count; i++)
            {
                variavel.Points.Add(new DataPoint(i,ItensAtualizados[i].DadosOriginais ));
               
            }

            // Adiciona a série ao gráfico
            plot.Model.Series.Add(variavel);

            plot.Model.InvalidatePlot(true);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // DADOOS

            List<Item> listaDados = new List<Item>();

            for (int i = 0; i < 100; i++)
            {
                Item item = new Item();
                item.Tempo = i;
                item.DadosOriginais = i;

                listaDados.Add(item);
            }

            // CONFIGURAÇÃO VARIAVEL LINGUISTICA

            int qntMSVariavel = Convert.ToInt32(ribbonUpDown2);

            List<VariavelLinguistica> _ListaVariaveis = new List<VariavelLinguistica>();

            VariavelLinguistica V0 = new VariavelLinguistica();
            V0.NomeVariavel = "Variavel0";
            V0.QntMS = qntMSVariavel;
            _ListaVariaveis.Add(V0);

            VariavelLinguistica V1 = new VariavelLinguistica();
            V1.NomeVariavel = "Variavel1";
            V1.QntMS = qntMSVariavel;
            _ListaVariaveis.Add(V1);

            VariavelLinguistica V2 = new VariavelLinguistica();
            V2.NomeVariavel = "Variavel2";
            V2.QntMS = qntMSVariavel;
            _ListaVariaveis.Add(V2);


            //CONFIGURAÇÃO PREVISÃO

            PrevisaoFuzzy previsaoFuzzy = new PrevisaoFuzzy();

            double previsao = previsaoFuzzy.Previsao(listaDados, _ListaVariaveis);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewCadastro viewCadastro = new ViewCadastro();
            viewCadastro.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View_Login view_Login = new View_Login();
            view_Login.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            zeraGraficoIntervalo();
        }

        private void LerCSV()
        {
            List<Item> Itens = new List<Item>();
            
            OpenFileDialog ler = new OpenFileDialog();
            if (ler.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }

            StreamReader rd = new StreamReader(ler.FileName);
            
            string linha = null;
          
            string[] linhaseparada = null;
           
            while ((linha = rd.ReadLine()) != null)
            {
                Item item = new Item();
                
                linhaseparada = linha.Split(';');

                item.Tempo = Convert.ToDouble(linhaseparada[0]);

                item.DadosOriginais = Convert.ToDouble(linhaseparada[1]);

                Itens.Add(item);

            }
            rd.Close();

            ItensAtualizados = Itens;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            LerCSV();
            AtualizaPlot();
        }

        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            View_Login view_Login = new View_Login();
            view_Login.ShowDialog();
        }

        private void ribbonOrbRecentItem1_Click(object sender, EventArgs e)
        {
            View_Login view_Login = new View_Login();
            view_Login.ShowDialog();
        }

        private void ribbonOrbRecentItem2_Click(object sender, EventArgs e)
        {
            ViewCadastro viewCadastro = new ViewCadastro();
            viewCadastro.ShowDialog();
        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            // DADOOS

            List<Item> listaDados = new List<Item>();

            for (int i = 0; i < 100; i++)
            {
                Item item = new Item();
                item.Tempo = i;
                item.DadosOriginais = i;

                listaDados.Add(item);
            }

            // CONFIGURAÇÃO VARIAVEL LINGUISTICA

            int qntMSVariavel = Convert.ToInt32(ribbonUpDown2);

            List<VariavelLinguistica> _ListaVariaveis = new List<VariavelLinguistica>();

            VariavelLinguistica V0 = new VariavelLinguistica();
            V0.NomeVariavel = "Variavel0";
            V0.QntMS = qntMSVariavel;
            _ListaVariaveis.Add(V0);

            VariavelLinguistica V1 = new VariavelLinguistica();
            V1.NomeVariavel = "Variavel1";
            V1.QntMS = qntMSVariavel;
            _ListaVariaveis.Add(V1);

            VariavelLinguistica V2 = new VariavelLinguistica();
            V2.NomeVariavel = "Variavel2";
            V2.QntMS = qntMSVariavel;
            _ListaVariaveis.Add(V2);


            //CONFIGURAÇÃO PREVISÃO

            PrevisaoFuzzy previsaoFuzzy = new PrevisaoFuzzy();

            double previsao = previsaoFuzzy.Previsao(listaDados, _ListaVariaveis);
        }
    }
}
