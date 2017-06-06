using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AcessoBD.Properties;
//using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{

    public class AcessoDadosSqlServer
    {
        #region Criar Conexão
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.StringConexao);
        }
        #endregion

        #region Instanciar Parametros
        //Parametros que vão para o banco de dados
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;
        #endregion

        #region Limpar Parametros
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }
        #endregion

        #region Adicionar Parametros
        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }
        #endregion

        #region Execultar Manipilação de Inserir - Alterar - Excluir
        public object ExecultarManupulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {

            try
            {
                //CriarConexao
                SqlConnection sqlConnection = CriarConexao();
                //AbrirConexao
                sqlConnection.Open();
                //Comando que leva informações ao banco de dados
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando(dentro do trafego da conexao)
                sqlCommand.CommandType = commandType; //Procidure ou texto
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql; //nome procidure ou texto
                sqlCommand.CommandTimeout = 7200; // Em segundos - 7200s=2hrs  Tempo de conexão aberta

                //Adicinar parametros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                }

                //Execultar comando, ou seja, mandar o comando ir ate o banco de dados
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultar registro do banco de dados
        public DataTable ExecultarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //CriarConexao
                SqlConnection sqlConnection = CriarConexao();
                //AbrirConexao
                sqlConnection.Open();
                //Comando que leva informações ao banco de dados
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando(dentro do trafego da conexao)
                sqlCommand.CommandType = commandType; //Procidure ou texto
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql; //nome procidure ou texto
                sqlCommand.CommandTimeout = 7200; // Em segundos - 7200s=2hrs  Tempo de conexão aberta

                //Adicinar parametros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Criando o adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //Criando DataTable Vazia
                DataTable dataTable = new DataTable();

                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher a tabela(datatable)
                sqlDataAdapter.Fill(dataTable);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}