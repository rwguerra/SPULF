using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class VariavelLinguistica
    {
        public string NomeVariavel { get; set; }
        public double RangeInicial { get; set; }
        public double RangeFinal { get; set; }
        public int QntMS { get; set; }
        public List<Ponto> FuzzySet { get; set; }
    }

}
