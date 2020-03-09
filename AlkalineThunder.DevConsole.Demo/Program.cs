using System;

namespace AlkalineThunder.DevConsole.Demo
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DevConsoleGame())
                game.Run();
        }
    }
}
