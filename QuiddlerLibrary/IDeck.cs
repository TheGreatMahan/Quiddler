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
        public string About { get; init; }
        public int CardCount { get; init; }
        public int CardsPerPlayer { get; set; }
        public string TopDiscard { get; init; }

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
