using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace le_black_jack
{
    class Playeur
    {
        private string name;
        private int point = 0;
        private int bet;
        private int totalMoney;
        bool jump = false;
        private List<Card> cards = new List<Card>();

        public Playeur(string theName, int money)
        {
            name = theName;
            totalMoney = money;            
        }

        public List<Card> getListCards()
        {
            return cards;
        }
        public int getPts()
        {
            return point;
        }

        public int getBet()
        {
            return bet;
        }

        public int getMoney()
        {
            return totalMoney;
        }

        public string getName()
        {
            return name;
        }

        public void setBet(int theBet)
        {
            bet = theBet;
        }

        public void setMoney(int theMoney)
        {
            totalMoney = theMoney;
            if (theMoney <= 0)
                totalMoney = 0;
        }

        public void setPts(int pts)
        {
            point = pts;
        }

        public void setJump(bool value)
        {
            
          jump = value;
            
        }

        public void take()
        {
            cards.Add(Deck.pickedOne());
            if (cards[cards.Count() - 1].getValue() == "Valet" ||
                cards[cards.Count() - 1].getValue() == "Dame" ||
                cards[cards.Count() - 1].getValue() == "Roi")
                point += 10;
            else if (cards[cards.Count() - 1].getValue() == "As")
            {
                if (point + 11 > 21 )
                {
                    point += 1;
                }
                else
                    point += 11;
            }
            else
                point += int.Parse(cards[cards.Count() - 1].getValue());
        }
    }
}
