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
            const double maxHeight = 594.078152d;
            const double maxWidth = 443.089168d;
            try
            {
                Console.Write("Bitte geben Sie die gewünschte Höhe in Millimetern ein: ");
                double userFormatHeight = Convert.ToDouble(Console.ReadLine());

                Console.Write("Bitte geben Sie die gewünschte Breite in Millimetern ein: ");
                userFormatWidth = Convert.ToDouble(Console.ReadLine());

                if (userFormatHeight <= 0 || userFormatWidth <= 0)
                {
                    Console.WriteLine("Die Höhe und Breite müssen größer als 0 sein. Bitte versuchen Sie es erneut.");
                    logicMethod();
                    
                }
                else if (userFormatHeight > maxHeight)
                {
                    Console.WriteLine($"Die Höhe darf nicht größer als {maxHeight} sein. Bitte versuchen Sie es erneut.");
                    logicMethod();
                    
                }
                else if (userFormatWidth > maxWidth)
                {
                    Console.WriteLine($"Die Breite darf nicht größer als {maxWidth} sein. Bitte versuchen Sie es erneut.");
                    logicMethod();
                    
                }

                var client = new RestClient("http://odin-probe.dev.faip.io/api/OrderFormat");
                var request = new RestRequest();
                JsonData[] data = client.Get<JsonData[]>(request);

                double userArea = userFormatHeight * userFormatWidth;
                List<Format> optimalFormatList = new List<Format>();

                foreach (var item in data)
                {
                    
                    bool fitsLandscape = item.heightInMM >= userFormatHeight && item.widthInMM >= userFormatWidth;
                    bool fitsPortrait = item.heightInMM >= userFormatWidth && item.widthInMM >= userFormatHeight;

                    if (fitsLandscape)
                    {
                        double diffSquareMM = (item.heightInMM * item.widthInMM) - userArea;
                        if (diffSquareMM >= 0)
                        {
                            Format format = new Format(diffSquareMM, item, "Querformat");
                            optimalFormatList.Add(format);
                        }
                    }else if (fitsPortrait)
                    {
                        double diffSquareMM = (item.heightInMM * item.widthInMM) - userArea;
                        if (diffSquareMM >= 0)
                        {
                            Format format = new Format(diffSquareMM, item, "Hochformat");
                            optimalFormatList.Add(format);
                        }
                    }
                }

                
                optimalFormatList.Sort((x, y) => x.Difference.CompareTo(y.Difference));

                if (optimalFormatList.Count > 0)
                {
                    Console.WriteLine($"Vorgeschlagenes Format: {optimalFormatList[0].JsonDataItem.format} (Verschnitt: {optimalFormatList[0].Difference:F2} mm²)");
                }
                else
                {
                    Console.WriteLine("Kein passendes Format gefunden.");
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es nochmal.");
                logicMethod();
            }
        }
    }
}
