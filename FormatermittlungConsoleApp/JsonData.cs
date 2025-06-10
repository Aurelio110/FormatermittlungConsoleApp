using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace FormatermittlungConsoleApp
{
    class JsonData
    {
        public int orderFormatId { get; set; }
        public string format { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public double heightInMM
        {
            get { return height * 0.352778; }
        }
        public double widthInMM
        {
            get { return width * 0.352778; }
        }



    }
}
