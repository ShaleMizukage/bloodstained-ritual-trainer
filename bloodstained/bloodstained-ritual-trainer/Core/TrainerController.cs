using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using BloodstainedRitualTrainer.Memory;

namespace BloodstainedRitualTrainer.Core
{
    /// <summary>
    /// Main controller for the trainer. Manages game attachment, memory patching, and hotkey handling.
    /// </summary>
    public class TrainerController
    {
        private GameMemoryManager? _memoryManager;
        private bool _isAttached;
        private Thread? _hotkeyThread;
        private bool _running;

        public bool IsAttached => _isAttached;

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private const int VK_F1 = 0x70;
        private const int VK_F2 = 0x71;
        private const int VK_F3 = 0x72;
        private const int VK_F4 = 0x73;
        private const int VK_F5 = 0x74;
        private const int VK_F6 = 0x75;
        private const int VK_F7 = 0x76;
        private const int VK_F8 = 0x77;

        /// <summary>
        /// Attempts to attach to the Bloodstained: Ritual of the Night process.
        /// </summary>
        public void AttachToGame()
        {
            try
            {
                var processes = Process.GetProcessesByName("BloodstainedRotN");
                if (processes.Length == 0)
                {
                    _isAttached = false;
                    return;
                }

                var gameProcess = processes[0];
                _memoryManager = new GameMemoryManager(gameProcess);
                _isAttached = true;
                _running = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error attaching to game: {ex.Message}");
                _isAttached = false;
            }
        }

        /// <summary>
        /// Starts a background thread that listens for hotkey presses.
        /// </summary>
        public void StartHotkeyListener()
        {
            if (!_isAttached || _memoryManager == null) return;

            _hotkeyThread = new Thread(() =>
            {
                while (_running)
                {
                    if (GetAsyncKeyState(VK_F1) < 0)
                    {
                        _memoryManager.SetInfiniteHP(true);
                        Console.WriteLine("Infinite HP activated.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F2) < 0)
                    {
                        _memoryManager.SetInfiniteMP(true);
                        Console.WriteLine("Infinite MP activated.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F3) < 0)
                    {
                        _memoryManager.SetMaxGold();
                        Console.WriteLine("Gold set to maximum.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F4) < 0)
                    {
                        _memoryManager.UnlockAllShards();
                        Console.WriteLine("All shards unlocked.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F5) < 0)
                    {
                        _memoryManager.MaxStats();
                        Console.WriteLine("Stats maxed.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F6) < 0)
                    {
                        _memoryManager.SavePosition();
                        Console.WriteLine("Position saved.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F7) < 0)
                    {
                        _memoryManager.LoadPosition();
                        Console.WriteLine("Position loaded.");
                        Thread.Sleep(300);
                    }
                    if (GetAsyncKeyState(VK_F8) < 0)
                    {
                        Console.WriteLine("Exiting trainer...");
                        _running = false;
                        break;
                    }
                    Thread.Sleep(100);
                }
            });
            _hotkeyThread.IsBackground = true;
            _hotkeyThread.Start();
        }
    }
}
