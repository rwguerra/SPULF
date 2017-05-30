using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vExcel = Microsoft.Office.Interop.Excel;

namespace Negocios
{
    public class Excel
    {
        public string CriarExcel(string NomeArquivo, DataTable DtConteudo)
        {
            try
            {
                vExcel.Workbook objBook;
                vExcel.Worksheet objSheet; // Criando objeto Workbook
                vExcel.Application ExcelApp = new vExcel.Application();

                object misValue = System.Reflection.Missing.Value;

                objBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value); // Criando objeto Workbook
                objSheet = (vExcel.Worksheet)objBook.Sheets["Plan1"]; // Criando objeto Workbook
                                                                      //objSheet.Name = nomedaAba; //Adicionando o nome a planilha

                int iLinha = 0;
                foreach (DataRow Dr in DtConteudo.Rows)
                {
                    iLinha = iLinha + 1;
                    int iColuna = 0;
                    foreach (DataColumn Dc in DtConteudo.Columns)
                    {
                        iColuna = iColuna + 1;
                        objSheet.Cells[iLinha, iColuna] = Dr[Dc.ColumnName];
                    }

                }
                //Salvando informações
                objBook.SaveAs(NomeArquivo, vExcel.XlFileFormat.xlWorkbookNormal,
                misValue, misValue, false, misValue, vExcel.XlSaveAsAccessMode.xlNoChange,
                misValue, misValue, misValue, misValue, misValue);
                objBook.Close(true, misValue, misValue);

                //Eliminando o Excel da memória
                objSheet = null;
                objBook = null;
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                ExcelApp = null;
                GC.Collect();

                ExcelApp = null;
            }
            catch (Exception e)
            {
                return "Houve um erro na criação do arquivo.Consulte o administrador do sistema.n" + e.Message;
            }

            return "";
        }

        public string AtualizarPlanilha(string pNomeArquivo, DataTable pDtConteudo, int pLinhaInicial = 0, int pColunaInicial = 0, string DirSalvarComo = "")
        {
            vExcel.Workbook objBook;
            vExcel.Worksheet objSheet;
            vExcel.Application ExcelApp = new vExcel.Application();

            object misValue = System.Reflection.Missing.Value;
            try
            {
                objBook = ExcelApp.Workbooks.Open(pNomeArquivo, misValue, false, misValue,
                misValue, misValue, misValue, misValue,
                misValue, true, misValue, misValue,
                misValue, misValue, misValue);

                objSheet = (vExcel.Worksheet)(objBook.Worksheets[1]);
                int iLinha = 0;
                if (pLinhaInicial != 0)
                    iLinha = pLinhaInicial - 1;
                foreach (DataRow Dr in pDtConteudo.Rows)
                {
                    iLinha = iLinha + 1;

                    int iColuna = 0;
                    if (pColunaInicial != 0)
                        iColuna = pColunaInicial - 1;
                    foreach (DataColumn Dc in pDtConteudo.Columns)
                    {
                        iColuna = iColuna + 1;
                        objSheet.Cells[iLinha, iColuna] = Dr[Dc.ColumnName];
                    }
                }

                if (DirSalvarComo.Equals(""))
                    objBook.Save();
                else
                    objBook.SaveAs(DirSalvarComo, vExcel.XlFileFormat.xlWorkbookNormal, misValue, misValue, false, misValue,
                    vExcel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);

                objSheet = null;
                objBook = null;
                ExcelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                ExcelApp = null;
                GC.Collect();

            }
            catch (Exception e)
            {
                return "Houve um erro na atualização do arquivo.Consulte o administrador do sistema.n" + e.Message;
            }
            return "";
        }

    }
}
