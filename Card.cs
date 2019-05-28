using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLowCardGame
{
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    /**
     * Models a Card 
     * */
    public class Card
    {
        public string DisplayName { get; set; }
        public Suit Suit { get; set; }
        public int Value { get; set; }
    }
}

