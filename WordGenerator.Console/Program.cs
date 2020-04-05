using System;

namespace WordGenerator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WordGame();
            //GuessGame();
        }

        static void GuessGame()
        {
            Console.WriteLine("Hello! We're going to guess your word.");
            Console.WriteLine("What is your word? say \"Stop\" to stop.");
            
            var word = Console.ReadLine();

            if (word.Length == 0)
            {
                Console.WriteLine("You have to give us a word!");
            }
            else
            {
                int length = word.Length;
                bool isCapitalized = word[0] == char.ToUpper(word[0]);
                var guess = "";
                long guessCount = 0;

                while (guess.ToLower() != word.ToLower() && guessCount <= 1000000)
                {
                    guess = isCapitalized ? WordGenerator.GetCapitalizedWord(length) : WordGenerator.GetLowerCaseWord(length);
                    guessCount++;
                }

                if (guess.ToLower() != word.ToLower())
                {
                    Console.WriteLine($"Awww darn! We couldn't guess your word after {guessCount} tries. We came up with {guess}");
                }
                else
                {
                    Console.WriteLine($"It took us {guessCount} tries to guess your word: {guess}");
                }
            }

            GuessGame();
        }

        static void WordGame()
        {
            Console.WriteLine("Hello! We're going to make a word for you.");
            Console.WriteLine("How long should your word be? say \"Stop\" to stop.");
            var response = Console.ReadLine();

            if (response.ToLowerInvariant() == "stop") return;
            else if (!int.TryParse(response, out int wordLength))
            {
                Console.WriteLine("You must give us an integer length!");
            }
            else
            {
                Console.WriteLine($"Here's the word we made for you: {WordGenerator.GetCapitalizedWord(wordLength)}");
            }

            WordGame();
        }
    }
}
