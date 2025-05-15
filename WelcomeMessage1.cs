using System;
using System.Media;
using System.IO;

namespace voice_greeting
{
    public class WelcomeMessage
    {
        // Constructor
        public WelcomeMessage()
        {
            // Display ASCII Art Header
            DisplayTitleScreen();

            // Play greeting audio
            PlayGreetingAudio();
        }

        // Function to display ASCII Art
        private void DisplayTitleScreen()
        {
            Console.Clear(); // Clears the console for a fresh display
            Console.ForegroundColor = ConsoleColor.Cyan; // Set text color



            Console.WriteLine(@"

 ____  ____  ____     ____  ____  _____ 
/*   _\/ ___\/  _ \   /  __\/  _ \/__ __\
|  /  |    \| / \|   | | //| / \|  / \  
|  \__\___ || |-||   | |_\\| \_/|  | |  
\____/\____/\_/ \|___\____/\____/  \_/  
                 \____\                 

------------------------------------------
  CYBERSECURITY AWARENESS BOT
  Stay Secure, Stay Aware!
------------------------------------------
");

            Console.ResetColor(); // Reset color to default
        }

        // Function to play greeting sound
        private void PlayGreetingAudio()
        {
            string full_location = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = full_location.Replace("bin\\Debug\\", "");

            try
            {
                string full_Path = Path.Combine(new_path, "greeting.wav");

                using (SoundPlayer play = new SoundPlayer(full_Path))
                {
                    play.PlaySync();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error playing sound: " + error.Message);
            }
        }
    }
}
