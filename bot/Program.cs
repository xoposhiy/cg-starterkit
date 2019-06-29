using System;

namespace bot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var reader = new StateReader();
            var ai = new Ai();
            var init = reader.ReadInitFromConsole();
            while (true)
            {
                var state = reader.ReadFromConsole(init);
                var command = ai.GetCommand(state);
                Console.WriteLine(command);
            }
        }
    }
}
