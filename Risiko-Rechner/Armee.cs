using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{ 
    public class Armee 
    {
        public Armee()
        {
            this.Units = new List<Unit>();
            this.NumberOfUnit = new List<int>();
        }
        public List<Unit> Units { get; set; }
        public List<int> NumberOfUnit { get; set; }
    }
}
