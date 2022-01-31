using System;
using System.Collections.Generic;
using QuiddlerLibrary;

namespace QuiddlerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IDeck deck = DeckCreator.Create();
            Console.WriteLine(deck.About);
           
            Console.WriteLine($"\nDeck initialized with the following {deck.CardCount} cards...");
            Console.WriteLine($"{deck.ToString()}\n");
           
            int numberOfPlayers = GetNumberOfPlayers();
            int numberOfCardsPerPlayer = GetNumberOfCards();
            deck.CardsPerPlayer = numberOfCardsPerPlayer;

            List<IPlayer> players = new List<IPlayer>();

            for (int i = 0; i < numberOfPlayers; i++)
                players.Add(deck.NewPlayer());

            Console.WriteLine($"\nCards were dealt to {numberOfPlayers} players(s).");
            Console.WriteLine($"The top card which was '{deck.TopDiscard}' was moved to the discard pile.");

            bool someoneHasWon = false;
            do
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Console.WriteLine("\n‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐");
                    Console.WriteLine($"Player {i + 1} ({players[i].TotalPoints} points)");
                    Console.WriteLine("‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐\n");

                    Console.WriteLine($"The deck now contains the following {deck.CardCount} cards...");
                    Console.WriteLine($"{deck.ToString()}\n");

                    Console.WriteLine($"Your cards are [{players[i].ToString()}].");
                    if (GetYesOrNoWithQuestion($"Do you want the top card in the discard pile which is '{deck.TopDiscard}'? (y/n): "))
                    {
                        players[i].PickupTopDiscard();
                    }
                    else
                    {
                        Console.WriteLine($"The dealer dealt '{players[i].DrawCard()}' to you from the deck.");
                        Console.WriteLine($"The deck contains {deck.CardCount} cards.");
                    }
                    Console.WriteLine($"Your cards are [{players[i].ToString()}].");

                    while (true)
                    {
                        if (!GetYesOrNoWithQuestion($"Test a word for its points value? (y/n): "))
                            break;

                        Console.Write($"Enter a word using [{players[i].ToString()}] leaving space between cards: ");
                        string wordEntered = Console.ReadLine();

                        Console.WriteLine($"the word {wordEntered} is worth {players[i].TestWord(wordEntered)} points.");
                        if (players[i].TestWord(wordEntered) > 0)
                        {
                            if (GetYesOrNoWithQuestion($"Do you want to play the word [{wordEntered}]? (y/n): "))
                            {
                                players[i].PlayWord(wordEntered);
                                Console.WriteLine($"Your cards are [{players[i].ToString()}] and you have {players[i].TotalPoints} points.");
                                break;
                            }
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter a card from your hand to drop on the discard pile: ");
                        string stringEntered = Console.ReadLine();
                        if (!players[i].Discard(stringEntered))
                        {
                            Console.WriteLine("Please enter an available card in your deck");
                            continue;
                        }
                        break;
                    }

                    if(players[i].CardCount == 0)
                    {
                        someoneHasWon = true;
                        break;
                    }

                    Console.WriteLine($"Your cards are [{players[i].ToString()}]");
                }

                if (!someoneHasWon)
                {
                    if(!GetYesOrNoWithQuestion($"\nWould you like each player to take another turn? (y/n): "))
                    {
                        break;
                    }
                }

            } while (!someoneHasWon);

            Console.WriteLine("\nRetiring the game.");

            Console.WriteLine("The final scores are...");
            Console.WriteLine("‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"Player {i + 1}: {players[i].TotalPoints} points");
            }
        }


        private static int GetNumberOfPlayers()
        {
            
            int numberOfPlayers = 0;
            while (true)
            {
                Console.Write("How many players are there? (1‐8): ");
                
                if (Int32.TryParse(Console.ReadLine(), out numberOfPlayers) && numberOfPlayers > 0 && numberOfPlayers < 9)
                    break;
                
                Console.WriteLine("Please enter a number between 1-8");
            }
            return numberOfPlayers;
        }

        private static int GetNumberOfCards()
        {
            int numberOfCards = 0;
            while (true)
            {
                Console.Write("How many cards will be dealt to each player? (3‐10): ");

                if (Int32.TryParse(Console.ReadLine(), out numberOfCards) && numberOfCards > 2 && numberOfCards < 11)
                    break;

                Console.WriteLine("Please enter a number between 1-8");
            }
            return numberOfCards;
        }

        private static bool GetYesOrNoWithQuestion(string QuestionString)
        {
            while (true)
            {
                Console.Write(QuestionString);
                char characterEntered = Console.ReadKey().KeyChar;

                if (characterEntered == 'y')
                {
                    Console.WriteLine();
                    return true;
                }

                else if (characterEntered == 'n')
                {
                    Console.WriteLine();
                    return false;
                }

                Console.WriteLine("\nPlease enter either 'y' or 'n'!");
            }

        }
    
    }
}
