using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class TESTE: Form
    {
        public TESTE()
        {
            InitializeComponent();
        }

        private void TESTE_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

            int qntMSVariavel = 10;

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
