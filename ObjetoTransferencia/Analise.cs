using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Analise
    {
        int MSAL = new int();
        int MSAR = new int();
        int MSC = new int();
        double erro = new double();

        public int MSTempo { get; set; }
        public int MSVariavel { get; set; }
        public double MAPE { get; set; }
        public double MAE { get; set; }

    }
}
