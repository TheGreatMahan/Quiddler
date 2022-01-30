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
        private List<Card> _cardsList = new List<Card>();
        private int _index = 0;
        private int _cardsPerPlaye = 0;
        private Stack<Card> _discardStack = new Stack<Card>();

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
        public string TopDiscard { get => _discardStack.Peek().CardLetter; }

        public IPlayer NewPlayer()
        {
            throw new NotImplementedException();
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
    }
}
