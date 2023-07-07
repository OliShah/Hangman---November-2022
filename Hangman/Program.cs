namespace Hangman
{
    internal class Program
    {
        static string[] wordList = { "hello", "hola", "bonjour", "hej", "oi", "konnichiwa", "namaste", "salam", "ciao", "salve" };

        static int wordListInt;
        static string chosenWord;
        static string usedLetters;

        static char[] hiddenWord;
        private static char guessedLetter;

        static string displayWord = "";
        static int remainingAttempts = 6;

        public static void Main(string[] args)

        {
            RandomWordGenerator();

            Print();

            GameLoop();

            ExitStrategy(remainingAttempts, chosenWord, displayWord);
        }

        private static void RandomWordGenerator()
        {
            Random rand = new Random();
            wordListInt = rand.Next(0, wordList.Length);
            chosenWord = wordList[wordListInt];

            hiddenWord = chosenWord.ToCharArray();

            for (int i = 0; i < hiddenWord.Length; i++)
            {
                hiddenWord[i] = '_';
            }
        }
        static void Print()
        {

            displayWord = new string(hiddenWord);
            Console.WriteLine(string.Join(' ', hiddenWord));
            Console.WriteLine(" ");

            PrintAttempts();
            Console.WriteLine("Used Letters: " + usedLetters);
            Console.WriteLine("Take a guess: ");

        }
        static void PrintAttempts()
        {
            Console.WriteLine("You have: " + remainingAttempts + " attempts remaining.");
        }

        private static void GameLoop()
        {
            do
            {
                checkinput();
                Console.Clear();
                usedLetters += guessedLetter + " ";


                bool found = false;
                for (int i = 0; i < chosenWord.Length; i++)

                    if (chosenWord[i] == guessedLetter)
                    {
                        hiddenWord[i] = guessedLetter;
                        found = true;
                    }

                if (!found)

                {
                    remainingAttempts--;
                }
                Print();

            } while (remainingAttempts > 0 && displayWord != chosenWord);
        }

        static void checkinput()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            guessedLetter = char.ToLower(input.KeyChar);

            if (char.IsLetter(guessedLetter))
            {
                Console.Write(guessedLetter);
                confirmsinput();
                return;
            }
            GameLoop();
        }

        static void confirmsinput()
        {
            ConsoleKeyInfo input = Console.ReadKey();

            if (input.Key == ConsoleKey.Enter)
            {
                return;
            }

            if (input.Key == ConsoleKey.Backspace)
            {

                Console.Clear();
                Print();
                checkinput();
                return;
            }
            confirmsinput();
        }
        private static void ExitStrategy(int remainingAttempts, string chosenWord, string displayWord)
        {
            if (remainingAttempts == 0)
            {
                Console.WriteLine("you have run out of guesses. You lose! The word is: " + chosenWord);
            }

            if (displayWord == chosenWord)
            {
                Console.WriteLine("you win!");
            }

            Console.ReadKey();
        }
    }
}