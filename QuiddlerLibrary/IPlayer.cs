/**
	 * Class Name:		IPlayer.cs
	 * Purpose:			Interface for the player class
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
    public interface IPlayer
    {
        public int CardCount { get; }
        public int TotalPoints { get; }

        /*Method Name: DrawCard
        *Purpose: If the deck is empty it throws an InvalidOperationException, 
        *         otherwise it takes the top card from the deck, adds it to the player’s hand 
        *         and returns the card’s letter(s) as a string.
        *Accepts: nothing
        *Returns: string of the card
        */
        public string DrawCard();

        /*Method Name: Discard
        *Purpose: Accepts a string representing a card and verifies that the card is in the player’s hand. 
        *         If it’s not in the hand it returns false, 
        *         otherwise it removes it from the hand making that card the top card on the deck’s discard pile and returns true.
        *Accepts: the card that is to be discarded as a string
        *Returns: is the card is discarded as a bool
        */
        public bool Discard(string card);

        /*Method Name: PickupTopDiscard
        *Purpose: Takes the top card in the discard pile and adds it to the player’s hand. 
        *         It also returns the value of the card as a string. 
        *Accepts: nothing
        *Returns: the a card as string 
        */
        public string PickupTopDiscard();

        /*Method Name: PlayWord
        *Purpose: Accepts a string representing a word and then calls TestWord. 
        *         If the candidate word is worth more than 0 points then all the cards in the word will be removed from the player’s hand 
        *         and the points will be added to the player’s TotalPoints property. The points for the word are returned by the method.
        *Accepts: the word as a string
        *Returns: the points as a int 
        */
        public int PlayWord(string candidate);

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
        public int TestWord(string candidate);
        public string ToString();
    }
}
