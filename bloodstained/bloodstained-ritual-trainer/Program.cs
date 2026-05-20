using System;
using System.Threading;
using BloodstainedRitualTrainer.Core;

namespace BloodstainedRitualTrainer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Bloodstained: Ritual of the Night Trainer v1.0");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Waiting for game process...");

            var trainer = new TrainerController();
            trainer.AttachToGame();

            if (trainer.IsAttached)
            {
                Console.WriteLine("Game found! Trainer active.");
                Console.WriteLine("Hotkeys:");
                Console.WriteLine("  F1 - Infinite HP");
                Console.WriteLine("  F2 - Infinite MP");
                Console.WriteLine("  F3 - Max Gold");
                Console.WriteLine("  F4 - Unlock All Shards");
                Console.WriteLine("  F5 - Max Stats");
                Console.WriteLine("  F6 - Save Position");
                Console.WriteLine("  F7 - Load Position");
                Console.WriteLine("  F8 - Exit");

                trainer.StartHotkeyListener();
            }
            else
            {
                Console.WriteLine("Game process not found. Exiting.");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
