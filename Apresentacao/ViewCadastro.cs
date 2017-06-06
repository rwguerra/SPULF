using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjetoTransferencia;
using Negocios;

namespace Apresentacao
{
    public partial class ViewCadastro : Form
    {
        public ViewCadastro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidacaoCampos())
            {
                if (textBoxConfirmacaoSenha.Text == textBoxSenha.Text)
                {
                    Login login = CarregarDados();

                    ClassLogin classLogin = new ClassLogin();
                    classLogin.inserirLogin(login);

                    MessageBox.Show("Cadastro Efetuado com Sucesso!");

                    this.Close();
                }
                else
                    MessageBox.Show("Senha difere da senha de confirmação!");
                
            }
               
          
        }

        private bool ValidacaoCampos()
        {
            if (textBoxEmail.Text == "")
                return false;
            else if (textBoxEmail.Text == "")
                return false;
            else if (textBoxUf.Text == "")
                return false;
            else if (textBoxNome.Text == "")
                return false;
            else if (textBoxSenha.Text == "")
                return false;
            else
                return true;

        }

        private Login CarregarDados()
        {
            Login login = new Login();

            login.Email = textBoxEmail.Text ;
            login.InstituicaoOrigem = textBoxUf.Text;
            login.NomeCompleto = textBoxNome.Text;
            login.Senha = textBoxSenha.Text;

            return login;

        } 
    }
}
