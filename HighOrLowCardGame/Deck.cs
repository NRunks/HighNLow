using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLowCardGame
{
    /**
     * Represents a deck of cards
     * */
    public class Deck
    {
        protected Queue<Card> cards; // Queue to store card objects

        public Deck()
        {
            cards = new Queue<Card>();
            for (int i = 2; i <= 14; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Enqueue(new Card()
                    {
                        Suit = suit,
                        Value = i,
                        DisplayName = GetShortName(i, suit)
                    });
                }
            }
            Shuffle(ref cards);
        }

        /**
         * "Draws" a card from the deck and returns the Card object
         * */
        public Card Draw()
        {
            if (cards.Count > 0)
            {
                return cards.Dequeue();
            }
            else return null;
        }

        /**
         * Returns true if the card queue is empty, false otherwise
         * */
        public bool Empty()
        {
            return cards.Count == 0;
        }

        /**
         * Returns the shortened name of the card
         * */
        private static string GetShortName(int value, Suit suit)
        {
            string valueDisplay = "";
            if (value >= 2 && value <= 10)
            {
                valueDisplay = value.ToString();
            }
            else if (value == 11)
            {
                valueDisplay = "J";
            }
            else if (value == 12)
            {
                valueDisplay = "Q";
            }
            else if (value == 13)
            {
                valueDisplay = "K";
            }
            else if (value == 14)
            {
                valueDisplay = "A";
            }

            return valueDisplay + Enum.GetName(typeof(Suit), suit)[0];
        }

        /**
         * Shuffles the card queue
         * */
        private void Shuffle(ref Queue<Card> cards)
        {
            //Shuffle the existing cards using Fisher-Yates Modern
            List<Card> transformedCards = cards.ToList();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int n = transformedCards.Count - 1; n > 0; --n)
            {
                //Randomly pick a card which has not been shuffled
                int k = r.Next(n + 1);

                //Swap the selected item with the last "unselected" card in the collection
                Card temp = transformedCards[n];
                transformedCards[n] = transformedCards[k];
                transformedCards[k] = temp;
            }

            cards = new Queue<Card>();
            foreach (var card in transformedCards)
            {
                cards.Enqueue(card);
            }
        }
    }
}
