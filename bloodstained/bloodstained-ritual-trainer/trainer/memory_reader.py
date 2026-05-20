import pymem
import pymem.process

class MemoryReader:
    """Handles reading and writing to the Bloodstained: Ritual of the Night process memory."""

    def __init__(self):
        self.pm = None
        self.process = None
        self.base_address = None

    def attach(self):
        """Attach to the Bloodstained process."""
        try:
            self.pm = pymem.Pymem("BloodstainedRotN.exe")
            self.process = pymem.process.process_from_name("BloodstainedRotN.exe")
            # Base address is typically the main module base
            self.base_address = pymem.process.module_from_name(self.pm.process_handle, "BloodstainedRotN.exe").lpBaseOfDll
            return True
        except pymem.exception.ProcessNotFound:
            return False

    def read_int(self, address_offset):
        """Read an integer from a relative offset from base address."""
        if self.pm is None:
            return None
        try:
            absolute = self.base_address + address_offset
            return self.pm.read_int(absolute)
        except Exception:
            return None

    def write_int(self, address_offset, value):
        """Write an integer to a relative offset from base address."""
        if self.pm is None:
            return False
        try:
            absolute = self.base_address + address_offset
            self.pm.write_int(absolute, value)
            return True
        except Exception:
            return False

    def read_float(self, address_offset):
        """Read a float from a relative offset from base address."""
        if self.pm is None:
            return None
        try:
            absolute = self.base_address + address_offset
            return self.pm.read_float(absolute)
        except Exception:
            return None

    def write_float(self, address_offset, value):
        """Write a float to a relative offset from base address."""
        if self.pm is None:
            return False
        try:
            absolute = self.base_address + address_offset
            self.pm.write_float(absolute, value)
            return True
        except Exception:
            return False

    def close(self):
        """Close the memory handle."""
        if self.pm is not None:
            self.pm.close_process()
