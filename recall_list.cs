using System;
using System.Collections.Generic;
using System.IO;

namespace memory_recall
{
    public class recall_list
    {
        private string path;
        private List<string> memory_collected;

        public recall_list()
        {
            // Get app path
            string full_path = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = full_path.Replace("bin\\Debug\\", "");
            path = Path.Combine(new_path, "memory.txt");

            // Load existing memory
            memory_collected = loadMemory(path);
        }

        private List<string> loadMemory(string path)
        {
            if (File.Exists(path))
            {
                return new List<string>(File.ReadAllLines(path));
            }
            else
            {
                File.CreateText(path).Close();
                return new List<string>();
            }
        }

        public void SaveInteraction(string userInput)
        {
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                memory_collected.Add(userInput);
                File.WriteAllLines(path, memory_collected);
            }
        }

        public void ShowMemory()
        {
            Console.WriteLine("\n📘 Recalling your past interactions:");
            foreach (var item in memory_collected)
            {
                Console.WriteLine("🔹 " + item);
            }
        }
    }
}
