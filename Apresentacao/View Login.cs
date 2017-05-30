using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            ViewMenu viewMenu = new ViewMenu();
            viewMenu.ShowDialog();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(textBoxLogin.Text == "")
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
