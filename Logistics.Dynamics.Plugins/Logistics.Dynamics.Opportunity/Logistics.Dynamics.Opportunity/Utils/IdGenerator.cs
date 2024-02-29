using System;
using System.Text;

namespace Logistics.Dynamics.Opportunity.Utils
{
    public class IdGenerator

    {
        static Random random = new Random();
        static string GenerateAlfaNumericPart()
        {
            StringBuilder alfaNumericBuilder = new StringBuilder();
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            alfaNumericBuilder.Append(caracteres[random.Next(caracteres.Length)]);
            alfaNumericBuilder.Append(random.Next(10));
            alfaNumericBuilder.Append(caracteres[random.Next(caracteres.Length)]);
            alfaNumericBuilder.Append(random.Next(10));
            return alfaNumericBuilder.ToString();
        }

        public string GenerateOpportunityID()
        {
            string prefix = "OPP";
            int numberPart = random.Next(10000, 90000);
            string alfaNumericPart = GenerateAlfaNumericPart();

            StringBuilder idBuilder = new StringBuilder();

            idBuilder.Append(prefix);
            idBuilder.Append('-');
            idBuilder.Append(numberPart);
            idBuilder.Append('-');
            idBuilder.Append(alfaNumericPart);

            return idBuilder.ToString();
        }
    }
}
