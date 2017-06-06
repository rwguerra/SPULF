using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcessoBancoDados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class ClassLogin
    {
        #region Instanciando Acesso Sql
        //Instanciar = criando um novo objeto se baseando em um modelo
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();
        #endregion


        public bool inserirLogin(Login login)
        {
            try
            {
                    acessoDadosSqlServer.LimparParametros();
                    acessoDadosSqlServer.AdicionarParametros("@NomeCompleto", login.NomeCompleto);
                    acessoDadosSqlServer.AdicionarParametros("@Email", login.Email);
                    acessoDadosSqlServer.AdicionarParametros("@Senha", login.Senha);
                    acessoDadosSqlServer.AdicionarParametros("@InstituicaoOrigem", login.InstituicaoOrigem);


                    acessoDadosSqlServer.ExecultarManupulacao(CommandType.StoredProcedure, "inserirTbLogin").ToString();

                return true;
            }

            catch (Exception)
            {
                return false;
            }

        }

        public List<Login> consultarLogins()
        {
            try
            {
                List<Login> ListLogin = new List<Login>();

                acessoDadosSqlServer.LimparParametros();

                DataTable dataTable = acessoDadosSqlServer.ExecultarConsulta(CommandType.StoredProcedure, "consultarTbLogin");

                foreach (DataRow linha in dataTable.Rows)
                {
                    Login login = new Login();

                    login.Email = Convert.ToString(linha["Email"]);
                    login.ID = Convert.ToInt32(linha["IdLogin"]);
                    login.InstituicaoOrigem = Convert.ToString(linha["InstituicaoOrigem"]);
                    login.NomeCompleto = Convert.ToString(linha["NomeCompleto"]);
                    login.Senha = Convert.ToString(linha["Senha"]);

                    ListLogin.Add(login);
                }

                return ListLogin;
            }
            catch (Exception exception)
            {
                throw new Exception("Não foi possível efetuar a consulta. Detalhes:" + exception.Message);
            }
        }







    }
}
