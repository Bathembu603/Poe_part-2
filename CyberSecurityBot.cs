using System;
using System.Collections.Generic;
using System.Threading;

namespace voice_greeting
{
    public class CyberSecurityBot
    {
        private Dictionary<string, List<string>> exactResponses;
        private Dictionary<string, Action> keywordHandlers;
        private Random random;
        private string lastTopic = "";
        private string userName = "";
        private string favoriteTopic = "";

        public CyberSecurityBot()
        {
            random = new Random();

            // Direct match responses
            exactResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you?", new List<string> { "I'm just a bot, but I'm here to help you stay secure!" } },
                { "what's your purpose?", new List<string> { "I am a Cybersecurity Awareness Bot! I provide tips to keep you safe online." } },
                { "what can i ask you about?", new List<string> {
                    "You can ask me about:\n🔹 Password Safety\n🔹 Phishing Awareness\n🔹 Safe Browsing\n🔹 Cybersecurity Tips!" } },
                { "tell me about password safety?", new List<string> {
                    "Always use strong passwords with at least 12 characters, including numbers and symbols.",
                    "Use a mix of uppercase, lowercase, numbers, and symbols. Avoid personal info.",
                    "Consider using a password manager to store your passwords securely." } },
                { "what is phishing?", new List<string> {
                    "Phishing is a type of cyber attack where scammers try to trick you into providing personal information.",
                    "⚠️ Watch out! Phishing emails often pretend to be from trusted companies.",
                    "Phishing links can look real — always double-check the sender and the URL!" } },
                { "how can i browse safely?", new List<string> {
                    "Keep your browser updated and avoid downloading unknown files.",
                    "Use HTTPS websites for secure communication.",
                    "Avoid public Wi-Fi when accessing sensitive information." } },
                { "what should i do if i suspect phishing?", new List<string> {
                    "Do not click any links! Verify the sender and report it.",
                    "Contact your IT support if you suspect phishing.",
                    "Trust your instincts — delete anything suspicious!" } },
                { "why is two-factor authentication important?", new List<string> {
                    "2FA adds a second layer of security, like a one-time code sent to your phone.",
                    "Even if someone steals your password, 2FA helps protect your account." } },
                { "how can i protect my personal information online?", new List<string> {
                    "Limit what you post on social media.",
                    "Use privacy settings on websites and apps.",
                    "Never overshare — personal info is valuable to cybercriminals." } },
                { "what are some common cybersecurity threats?", new List<string> {
                    "Common threats include phishing, malware, and ransomware.",
                    "Cyber threats can also come through social engineering — always verify requests.",
                    "Stay informed, use antivirus software, and practice safe habits!" } }
            };

