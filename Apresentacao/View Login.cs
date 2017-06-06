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
    public partial class View_Login : Form
    {
        public View_Login()
        {
            InitializeComponent();
        }

        private void View_Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClassLogin classLogin = new ClassLogin();
            List<Login> ListLogin = classLogin.consultarLogins();

            Login logado = new Login();

            foreach (var login in ListLogin)
            {
                if (login.Email == textBoxEmail.Text && login.Senha == textBoxSenha.Text)
                {
                    logado = login;
                    MessageBox.Show("Login Efetuado com Sucesso!!");
                }
    
            }

            if (logado.Email == textBoxEmail.Text && logado.Senha == textBoxSenha.Text)
                this.Close();
            else
                MessageBox.Show("Login Incorreto!");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(textBoxEmail.Text == "")
                MessageBox.Show("Digite um e-mail cadastrado!");

            else
                MessageBox.Show("Verifique sua caixa de e-mail!");


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewCadastro viewCadastro = new ViewCadastro();
            viewCadastro.ShowDialog();
        }
    }
}
