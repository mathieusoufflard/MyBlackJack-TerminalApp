using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace le_black_jack
{
    class Deck
    {
        private string color;
        public static List<Card> cards = new List<Card>();
        public Deck()
        {
            createDeck();
        }

        static public List<Card> getList()
        {
            return cards;
        }
        private void createDeck()
        {
            Console.WriteLine("création du sabot");
            for (int i = 0; i < 6; i++)
            {
                for (int i1 = 0; i1 <= 3; i1++)
                {
                    switch (i1)
                    {
                        case 0:
                            color = "Pique";
                            break;
                        case 1:
                            color = "Trèfle";
                            break;
                        case 2:
                            color = "Carreau";
                            break;
                        case 3:
                            color = "Coeur";
                            break;
                        default:
                            break;
                    }
                    for (int i2 = 1; i2 <= 13; i2++)
                    {
                        switch (i2)
                        {
                            case 1:
                                cards.Add(new Card("As", color, i2));
                                break;
                            case 11:
                                cards.Add(new Card("Valet", color, i2));
                                break;
                            case 12:
                                cards.Add(new Card("Dame", color, i2));
                                break;
                            case 13:
                                cards.Add(new Card("Roi", color, i2));
                                break;
                            default:
                                cards.Add(new Card(i2.ToString(), color, i2));
                                break;
                        }
                    }
                }
            }
            Console.WriteLine($"le sabot contient {cards.Count()} cartes");
        }
        public static Card pickedOne()
        {
            Random rand = new Random();
            int nb = rand.Next(0, cards.Count());
            Card picked = new Card(cards[nb].getValue(), cards[nb].getColor(), 0);
            cards.Remove(cards[nb]);
            return picked;
        }
    }
}
