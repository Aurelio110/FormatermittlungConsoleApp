using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatermittlungConsoleApp
{
    class Format

    {
        public Format(double diffSquareMM, JsonData item, string ausrichtung)
        {
            Difference = diffSquareMM;
            
            JsonDataItem = item;

            Ausrichtung = ausrichtung;

        }
        public double Difference { get; set; }
        public JsonData JsonDataItem { get; set; }

        public string Ausrichtung { get; set; }


    }
}
