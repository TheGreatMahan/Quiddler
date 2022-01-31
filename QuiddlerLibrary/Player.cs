/**
	 * Class Name:		Player.cs
	 * Purpose:			concerete player class for the user to play the game
	 * Coder:			Mahan Mehdipour Dylan Mahyuddin
	 * Date:			2022-1-22
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    internal class Player : IPlayer
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

        /*Method Name: Discard
        *Purpose: Accepts a string representing a card and verifies that the card is in the player’s hand. 
        *         If it’s not in the hand it returns false, 
        *         otherwise it removes it from the hand making that card the top card on the deck’s discard pile and returns true.
        *Accepts: the card that is to be discarded as a string
        *Returns: is the card is discarded as a bool
        */
        public bool Discard(string card)
        {
            for(int i = 0; i < _playerCards.Count; i++)
            {
                if (_playerCards[i].CardLetter == card)
                {
                    _playerCards.RemoveAt(i);
                    return _deck.PushToDiscardStack(card);
                }
            }
            return false;
        }

        /*Method Name: DrawCard
        *Purpose: If the deck is empty it throws an InvalidOperationException, 
        *         otherwise it takes the top card from the deck, adds it to the player’s hand 
        *         and returns the card’s letter(s) as a string.
        *Accepts: nothing
        *Returns: string of the card
        */
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


        /*Method Name: PickupTopDiscard
        *Purpose: Takes the top card in the discard pile and adds it to the player’s hand. 
        *         It also returns the value of the card as a string. 
        *Accepts: nothing
        *Returns: the a card as string 
        */
        public string PickupTopDiscard()
        {
            Card card = _deck.DrawCardFromDiscardCards();
            _playerCards.Add(card);
            return card.CardLetter;
        }

        /*Method Name: PlayWord
        *Purpose: Accepts a string representing a word and then calls TestWord. 
        *         If the candidate word is worth more than 0 points then all the cards in the word will be removed from the player’s hand 
        *         and the points will be added to the player’s TotalPoints property. The points for the word are returned by the method.
        *Accepts: the word as a string
        *Returns: the points as a int 
        */
        public int PlayWord(string candidate)
        {
            int resultOfTestWord = TestWord(candidate);

            if (resultOfTestWord == 0)
                return 0;

            RemovePlayersCards(candidate);

            int pointsEarned = AddPointsToPlayer(candidate);
            return pointsEarned;
        }

        /*Method Name: TestWord
        *Purpose: Accepts a string containing a word using the same space-delimited format used by PlayWord() 
        *         and returns its point value based on three criteria: 
        *         1) the player has not used all their cards to form the word (need to discard)
        *         2) the letters of the candidate string are a subset of the letters in the current rack object using spaces as delimiters, 
        *         3) the candidate provided (after removing the delimiters) is a valid word as tested using the Application object’s CheckSpelling() method. 
        *         If a candidate word fails to meet either criteria the method returns 0. Note that this method does not modify the player’s TotalPoints, nor does it remove the cards from the player’s hand.
        *Accepts: the card that is to be tested as a string
        *Returns: the points the player would gain as an int
        */
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
            {
                int pointCalculated = 0;
                foreach(string letters in candidateLetters)
                {
                    pointCalculated +=_deck.GetValueOfLetter(letters);
                }
                return pointCalculated;
            }

            return 0;
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (Card element in _playerCards)
            {
                strBuilder.Append($"{element.CardLetter} ");
            }

            if (strBuilder[strBuilder.Length - 1] == ' ')
                strBuilder.Length--;

            return strBuilder.ToString();
        }

        private bool PlayersHasAllCandidateLetters(List<Card> playerCards, List<string> candidateStrings)
        {
            List<Card> playerCardsCopy = new List<Card>();
            playerCardsCopy.AddRange(playerCards);

            foreach (string candidateString in candidateStrings)
            {
                bool cardExistsInPlayerDeck = false;
                for (int i = 0; i < playerCardsCopy.Count; i++)
                {
                    if (playerCardsCopy[i].CardLetter == candidateString)
                    {
                        cardExistsInPlayerDeck = true;
                        playerCardsCopy.RemoveAt(i);
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
                pointsEarned += _deck.GetValueOfLetter(candidateElement);
                
            }

            _totalPoints += pointsEarned;
            return pointsEarned;
        }
    }
}
