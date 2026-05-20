from trainer.memory_reader import MemoryReader
from trainer.offsets import Offsets

class Cheats:
    """Provides cheat functions for Bloodstained: Ritual of the Night."""

    def __init__(self, memory_reader: MemoryReader):
        self.mem = memory_reader

    def set_infinite_hp(self, enabled: bool):
        """Set HP to max or restore on low HP."""
        if enabled:
            max_hp = self.mem.read_int(Offsets.HP_BASE + Offsets.MAX_HP_OFFSET)
            if max_hp is not None:
                self.mem.write_int(Offsets.HP_BASE + Offsets.HP_OFFSET, max_hp)

    def set_infinite_mp(self, enabled: bool):
        """Set MP to max or restore on low MP."""
        if enabled:
            max_mp = self.mem.read_int(Offsets.MP_BASE + Offsets.MAX_MP_OFFSET)
            if max_mp is not None:
                self.mem.write_int(Offsets.MP_BASE + Offsets.MP_OFFSET, max_mp)

    def set_gold(self, amount: int):
        """Set player gold to a specific amount."""
        if amount >= 0:
            self.mem.write_int(Offsets.GOLD_BASE + Offsets.GOLD_OFFSET, amount)

    def teleport_to(self, x: float, y: float, z: float):
        """Teleport player to given coordinates."""
        self.mem.write_float(Offsets.POSITION_BASE + Offsets.POS_X_OFFSET, x)
        self.mem.write_float(Offsets.POSITION_BASE + Offsets.POS_Y_OFFSET, y)
        self.mem.write_float(Offsets.POSITION_BASE + Offsets.POS_Z_OFFSET, z)

    def heal_full(self):
        """Restore HP and MP to maximum."""
        max_hp = self.mem.read_int(Offsets.HP_BASE + Offsets.MAX_HP_OFFSET)
        max_mp = self.mem.read_int(Offsets.MP_BASE + Offsets.MAX_MP_OFFSET)
        if max_hp is not None:
            self.mem.write_int(Offsets.HP_BASE + Offsets.HP_OFFSET, max_hp)
        if max_mp is not None:
            self.mem.write_int(Offsets.MP_BASE + Offsets.MP_OFFSET, max_mp)
