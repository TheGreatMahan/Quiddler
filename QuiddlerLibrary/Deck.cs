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
        private string[] _letters = { "a", "e", "i", "o", "l", "s", "t", "u", "y", "d", "m", "n", "r", "f", "g", "p", "h", "er", "in", "b", "c", "k", "qu", "th", "w", "cl", "v", "x", "j", "z", "q"};
        private int[]    _values  = {  2 ,  2 ,  2 ,  2 ,  3 ,  3 ,  3 ,  4 ,  4 ,  5 ,  5 ,  5 ,  5 ,  6 ,  6 ,  6 ,  7 ,  7  ,  7  ,  8 ,  8 ,  8 ,  9  ,  9  ,  10,  10 ,  11,  12,  13,  14,  15};
        private int[]    _counts  = {  10,  12,  8 ,  8 ,  4 ,  4 ,  6 ,  6 ,  4 ,  4 ,  2 ,  6 ,  6 ,  2 ,  4 ,  2 ,  2 ,  2  ,  2  ,  2 ,  2 ,  2 ,  2  ,  2  ,  2 ,   2 ,  2 ,  2 ,  2 ,  2 ,  2 };

        // Private attributes
        private static List<Card> _cardsList = new List<Card>();
        private static int _index = 0;
        private static int _cardsPerPlayer = 0;
        private static Stack<Card> _discardStack = new Stack<Card>();

        public Deck()
        {
            DeckGenerator();
        }
        
        public string About {get => "Test Client for: Quiddler (TM) Library, © 2022 M. Mehdipour, D. Mahyuddin"; }
        public int CardCount { get => _cardsList.Count - _index;}
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

        public IPlayer NewPlayer()
        {
            IPlayer player = new Player(this);
            for (int i = 0; i < _cardsPerPlayer; i++)
                player.DrawCard();

            return player;
        }

        public override string ToString()
        {
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "cl", "er", "in", "qu", "th"};

            StringBuilder str = new StringBuilder();

            for(int i = 0; i < letters.Length; i++)
            {
                str.Append($"{letters[i]}({CountPerLetter(letters[i])})");

                if (letters[i] == "l" || letters[i] == "x" || letters[i] == "th")
                    str.Append("\n");
                else
                    str.Append("\t");
            }
            return str.ToString();
        }

        private void DeckGenerator()
        {
            AddCardsToList();
            Shuffle();
        }

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

        private void Shuffle()
        {
            Random rng = new Random();
            _cardsList = _cardsList.OrderBy(card => rng.Next()).ToList();
        }

        private int CountPerLetter(string letters)
        {
            int counter = 0;
            for (int i = _index; i < _cardsList.Count; i++)
                if (letters == _cardsList[i].CardLetter)
                    counter++;
            return counter;
        }

        internal Card DrawCardFromUndealtCards()
        {
            if (_cardsList.Count > 0)
                return _cardsList[_index++];
            
            throw new ArgumentOutOfRangeException("There is no undealt cards left to draw from!");
        }

        internal Card DrawCardFromDiscardCards()
        {
            if (_discardStack.Count == 0)
            {
                _discardStack.Push(DrawCardFromUndealtCards());
            }

            return _discardStack.Pop();
        }

        internal bool PushToDiscardStack(string cardLetters)
        {
            int value = GetValueOfLetter(cardLetters);
            
            if (value == 0)
                return false;

            Card card = new Card(cardLetters, value);
           
            _discardStack.Push(card);
            return true;
        }

        internal int GetValueOfLetter(string cardLetters)
        {
            return GetValueOfLetterByIndex(FindIndexForLetters(cardLetters));
        }

        private int FindIndexForLetters(string str)
        {
            for (int i = 0; i < _letters.Length; i++)
                if (str == _letters[i])
                    return i;

            return -1;
        }

        private int GetValueOfLetterByIndex(int index)
        {
            if (index == -1)
                return 0;

            return _values[index];
        }
    }
}
