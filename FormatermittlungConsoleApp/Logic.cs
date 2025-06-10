using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatermittlungConsoleApp
{
    public class Logic
    {
        private double userFormatHeight;
        private double userFormatWidth;

        public void logicMethod()
        {
            try
            {
                Console.Write("Bitte geben Sie die gewünschte Höhe ein: ");
                double userFormatHeight = Convert.ToDouble(Console.ReadLine());

                Console.Write("Bitte geben Sie die gewünschte Breite ein: ");
                double userFormatWidth = Convert.ToDouble(Console.ReadLine());

            }
            catch (System.FormatException)
            {

                Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es nochmal.");
                logicMethod();
            }
            

            var client = new RestClient("http://odin-probe.dev.faip.io/api/OrderFormat");

            var request = new RestRequest();

            JsonData[] data = client.Get<JsonData[]>(request);

            double diffSquareMM;


            List<Format> optimalFormatList = new List<Format>();


            foreach (var item in data)
            {

                if (item.heightInMM - userFormatHeight > 0 && item.widthInMM - userFormatWidth > 0 && item.heightInMM - userFormatWidth > 0 && item.widthInMM - userFormatHeight > 0)
                {
                    diffSquareMM = (item.heightInMM * item.widthInMM) - (userFormatHeight * userFormatWidth);
                    Format format = new Format(diffSquareMM, item);
                    optimalFormatList.Add(format);
                }
                else
                {
                    continue;
                }
            }

            optimalFormatList.Sort((x, y) => x.Difference.CompareTo(y.Difference));

            if (optimalFormatList.Count > 0)
            {
                Console.WriteLine(optimalFormatList[0].JsonDataItem.format);
            }
            else
            {
                Console.WriteLine("No optimal format found.");
            }
        }
    }
}
