class Offsets:
    """Memory offsets for Bloodstained: Ritual of the Night (version 1.5+).
    These offsets may need updating with game patches.
    """
    # Player HP
    HP_BASE = 0x00A3F8C0       # Pointer to HP structure
    HP_OFFSET = 0x1C           # Offset from HP base to current HP
    MAX_HP_OFFSET = 0x20       # Offset to max HP

    # Player MP
    MP_BASE = 0x00A3F8C4
    MP_OFFSET = 0x1C
    MAX_MP_OFFSET = 0x20

    # Gold / Money
    GOLD_BASE = 0x00A3F8E0
    GOLD_OFFSET = 0x10

    # Shard count (for a specific shard, e.g., index 0)
    SHARD_BASE = 0x00A3F900
    SHARD_OFFSET = 0x04 * 0   # Multiply by shard index

    # Player position (for teleport)
    POSITION_BASE = 0x00A3F940
    POS_X_OFFSET = 0x00
    POS_Y_OFFSET = 0x04
    POS_Z_OFFSET = 0x08
