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
        public Deck()
        {

        }
        public string About { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public int CardCount { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public int CardsPerPlayer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TopDiscard { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        /*Method Name: NewPlayer
        *Purpose: Creates a new player object, immediately populates it with CardsPerPlayer cards 
        *         and returns an IPlayer interface reference to the player object
        *Accepts: nothing
        *Returns: populated player object
        */
        public IPlayer NewPlayer()
        {
            throw new NotImplementedException();
        }
        public string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
