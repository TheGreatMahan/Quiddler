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

        public IPlayer NewPlayer()
        {
            throw new NotImplementedException();
        }
    }
}
