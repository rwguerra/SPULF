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

namespace Apresentacao
{
    public partial class Main : Form
    {
        Login logado;

        public Main()
        {
            InitializeComponent();
            var myModel = new PlotModel { Title = "Exemplo" };
            myModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            this.plot.Model = myModel;
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

        }

        private List<Item> LerCSV()
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

            return Itens;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            LerCSV();
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
