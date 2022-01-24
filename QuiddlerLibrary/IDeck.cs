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
        public IPlayer NewPlayer();
        public string ToString();
    }
}
