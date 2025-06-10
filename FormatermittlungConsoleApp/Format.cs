using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatermittlungConsoleApp
{
    class Format

    {
        public Format(double diffSquareMM, JsonData item)
        {
            Difference = diffSquareMM;
            
            JsonDataItem = item;

        }
        public double Difference { get; set; }
        public JsonData JsonDataItem { get; set; }

        public string FormatName { get; set; }


    }
}
