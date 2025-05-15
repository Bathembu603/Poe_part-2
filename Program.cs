using System;
using System.Threading;
using System.Drawing;
using System.IO;
using memory_recall;

namespace voice_greeting
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display ASCII logo and play greeting sound
            new WelcomeMessage() { };
            new Logo() { }; // Convert image to ASCII logo


            // Ask for the user's name
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nBot: Hello! What's your name? \n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("User: ");
            Console.ResetColor();

            string userName = Console.ReadLine().Trim();

            // Ensure the user enters a name
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nBot: Please enter a valid name.\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("User: ");
                Console.ResetColor();

                userName = Console.ReadLine().Trim();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nBot: Thank you, {userName}! How can I help you today? 😊");
            Console.ResetColor();

            // Create chatbot instance
            CyberSecurityBot bot = new CyberSecurityBot();
            // Create memory recall instance
            recall_list memory = new recall_list();

            // Display chatbot instructions
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n💬 Ask me a question about cybersecurity! Type 'exit' to quit.");
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\n{userName}: ");
                Console.ResetColor();

                string userInput = Console.ReadLine().Trim().ToLower();

                if (userInput == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nBot: Goodbye, {userName}! Stay safe online. 🛡️");
                    Console.ResetColor();

                    // Save the farewell message to memory
                    memory.SaveInteraction($"Bot: Goodbye, {userName}! Stay safe online. 🛡️");

                    // Show all the stored memory before exit
                    memory.ShowMemory();

                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                    break;
                }

                // Save user input to memory (ongoing conversation)
                memory.SaveInteraction($"{userName}: {userInput}");

                // Bot responds
                bot.RespondToUser(userInput);

                // Small delay to make the interaction feel natural
                Thread.Sleep(500);
            }
        }
    }
}
