using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    public class Player : IPlayer
    {
        private List<Card> _playerCards = new List<Card>();
        private int _totalPoints = 0;
        private Deck _deck;

        public Player(Deck d)
        {
            _deck = d;
        }
        public int CardCount { get => _playerCards.Count(); }
        public int TotalPoints { get => _totalPoints; }

        public bool Discard(string card)
        {
            return _deck.PushToDiscardStack(card);
        }

        public string DrawCard()
        {
            try
            {
                Card drawnCard = _deck.DrawCardFromUndealtCards();
                _playerCards.Add(drawnCard);
                return drawnCard.CardLetter;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            
        }

        public string PickupTopDiscard()
        {
            Card card = _deck.DrawCardFromDiscardCards();
            _playerCards.Add(card);
            return card.CardLetter;
        }

        public int PlayWord(string candidate)
        {
            int resultOfTestWord = TestWord(candidate);

            if (resultOfTestWord == 0)
                return 0;

            // th e r e
        }

        public int TestWord(string candidate)
        {
            string strConcatenated = candidate.Replace(" ", String.Empty);
            Application microsoftWordObject = new Application();


            bool isAWord = microsoftWordObject.CheckSpelling(strConcatenated);

        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach(Card element in _playerCards)
            {
                strBuilder.Append($"{element} ");
            }
            strBuilder.Length--;

            return strBuilder.ToString();
        }
    }
}
