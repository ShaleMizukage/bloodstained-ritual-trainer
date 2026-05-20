import sys
import time
import keyboard
from trainer.memory_reader import MemoryReader
from trainer.cheats import Cheats

def main():
    """Main entry point for the Bloodstained: Ritual of the Night trainer."""
    print("Bloodstained: Ritual of the Night Trainer")
    print("----------------------------------------")
    print("Hotkeys:")
    print("  F1 - Toggle Infinite HP")
    print("  F2 - Toggle Infinite MP")
    print("  F3 - Heal to Full")
    print("  F4 - Set Gold to 99999")
    print("  F5 - Teleport to (0, 0, 0)")
    print("  ESC - Exit")
    print()

    mem = MemoryReader()
    if not mem.attach():
        print("Error: Could not find BloodstainedRotN.exe process. Is the game running?")
        sys.exit(1)

    cheats = Cheats(mem)
    infinite_hp = False
    infinite_mp = False

    print("Trainer attached successfully. Press ESC to exit.")

    try:
        while True:
            if keyboard.is_pressed('F1'):
                infinite_hp = not infinite_hp
                print(f"Infinite HP: {'ON' if infinite_hp else 'OFF'}")
                time.sleep(0.3)  # Debounce

            if keyboard.is_pressed('F2'):
                infinite_mp = not infinite_mp
                print(f"Infinite MP: {'ON' if infinite_mp else 'OFF'}")
                time.sleep(0.3)

            if keyboard.is_pressed('F3'):
                cheats.heal_full()
                print("Healed to full HP/MP")
                time.sleep(0.3)

            if keyboard.is_pressed('F4'):
                cheats.set_gold(99999)
                print("Gold set to 99999")
                time.sleep(0.3)

            if keyboard.is_pressed('F5'):
                cheats.teleport_to(0.0, 0.0, 0.0)
                print("Teleported to (0, 0, 0)")
                time.sleep(0.3)

            if keyboard.is_pressed('ESC'):
                print("Exiting...")
                break

            # Apply infinite health/mp every loop if enabled
            if infinite_hp:
                cheats.set_infinite_hp(True)
            if infinite_mp:
                cheats.set_infinite_mp(True)

            time.sleep(0.05)  # Prevent CPU hogging

    except KeyboardInterrupt:
        pass
    finally:
        mem.close()
        print("Trainer detached.")

if __name__ == "__main__":
    main()
