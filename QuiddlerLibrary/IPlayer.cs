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
        public int CardCount { get; init; }
        public int TotalPoints { get; init; }
        public string DrawCard();
        public bool Discard(string card);
        public string PickupTopDiscard();
        public int PlayWord(string candiadate);
        public int TestWord(string candidate);
        public string ToString();
    }
}
