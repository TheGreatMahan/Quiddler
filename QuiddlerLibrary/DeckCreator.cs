using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public class DeckCreator
    {
        public static IDeck Create()
        {
            return new Deck();
        }
    }
}
