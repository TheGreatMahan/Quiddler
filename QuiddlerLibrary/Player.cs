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
        public Player(Deck d)
        {

        }
        public int CardCount { get => throw new NotImplementedException();}
        public int TotalPoints { get => throw new NotImplementedException(); }

        public bool Discard(string card)
        {
            throw new NotImplementedException();
        }

        public string DrawCard()
        {
            throw new NotImplementedException();
        }

        public string PickupTopDiscard()
        {
            throw new NotImplementedException();
        }

        public int PlayWord(string candiadate)
        {
            throw new NotImplementedException();
        }

        public int TestWord(string candidate)
        {
            throw new NotImplementedException();
        }
    }
}
