using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    /// <summary>
    /// Classe com Métodos para Cálculo de Erros 
    /// </summary>
    public static class ClassErros
    {
        #region Métodos Públicos

        /// <summary>
        /// Calculo do Erro Médio
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista  2 de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double ME(List<double> Lista1, List<double> Lista2)
        {
            double ME = 0;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    ME = ME + (Lista1[i] - Lista2[i]);
                }
                return (ME / Lista1.Count) * 100;
            }

            else
                return 101;
        }


        /// <summary>
        /// Calculo do Erro Absoluto Médio
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista 2  de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double MAE(List<double> Lista1, List<double> Lista2)
        {
            double MAE = 0;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    MAE = MAE + Math.Abs(Lista1[i] - Lista2[i]);
                }
                return (MAE / Lista1.Count) * 100;
            }

            else
                return 101;
        }


        /// <summary>
        /// Calculo do Erro Quadrático Médio
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista 2  de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double MSE(List<double> Lista1, List<double> Lista2)
        {
            double MSE = 0;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    MSE = MSE + Math.Pow((Lista1[i] - Lista2[i]), 2);
                }
                return (MSE / Lista1.Count) * 100;
            }

            else
                return 101;
        }


        /// <summary>
        /// Calculo da Raiz do Erro Quadrático Médio
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista 2  de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double RMSE(List<double> Lista1, List<double> Lista2)
        {
            double RMSE = 0;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    RMSE = RMSE + Math.Pow((Lista1[i] - Lista2[i]), 2);
                }
                return (Math.Sqrt(RMSE / Lista1.Count)) * 100;
            }

            else
                return 101;
        }


        /// <summary>
        /// Calculo do Erro Percentual Médio
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista 2  de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double MPE(List<double> Lista1, List<double> Lista2)
        {
            double MPE = 0;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    MPE = MPE + ((Lista1[i] - Lista2[i]) / Lista1[i]);
                }
                return (MPE / Lista1.Count) * 100;
            }

            else
                return 101;
        }




        /// <summary>
        /// Calculo da Média dos erros percentuais absolutos 
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista 2  de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double MAPE(List<double> Lista1, List<double> Lista2)
        {
            double MAPE = 0;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    MAPE = MAPE + Math.Abs((Lista1[i] - Lista2[i]) / Lista1[i]);
                }
                return (MAPE / Lista1.Count) * 100;
            }

            else
                return 101;
        }


        /// <summary>
        /// Calculo da Raiz da Média Geométrica do Erro Quadrático
        /// </summary>
        /// <param name="Lista 1  de parametros para calculo do erro - double"></param>
        /// <param name="Lista 2  de parametros para calculo do erro - double"></param>
        /// <returns></returns>
        public static double GRMSE(List<double> Lista1, List<double> Lista2)
        {
            double GRMSE = 1;

            if (Lista1.Count == Lista2.Count)
            {
                for (int i = 0; i < Lista1.Count; i++)
                {
                    GRMSE = GRMSE * Math.Pow((Lista1[i] - Lista2[i]), 2);
                }
                return (Math.Pow(GRMSE, (1 / (2 * Lista1.Count)))) * 100;
            }

            else
                return 101;
        }

        #endregion
    }
}

