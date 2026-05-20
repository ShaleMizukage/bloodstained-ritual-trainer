using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BloodstainedRitualTrainer.Memory
{
    /// <summary>
    /// Manages reading and writing to the game's memory for cheats.
    /// Addresses are based on game version 1.4 (Steam). Use at your own risk.
    /// </summary>
    public class GameMemoryManager
    {
        private readonly Process _gameProcess;
        private readonly IntPtr _processHandle;
        private IntPtr _baseAddress;

        // Example static addresses (would need to be updated per game version)
        private const int HPOffset = 0x1A2B3C;
        private const int MPOffset = 0x1A2B40;
        private const int GoldOffset = 0x1A2B44;
        private const int StatsOffset = 0x1A2B48;
        private const int ShardArrayOffset = 0x1A2B60;
        private const int PositionXOffset = 0x1A2BA0;
        private const int PositionYOffset = 0x1A2BA4;
        private const int PositionZOffset = 0x1A2BA8;

        private float _savedX, _savedY, _savedZ;
        private bool _hpPatched;
        private bool _mpPatched;

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        public GameMemoryManager(Process process)
        {
            _gameProcess = process;
            _processHandle = process.Handle;
            _baseAddress = process.MainModule.BaseAddress;
        }

        /// <summary>
        /// Reads a float value from the game's memory at a given offset.
        /// </summary>
        private float ReadFloat(int offset)
        {
            byte[] buffer = new byte[4];
            IntPtr address = IntPtr.Add(_baseAddress, offset);
            if (ReadProcessMemory(_processHandle, address, buffer, 4, out _))
            {
                return BitConverter.ToSingle(buffer, 0);
            }
            return 0f;
        }

        /// <summary>
        /// Writes a float value to the game's memory at a given offset.
        /// </summary>
        private void WriteFloat(int offset, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            IntPtr address = IntPtr.Add(_baseAddress, offset);
            WriteProcessMemory(_processHandle, address, buffer, 4, out _);
        }

        /// <summary>
        /// Writes an integer value to the game's memory at a given offset.
        /// </summary>
        private void WriteInt(int offset, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            IntPtr address = IntPtr.Add(_baseAddress, offset);
            WriteProcessMemory(_processHandle, address, buffer, 4, out _);
        }

        /// <summary>
        /// Enables infinite HP by freezing the HP value at a high amount.
        /// </summary>
        public void SetInfiniteHP(bool enable)
        {
            if (enable && !_hpPatched)
            {
                WriteFloat(HPOffset, 9999f);
                _hpPatched = true;
            }
            else if (!enable)
            {
                _hpPatched = false;
            }
        }

        /// <summary>
        /// Enables infinite MP by freezing the MP value at a high amount.
        /// </summary>
        public void SetInfiniteMP(bool enable)
        {
            if (enable && !_mpPatched)
            {
                WriteFloat(MPOffset, 9999f);
                _mpPatched = true;
            }
            else if (!enable)
            {
                _mpPatched = false;
            }
        }

        /// <summary>
        /// Sets the player's gold to a maximum value.
        /// </summary>
        public void SetMaxGold()
        {
            WriteInt(GoldOffset, 999999);
        }

        /// <summary>
        /// Unlocks all shards by writing a specific pattern to the shard array.
        /// </summary>
        public void UnlockAllShards()
        {
            // Simulate unlocking all shards by writing a large number of shard IDs
            for (int i = 0; i < 50; i++)
            {
                WriteInt(ShardArrayOffset + (i * 4), 1); // 1 = unlocked
            }
        }

        /// <summary>
        /// Maximizes all player stats (HP, MP, STR, CON, INT, MND, LCK).
        /// </summary>
        public void MaxStats()
        {
            WriteFloat(HPOffset, 9999f);
            WriteFloat(MPOffset, 9999f);
            WriteInt(StatsOffset, 99);      // STR
            WriteInt(StatsOffset + 4, 99);   // CON
            WriteInt(StatsOffset + 8, 99);   // INT
            WriteInt(StatsOffset + 12, 99);  // MND
            WriteInt(StatsOffset + 16, 99);  // LCK
        }

        /// <summary>
        /// Saves the player's current position to memory.
        /// </summary>
        public void SavePosition()
        {
            _savedX = ReadFloat(PositionXOffset);
            _savedY = ReadFloat(PositionYOffset);
            _savedZ = ReadFloat(PositionZOffset);
        }

        /// <summary>
        /// Loads the saved player position.
        /// </summary>
        public void LoadPosition()
        {
            WriteFloat(PositionXOffset, _savedX);
            WriteFloat(PositionYOffset, _savedY);
            WriteFloat(PositionZOffset, _savedZ);
        }
    }
}
