using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    public static class ServerUI
    {
        private static List<(int, ConsoleColor?, string)> consoleLines = new List<(int, ConsoleColor?, string)>();

        private static int consoleLineIndex = 0;
        private static bool followLogger = true;
        
        private static string input = "";
        
        private static Dictionary<string, Action<string[]>> commands = new Dictionary<string, Action<string[]>>();
        
        public static void Log(string message, ConsoleColor? color = null)
        {
            var last = consoleLines.LastOrDefault();
            if (last == default) goto AddAnyway;
            
            if (message == last.Item3 && color == last.Item2)
                consoleLines[consoleLines.Count - 1] = (last.Item1 + 1, last.Item2, last.Item3);
            
            AddAnyway:
            consoleLines.Add((1, color, message));
            
            //if (consoleLineIndex < consoleLines.Count - 32)
        }

        public static void RefreshUI()
        {
            Console.SetBufferSize(120, 40);
            Console.SetWindowSize(120, 40);
            
            //for (int i = consoleLineIndex;)
        }

        public static void RegisterCommand(string commandName, Action<string[]> onInvoke)
        {
            commands.Add(commandName, onInvoke);
        }

        public static void OnServerLoop()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        break; // TODO commands system
                    case ConsoleKey.UpArrow:
                        consoleLineIndex = Math.Max(0, consoleLineIndex - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        consoleLineIndex = Math.Min(consoleLineIndex + 1, Math.Max(0, consoleLines.Count));
                        if (consoleLineIndex >= consoleLines.Count - 1)
                            followLogger = true;
                        break;
                    case ConsoleKey.Backspace:
                        input = input.Remove(input.Length - 1, 1);
                        break;
                    case ConsoleKey.U:
                        if (key.Modifiers == ConsoleModifiers.Control)
                        {
                            followLogger ^= true;
                            break;
                        }
                        input += key.KeyChar;
                        break;
                    default:
                        input += key.KeyChar;
                        break;
                }
            }
        }
    }
}