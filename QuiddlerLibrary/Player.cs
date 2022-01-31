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
        Application microsoftWordObject = new Application();

        public Player(Deck d)
        {
            _deck = d;
        }

        ~Player()
        {
            microsoftWordObject.Quit();
        }

        public int CardCount { get => _playerCards.Count(); }
        public int TotalPoints { get => _totalPoints; }

        public bool Discard(string card)
        {
            return _deck.PushToDiscardStack(card);
        }
        // th e r e
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

            RemovePlayersCards(candidate);

            int pointsEarned = AddPointsToPlayer(candidate);
            return pointsEarned;
        }

        public int TestWord(string candidate)
        {
            List<string> candidateLetters = candidate.Split(' ').ToList();

            // Condition 1
            bool playerHasMoreThanEnoughCards = candidateLetters.Count < _playerCards.Count;

            // Condition 
            bool playerHasAllCandidateLetters = PlayersHasAllCandidateLetters(_playerCards, candidateLetters);

            // Condition 3
            string strConcatenated = candidate.Replace(" ", String.Empty);
            bool isAWord = microsoftWordObject.CheckSpelling(strConcatenated);


            if (playerHasMoreThanEnoughCards && playerHasAllCandidateLetters && isAWord)
                return 1;

            return 0;
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (Card element in _playerCards)
            {
                strBuilder.Append($"{element} ");
            }
            strBuilder.Length--;

            return strBuilder.ToString();
        }

        private bool PlayersHasAllCandidateLetters(List<Card> playerCards, List<string> candidateStrings)
        {
            foreach (string candidateString in candidateStrings)
            {
                bool cardExistsInPlayerDeck = false;
                for (int i = 0; i < playerCards.Count; i++)
                {
                    if (playerCards[i].CardLetter == candidateString)
                    {
                        cardExistsInPlayerDeck = true;
                        playerCards.RemoveAt(i);
                        break;
                    }
                }

                if (!cardExistsInPlayerDeck)
                    return false;
            }
            return true;

        }

        private void RemovePlayersCards(string candidateString)
        {
            List<string> candidatesStringList = candidateString.Split(' ').ToList();

            foreach (string candidateElement in candidatesStringList)
            {
                for (int i = 0; i < _playerCards.Count; i++)
                {
                    if (_playerCards[i].CardLetter == candidateElement)
                    {
                        _playerCards.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private int AddPointsToPlayer(string candidateString)
        {
            List<string> candidatesStringList = candidateString.Split(' ').ToList();

            int pointsEarned = 0;
            foreach (string candidateElement in candidatesStringList)
            {
                pointsEarned += _deck.GetValueOfLetter(candidateString);
                _totalPoints += pointsEarned;
            }

            return pointsEarned;
        }
    }
}
