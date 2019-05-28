using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLowCardGame
{
    /**
     * Used as a controller class that represents a game 
     * */
    class Game
    {
        private Player player1;
        private Deck deck;
        private Random rand;
        public int Balance { get; set; }
        public int Wager { get; set; }
        public string Player1Name
        {
            get
            {
                return player1.Name;
            }
        }
        public enum Chip
        {
            One,
            Five,
            TwentyFive,
            OneHundred,
            FiveHundred
        }


        public enum CardCover
        {
            Blue = 0,
            Red = 1,
            Green = 2,
            Gray = 3,
            Yellow = 4,
            Purple = 5

        }
        public enum Guess
        {
            Low,
            High
        }

        /**
         * Class that models the result of a player's guess
         * */
        public class GuessResult
        {
            public Card flippedCard;
            public static int cardNumber = 0;
            public bool isGuessCorrect;
            public bool isDeckEmpty;
            public bool isBalanceZero;

            public GuessResult(Card flippedCard, bool isGuessCorrect, bool isDeckEmpty, bool isBalanceZero)
            {
                this.flippedCard = flippedCard;
                this.isGuessCorrect = isGuessCorrect;
                ++cardNumber;
                this.isDeckEmpty = isDeckEmpty;
                this.isBalanceZero = isBalanceZero;
            }
            public GuessResult(bool isDeckEmpty, bool isBalanceZero)
            {
                this.isDeckEmpty = isDeckEmpty;
                this.isBalanceZero = isBalanceZero;
            }
        }

        public int highCount;
        public int lowCount;
        public int guessCount;

        public Game(String playerName, int balance)
        {
            player1 = new Player();
            player1.Name = playerName;
            this.Balance = balance;
            deck = new Deck();
            highCount = lowCount = guessCount = Wager = 0;
        }

        /**
         * Returns true if game has a wager
         * */
        public bool HasWager()
        {
            return Wager != 0;
        }

        /**
         * Returns true if the amount being wagered in the game is less than or equal to the 
         * amount in the game's balance, false otherwise
         * */
        public bool CanAffordToWager(int wagerAmount)
        {
            return wagerAmount <= Balance;
        }

        /**
         * Returns true if the deck is empty or the player's balance has reached 0
         * */
        public bool IsEndOfGame()
        {
            if (deck.Empty())
            {
                Console.WriteLine(player1.Name + " is out of cards!");
                return true;
            }
            else if (this.Balance == 0)
            {
                Console.WriteLine(player1.Name + " is out of money!");
                return true;
            }
            return false;
        }

        /**
         * Plays a turn and returns the result of the player's guess
         * */
        public GuessResult PlayTurn(Guess guess)
        {
            if (HasWager())
            {
                Card flippedCard = deck.Draw();
                if (flippedCard != null)
                {
                    bool isGuessCorrect = false;
                    if (guess.Equals(Guess.High))
                    {
                        isGuessCorrect = (flippedCard.Value >= 7 || flippedCard.Value == 14);
                    }
                    else if (guess.Equals(Guess.Low))
                    {
                        isGuessCorrect = (flippedCard.Value <= 7 || flippedCard.Value == 14);
                    }
                    if (isGuessCorrect)
                    {
                        this.Balance += this.Wager;
                    }
                    else
                    {
                        this.Balance = ((this.Balance - this.Wager) >= 0) ? (this.Balance - this.Wager) : 0;
                    }
                    return new GuessResult(flippedCard, isGuessCorrect, deck.Empty(), (this.Balance == 0));
                }
                else
                    return new GuessResult(true, (this.Balance == 0));
            }
            return null;
        }

        /**
         * Used by the view controller to randomly change the color of the card covers between flips
         * */
        public CardCover getCardCover()
        {
            rand = new Random();
            int value = rand.Next(0, 6);
            switch (value)
            {
                case (int)CardCover.Blue:
                    return CardCover.Blue;
                case (int)CardCover.Red:
                    return CardCover.Red;
                case (int)CardCover.Green:
                    return CardCover.Green;
                case (int)CardCover.Gray:
                    return CardCover.Gray;
                case (int)CardCover.Yellow:
                    return CardCover.Yellow;
                default:
                    return CardCover.Purple;
            }      
        }
    }
}
