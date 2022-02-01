/**
	 * Class Name:		Deck.cs
	 * Purpose:			concerete Deck class to hold cards
	 * Coder:			Mahan Mehdipour Dylan Mahyuddin
	 * Date:			2022-1-22
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public class Deck : IDeck
    {
        // 3 arrays for generating a list
        private string[] _letters = { "a", "e", "i", "o", "l", "s", "t", "u", "y", "d", "m", "n", "r", "f", "g", "p", "h", "er", "in", "b", "c", "k", "qu", "th", "w", "cl", "v", "x", "j", "z", "q" };
        private int[] _values = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 10, 10, 11, 12, 13, 14, 15 };
        private int[] _counts = { 10, 12, 8, 8, 4, 4, 6, 6, 4, 4, 2, 6, 6, 2, 4, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

        // Private attributes
        private static List<Card> _cardsList = new List<Card>();
        private static int _index = 0;
        private static int _cardsPerPlayer = 0;
        private static Stack<Card> _discardStack = new Stack<Card>();

        public Deck()
        {
            DeckGenerator();
        }

        public string About { get => "Test Client for: Quiddler (TM) Library, © 2022 M. Mehdipour, D. Mahyuddin"; }
        public int CardCount { get => _cardsList.Count - _index; }
        public int CardsPerPlayer
        {
            get => _cardsPerPlayer;
            set
            {
                if (value < 3 || value > 10)
                    throw new ArgumentOutOfRangeException("Cards Per Player should be between 3 and 10");

                else
                    _cardsPerPlayer = value;
            }
        }
        public string TopDiscard
        {
            get
            {
                if (_discardStack.Count == 0)
                {
                    _discardStack.Push(DrawCardFromUndealtCards());
                }

                return _discardStack.Peek().CardLetter;
            }
        }

        /*Method Name: NewPlayer
        *Purpose: Creates a new player object, immediately populates it with CardsPerPlayer cards 
        *         and returns an IPlayer interface reference to the player object
        *Accepts: nothing
        *Returns: populated player object
        */
        public IPlayer NewPlayer()
        {
            IPlayer player = new Player(this);
            for (int i = 0; i < _cardsPerPlayer; i++)
                player.DrawCard();

            return player;
        }

        public override string ToString()
        {
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "cl", "er", "in", "qu", "th" };

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < letters.Length; i++)
            {
                str.Append($"{letters[i]}({CountPerLetter(letters[i])})");

                if (letters[i] == "l" || letters[i] == "x" || letters[i] == "th")
                    str.Append("\n");
                else
                    str.Append("\t");
            }
            return str.ToString();
        }

        /*Method Name: DeckGenerator
        *Purpose: generators a deck by calling AddCardsToList() and Shuffle()
        *Accepts: nothing
        *Returns: void
        */
        private void DeckGenerator()
        {
            AddCardsToList();
            Shuffle();
        }

        /*Method Name: AddCardsToList
        *Purpose: Adds cards to the deck which is a list<Card>
        *Accepts: nothing
        *Returns: void
        */
        private void AddCardsToList()
        {
            _cardsList = new List<Card>();
            for (int i = 0; i < _counts.Length; i++)
            {
                int counter = 0;
                while (counter < _counts[i])
                {
                    _cardsList.Add(new Card(_letters[i], _values[i]));
                    counter++;
                }
            }
        }

        /*Method Name: Shuffle
        *Purpose: with a random object it shuffle the deck
        *Accepts: nothing
        *Returns: void
        */
        private void Shuffle()
        {
            Random rng = new Random();
            _cardsList = _cardsList.OrderBy(card => rng.Next()).ToList();
        }

        /*Method Name: CountPerLetter
        *Purpose: counts how many letters in the deck are left
        *Accepts: string which is the letter
        *Returns: the total number of letters as a int
        */
        private int CountPerLetter(string letters)
        {
            int counter = 0;
            for (int i = _index; i < _cardsList.Count; i++)
                if (letters == _cardsList[i].CardLetter)
                    counter++;
            return counter;
        }

        /*Method Name: DrawCardFromUndealtCards
        *Purpose: draws any card that is undealt from the deck
        *Accepts: nothing
        *Returns: a Card that hasn't been drawen
        */
        internal Card DrawCardFromUndealtCards()
        {
            if (_cardsList.Count > 0)
                return _cardsList[_index++];

            throw new ArgumentOutOfRangeException("There is no undealt cards left to draw from!");
        }

        /*Method Name: DrawCardFromDiscardCards
        *Purpose: takes the top card from the discard Stack
        *Accepts: nothing
        *Returns: a Card from the discard stack
        */
        internal Card DrawCardFromDiscardCards()
        {
            if (_discardStack.Count == 0)
            {
                _discardStack.Push(DrawCardFromUndealtCards());
            }

            return _discardStack.Pop();
        }

        /*Method Name: PushToDiscardStack
        *Purpose: removes a card from hand and puts it in the discard Stack
        *Accepts: a string of the card to be put in the Stack
        *Returns: a bool on if the card was put in the Stack
        */
        internal bool PushToDiscardStack(string cardLetters)
        {
            int value = GetValueOfLetter(cardLetters);

            if (value == 0)
                return false;

            Card card = new Card(cardLetters, value);

            _discardStack.Push(card);
            return true;
        }

        /*Method Name: GetValueOfLetter
        *Purpose: gets the value of the letter card by calling GetValueOfLetterByIndex()
        *Accepts: a string of the current card
        *Returns: int as the value of the letter
        */
        internal int GetValueOfLetter(string cardLetters)
        {
            return GetValueOfLetterByIndex(FindIndexForLetters(cardLetters));
        }

        /*Method Name: FindIndexForLetters
        *Purpose: finds the index for the current letter
        *Accepts: a string of the current card
        *Returns: int as the index of the letter
        */
        private int FindIndexForLetters(string str)
        {
            for (int i = 0; i < _letters.Length; i++)
                if (str == _letters[i])
                    return i;

            return -1;
        }

        /*Method Name: GetValueOfLetterByIndex
        *Purpose: by the current index get the value of the letter
        *Accepts: a int of the index of the letter 
        *Returns: int based on the value of the letter
        */
        private int GetValueOfLetterByIndex(int index)
        {
            if (index == -1)
                return 0;

            return _values[index];
        }
    }
}
