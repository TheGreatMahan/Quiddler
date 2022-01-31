namespace QuiddlerLibrary
{
    internal class Card
    {
        public string CardLetter { get; set; }
        public int Value { get; set; }

        public Card(string letter, int value)
        {
            this.CardLetter = letter;
            this.Value = value;
        }
    }
}
