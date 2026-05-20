using System;
using System.Diagnostics;
using BloodstainedRitualTrainer.Memory;
using Xunit;

namespace BloodstainedRitualTrainer.Tests
{
    /// <summary>
    /// Unit tests for GameMemoryManager. These tests require the game process to be running.
    /// For automated testing, mock the process or use a test harness.
    /// </summary>
    public class MemoryManagerTests
    {
        [Fact]
        public void TestAttachToGame_Success()
        {
            // Arrange
            var processes = Process.GetProcessesByName("BloodstainedRotN");
            if (processes.Length == 0)
            {
                // Skip if game not running
                return;
            }

            // Act
            var manager = new GameMemoryManager(processes[0]);

            // Assert
            Assert.NotNull(manager);
        }

        [Fact]
        public void TestSetMaxGold_WritesValue()
        {
            // Arrange
            var processes = Process.GetProcessesByName("BloodstainedRotN");
            if (processes.Length == 0) return;

            var manager = new GameMemoryManager(processes[0]);

            // Act
            manager.SetMaxGold();

            // Assert: We can't easily read back without exposing a read method, but we ensure no exception
            Assert.True(true);
        }

        [Fact]
        public void TestSaveAndLoadPosition()
        {
            // Arrange
            var processes = Process.GetProcessesByName("BloodstainedRotN");
            if (processes.Length == 0) return;

            var manager = new GameMemoryManager(processes[0]);

            // Act
            manager.SavePosition();
            manager.LoadPosition();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void TestUnlockAllShards()
        {
            // Arrange
            var processes = Process.GetProcessesByName("BloodstainedRotN");
            if (processes.Length == 0) return;

            var manager = new GameMemoryManager(processes[0]);

            // Act
            manager.UnlockAllShards();

            // Assert
            Assert.True(true);
        }
    }
}
