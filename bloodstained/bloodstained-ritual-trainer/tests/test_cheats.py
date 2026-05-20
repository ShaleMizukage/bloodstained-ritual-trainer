import unittest
from unittest.mock import MagicMock, patch
from trainer.memory_reader import MemoryReader
from trainer.cheats import Cheats

class TestCheats(unittest.TestCase):
    """Tests for the Cheats class using a mock MemoryReader."""

    def setUp(self):
        self.mock_mem = MagicMock(spec=MemoryReader)
        self.cheats = Cheats(self.mock_mem)

    def test_set_infinite_hp_enabled(self):
        # Mock read_int to return a max HP of 500
        self.mock_mem.read_int.return_value = 500
        self.cheats.set_infinite_hp(True)
        # Should write max HP to current HP
        self.mock_mem.write_int.assert_called_once()
        args, _ = self.mock_mem.write_int.call_args
        self.assertEqual(args[1], 500)

    def test_set_infinite_hp_disabled(self):
        self.mock_mem.read_int.return_value = 500
        self.cheats.set_infinite_hp(False)
        # Should not write anything when disabled
        self.mock_mem.write_int.assert_not_called()

    def test_set_gold_positive(self):
        self.cheats.set_gold(1000)
        self.mock_mem.write_int.assert_called_once()
        args, _ = self.mock_mem.write_int.call_args
        self.assertEqual(args[1], 1000)

    def test_set_gold_negative(self):
        self.cheats.set_gold(-50)
        # Should not write negative gold
        self.mock_mem.write_int.assert_not_called()

    def test_teleport_to(self):
        self.cheats.teleport_to(10.5, 20.3, 30.7)
        # Should write three floats
        self.assertEqual(self.mock_mem.write_float.call_count, 3)
        calls = self.mock_mem.write_float.call_args_list
        self.assertAlmostEqual(calls[0][0][1], 10.5)
        self.assertAlmostEqual(calls[1][0][1], 20.3)
        self.assertAlmostEqual(calls[2][0][1], 30.7)

    def test_heal_full(self):
        self.mock_mem.read_int.side_effect = [300, 150]  # max_hp, max_mp
        self.cheats.heal_full()
        # Should write both HP and MP
        self.assertEqual(self.mock_mem.write_int.call_count, 2)
        first_call_args = self.mock_mem.write_int.call_args_list[0][0]
        second_call_args = self.mock_mem.write_int.call_args_list[1][0]
        self.assertEqual(first_call_args[1], 300)
        self.assertEqual(second_call_args[1], 150)

if __name__ == '__main__':
    unittest.main()