            // Keyword-based handlers
            keywordHandlers = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", () => RespondWith("password", "🔐 Make sure to use strong, unique passwords for each account.") },
                { "scam", () => RespondWith("scam", "🚨 Be cautious of online scams. Don’t click suspicious links or give out personal info.") },
                { "privacy", () => RespondWith("privacy", "🔒 Protect your privacy by adjusting settings and avoiding oversharing online.") },
                { "phishing", () =>
                    {
                        lastTopic = "phishing";
                        favoriteTopic = string.IsNullOrEmpty(favoriteTopic) ? "phishing awareness" : favoriteTopic;
                        var tips = new List<string>
                        {
                            "Be cautious of emails asking for personal information.",
                            "Don't click on links from unknown senders.",
                            "Check the sender's email address closely — scammers often fake domains."
                        };
                        TypeResponse(tips[random.Next(tips.Count)]);
                    }
                }
            };
        }

        private void TypeResponse(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
        }

        private void RespondWith(string topic, string message)
        {
            lastTopic = topic;
            favoriteTopic = string.IsNullOrEmpty(favoriteTopic) ? topic : favoriteTopic;
            TypeResponse(message);
        }

        private bool HandleSentiment(string input)
        {
            string lower = input.ToLower();

            if (lower.Contains("worried"))
            {
                TypeResponse("😟 It's completely understandable to feel that way. Scammers can be very convincing. Let me help you stay safe.");
                return true;
            }
            else if (lower.Contains("frustrated"))
            {
                TypeResponse("😣 I get it — cybersecurity can feel overwhelming. You're not alone, and I'm here to help step by step.");
                return true;
            }
            else if (lower.Contains("curious"))
            {
                TypeResponse("😊 Curiosity is a great way to stay informed! Ask me anything you'd like to learn about cybersecurity.");
                return true;
            }

            return false;
        }

        public void RespondToUser(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Bot: I didn't catch that. Could you please say it again?");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nBot: ");
            Console.ResetColor();

            string lowerInput = userInput.ToLower();

            if (HandleSentiment(userInput))
            {
                Console.WriteLine(new string('-', 50));
                return;
            }

            if (lowerInput.StartsWith("my name is "))
            {
                userName = userInput.Substring(11).Trim();
                TypeResponse($"Nice to meet you, {userName}! I'm here to help you stay cyber safe.");
                return;
            }
            else if (lowerInput.StartsWith("i'm interested in ") || lowerInput.StartsWith("i am interested in "))
            {
                int index = userInput.IndexOf("interested in") + "interested in".Length;
                favoriteTopic = userInput.Substring(index).Trim();
                TypeResponse($"Great! I'll remember that you're interested in {favoriteTopic}. It's a crucial part of staying safe online.");
                lastTopic = favoriteTopic;
                return;
            }

            if (lowerInput.Contains("don't understand") || lowerInput.Contains("explain") || lowerInput.Contains("what does that mean"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    HandleFollowUp(lastTopic);
                }
                else
                {
                    TypeResponse("Sure! Ask me about any cybersecurity topic, like phishing or passwords.");
                }
                Console.WriteLine(new string('-', 50));
                return;
            }

            if (lowerInput.Contains("remind me") || lowerInput.Contains("what do you remember"))
            {
                if (!string.IsNullOrEmpty(favoriteTopic))
                {
                    TypeResponse($"As someone interested in {favoriteTopic}, you might want to review your security settings regularly.");
                }
                else
                {
                    TypeResponse("You haven’t told me your favorite topic yet. You can say, for example: I'm interested in privacy.");
                }
                return;
            }

            // Exact phrase match
            if (exactResponses.TryGetValue(userInput, out List<string> matchList))
            {
                string response = matchList[random.Next(matchList.Count)];
                if (!string.IsNullOrEmpty(userName))
                    response = $"Hey {userName}, here's a tip: " + response;

                TypeResponse(response);
                lastTopic = userInput;
            }
            else
            {
                // Keyword-based fallback
                bool matched = false;
                foreach (var keyword in keywordHandlers.Keys)
                {
                    if (lowerInput.Contains(keyword))
                    {
                        keywordHandlers[keyword].Invoke();
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    lastTopic = "";
                    TypeResponse("🤖 I'm not sure I understand. Can you try rephrasing?");
                }
            }

            Console.WriteLine(new string('-', 50));
        }

        private void HandleFollowUp(string topic)
        {
            switch (topic.ToLower())
            {
                case "password":
                    TypeResponse("Passwords should be long, unpredictable, and unique to each account.");
                    break;
                case "scam":
                    TypeResponse("Online scams trick people using fake emails or offers. Always verify before responding.");
                    break;
                case "privacy":
                    TypeResponse("Privacy means keeping your personal data safe. Use strong privacy settings and limit info sharing.");
                    break;
                case "phishing":
                    TypeResponse("Phishing is when attackers impersonate trusted entities to steal data. Look for red flags in emails.");
                    break;
                default:
                    TypeResponse("Let’s continue. Ask me more about cybersecurity!");
                    break;
            }
        }
    }
}
