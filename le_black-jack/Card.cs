using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace le_black_jack
{
    class Card
    {
        private int cardNb;
        private string value;
        private string color;

        public Card(string theValue, string theColor, int theCardNb)
        {
            value = theValue;
            color = theColor;
            cardNb = theCardNb;
        }

        public void printInfo()
        {
            Console.WriteLine($"{value} de {color}");
        }

        public string getValue()
        {
            return value;
        }

        public string getColor()
        {
            return color;
        }
    }
}
