/**
	 * Class Name:		IDeck.cs
	 * Purpose:			Interface for the card class
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
    public interface IDeck
    {
        public string About { get; }
        public int CardCount { get;}
        public int CardsPerPlayer { get;}
        public string TopDiscard { get; }

        /*Method Name: NewPlayer
        *Purpose: Creates a new player object, immediately populates it with CardsPerPlayer cards 
        *         and returns an IPlayer interface reference to the player object
        *Accepts: nothing
        *Returns: populated player object
        */
        public IPlayer NewPlayer();
        public string ToString();
    }
}
