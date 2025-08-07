import ctypes
import time
import random
import threading
import win32con
import win32api
import win32gui
import pyaudio
import pyfiglet
from win32api import RGB
from ctypes import wintypes

pyfiglet.print_figlet("CARBONMONOXIDE", font="ansi_shadow", colors="CYAN")

print("My first GDI malware. Flashing lights warning!")
input("Press Enter to continue...")


# =================== Setup DPI ===================
try:
    ctypes.windll.shcore.SetProcessDpiAwareness(1)
except:
    ctypes.windll.user32.SetProcessDPIAware()

# =================== Screen Setup ===================
x = win32api.GetSystemMetrics(76)
y = win32api.GetSystemMetrics(77)
w = win32api.GetSystemMetrics(78)
h = win32api.GetSystemMetrics(79)

# =================== Bytebeat Player ===================
class BytebeatPlayer(threading.Thread):
    def __init__(self):
        super().__init__(daemon=True)
        self.running = threading.Event()
        self.running.set()

    def stop(self):
        self.running.clear()

    def run(self):
        p = pyaudio.PyAudio()
        stream = p.open(format=pyaudio.paUInt8, channels=1, rate=8000, output=True)
        t = 0
        while self.running.is_set():
            buffer = bytes(
                ((int((tt * (tt >> 5 | tt >> 8)) >> (tt >> 16)) & 0xFF) for tt in range(t, t + 800))
            )
            stream.write(buffer)
            t += 800
        stream.stop_stream()
        stream.close()
        p.terminate()

class BytebeatPlayer44kHz(threading.Thread):
    def __init__(self):
        super().__init__(daemon=True)
        self.running = threading.Event()
        self.running.set()

    def stop(self):
        self.running.clear()

    def run(self):
        import numpy as np
        p = pyaudio.PyAudio()
        stream = p.open(format=pyaudio.paUInt8, channels=1, rate=44100, output=True)
        t = 0
        while self.running.is_set():
            # Generate 1024 samples at a time
            buffer = bytes(
                (
                    (
                        (
                            ((tt >> 6) ^ (tt & (tt >> 9)) ^ (tt >> 12) | ((tt << ((tt >> 6) % 4)) ^ -tt & -tt >> 13) % 128 ^ -tt >> 1)
                        ) & 0xFF
                    )
                    for tt in range(t, t + 1024)
                )
            )
            stream.write(buffer)
            t += 1024
        stream.stop_stream()
        stream.close()
        p.terminate()

# =================== Base Effect Class ===================
class BaseEffect:
    def __init__(self, hdc, memdc, x, y, w, h):
        self.hdc = hdc
        self.memdc = memdc
        self.x, self.y, self.w, self.h = x, y, w, h

    def run(self):
        raise NotImplementedError

# =================== Tunnel + Invert Effect ===================
class TunnelInvertEffect(BaseEffect):
    def run(self):
        # Capture screen
        ctypes.windll.gdi32.BitBlt(self.memdc, 0, 0, self.w, self.h, self.hdc, self.x, self.y, win32con.SRCCOPY)

        # Tunnel effect
        for i in range(10):
            offset = i * 10
            ctypes.windll.gdi32.StretchBlt(
                self.hdc, self.x + offset, self.y + offset, self.w - 2 * offset, self.h - 2 * offset,
                self.memdc, 0, 0, self.w, self.h,
                win32con.SRCCOPY
            )

        # Invert colors
        ctypes.windll.gdi32.BitBlt(self.hdc, self.x, self.y, self.w, self.h, self.hdc, self.x, self.y, win32con.NOTSRCCOPY)

# =================== Icon Spam Effect ===================
class IconSpamEffect(BaseEffect):
    def __init__(self, hdc, memdc, x, y, w, h):
        super().__init__(hdc, memdc, x, y, w, h)
        self.icons = [
            win32gui.LoadIcon(0, win32con.IDI_ERROR),
            win32gui.LoadIcon(0, win32con.IDI_WARNING),
            win32gui.LoadIcon(0, win32con.IDI_INFORMATION),
            win32gui.LoadIcon(0, win32con.IDI_QUESTION)
        ]
        self.hbrush = ctypes.windll.gdi32.GetStockObject(win32con.NULL_BRUSH)

    def run(self):
        for _ in range(30):
            icon = random.choice(self.icons)
            xpos = random.randint(self.x, self.x + self.w - 32)
            ypos = random.randint(self.y, self.y + self.h - 32)
            win32gui.DrawIconEx(self.hdc, xpos, ypos, icon, 32, 32, 0, self.hbrush, win32con.DI_NORMAL)

class IconTunnelInvertEffect(BaseEffect):
    def run(self):
        # Capture screen
        ctypes.windll.gdi32.BitBlt(self.memdc, 0, 0, self.w, self.h, self.hdc, self.x, self.y, win32con.SRCCOPY)

        # Tunnel effect with icons
        for i in range(10):
            offset = i * 10
            ctypes.windll.gdi32.StretchBlt(
                self.hdc, self.x + offset, self.y + offset, self.w - 2 * offset, self.h - 2 * offset,
                self.memdc, 0, 0, self.w, self.h,
                win32con.SRCCOPY
            )
        self.icons = [
            win32gui.LoadIcon(0, win32con.IDI_ERROR),
            win32gui.LoadIcon(0, win32con.IDI_WARNING),
            win32gui.LoadIcon(0, win32con.IDI_INFORMATION),
            win32gui.LoadIcon(0, win32con.IDI_QUESTION)
        ]
        self.hbrush = ctypes.windll.gdi32.GetStockObject(win32con.NULL_BRUSH)

        # Draw icons
        for _ in range(30):
            icon = random.choice(self.icons)
            xpos = random.randint(self.x, self.x + self.w - 32)
            ypos = random.randint(self.y, self.y + self.h - 32)
            win32gui.DrawIconEx(self.hdc, xpos, ypos, icon, 32, 32, 0, self.hbrush, win32con.DI_NORMAL)

        # Invert colors
        ctypes.windll.gdi32.BitBlt(self.hdc, self.x, self.y, self.w, self.h, self.hdc, self.x, self.y, win32con.NOTSRCCOPY)


class ColorEffect(BaseEffect):
    def run(self):
        # Capture screen
        ctypes.windll.gdi32.BitBlt(self.memdc, 0, 0, self.w, self.h, self.hdc, self.x, self.y, win32con.SRCCOPY)

        # Pick a random raster operation for color filtering
        rop_codes = [
            win32con.PATINVERT,    # Pattern Invert
            win32con.NOTSRCCOPY,   # Invert
            win32con.MERGECOPY,    # Merge with pattern
            win32con.SRCPAINT,     # OR
            win32con.SRCINVERT,    # XOR
            win32con.PATCOPY,      # Pattern copy
        ]
        rop = random.choice(rop_codes)

        # Create a solid brush with a random color
        color = RGB(random.randint(0,255), random.randint(0,255), random.randint(0,255))
        brush = win32gui.CreateSolidBrush(color)
        # Use brush handle directly (do not wrap with wintypes.HANDLE)
        old_brush = ctypes.windll.gdi32.SelectObject(int(self.hdc), int(brush))

        # Fill a rectangle with the brush and chosen ROP
        ctypes.windll.gdi32.PatBlt(
            int(self.hdc),
            int(self.x),
            int(self.y),
            int(self.w),
            int(self.h),
            int(rop)
        )

        # Spam text on screen
        texts = ["carbonmonoxide.py", "un3nown", "desktop destroyer", "blau weiss linz"]
        for _ in range(20):
            msg = random.choice(texts)
            xpos = random.randint(self.x, self.x + self.w - 100)
            ypos = random.randint(self.y, self.y + self.h - 30)
            color = RGB(random.randint(0,255), random.randint(0,255), random.randint(0,255))
            win32gui.SetTextColor(self.hdc, color)
            win32gui.SetBkMode(self.hdc, win32con.TRANSPARENT)
            # Draw text using TextOutW (unicode)
            ctypes.windll.gdi32.TextOutW(
                int(self.hdc),
                int(xpos),
                int(ypos),
                msg,
                len(msg)
            )

        # Restore old brush and delete created brush
        ctypes.windll.gdi32.SelectObject(int(self.hdc), int(old_brush))
        ctypes.windll.gdi32.DeleteObject(int(brush))

class InvertRandColors(BaseEffect):
    def __init__(self, hdc, memdc, x, y, w, h):
        super().__init__(hdc, memdc, x, y, w, h)
        self.colors = [0xFF0000, 0xFF00BC, 0x00FF33, 0xFFF700, 0x00FFEF]

    def run(self):
        # Create brush with random color from predefined list
        color = random.choice(self.colors)
        brush = win32gui.CreateSolidBrush(color)
        old_brush = ctypes.windll.gdi32.SelectObject(int(self.hdc), int(brush))

        # Fill entire screen with color using PATINVERT
        ctypes.windll.gdi32.PatBlt(
            int(self.hdc),
            int(self.x), 
            int(self.y),
            int(self.w),
            int(self.h),
            win32con.PATINVERT
        )

        # Cleanup
        ctypes.windll.gdi32.SelectObject(int(self.hdc), int(old_brush))
        ctypes.windll.gdi32.DeleteObject(int(brush))
        
        time.sleep(0.05) # Short delay between color changes

class MeltingScreenEffect(BaseEffect):
    def __init__(self, hdc, memdc, x, y, w, h):
        super().__init__(hdc, memdc, x, y, w, h)
        self.melt_positions = {}  # Track melting positions for each column
        
    def run(self):
        # Capture current screen
        ctypes.windll.gdi32.BitBlt(self.memdc, 0, 0, self.w, self.h, self.hdc, self.x, self.y, win32con.SRCCOPY)
        
        # Create melting effect by copying strips with vertical offsets
        strip_width = 4  # Width of each melting strip
        max_melt = 50   # Maximum pixels to melt down
        
        for strip_x in range(0, self.w, strip_width):
            # Get or initialize melt position for this strip
            if strip_x not in self.melt_positions:
                self.melt_positions[strip_x] = 0
            
            # Randomly increase melt position
            if random.randint(1, 20) == 1:  # 5% chance to melt more
                self.melt_positions[strip_x] += random.randint(1, 5)
            
            # Cap the melt position
            if self.melt_positions[strip_x] > max_melt:
                self.melt_positions[strip_x] = max_melt
            
            melt_offset = self.melt_positions[strip_x]
            
            # Copy the strip with vertical offset (melting down)
            actual_width = min(strip_width, self.w - strip_x)
            
            # Copy the melted strip
            if melt_offset > 0:
                ctypes.windll.gdi32.BitBlt(
                    ctypes.c_void_p(self.hdc),
                    ctypes.c_int(self.x + strip_x),
                    ctypes.c_int(self.y + melt_offset),
                    ctypes.c_int(actual_width),
                    ctypes.c_int(self.h - melt_offset),
                    ctypes.c_void_p(self.memdc),
                    ctypes.c_int(strip_x),
                    ctypes.c_int(0),
                    ctypes.c_uint(win32con.SRCCOPY)
                )

                
                # Fill the top area with background color or black
                black_brush = ctypes.windll.gdi32.GetStockObject(win32con.BLACK_BRUSH)
                old_brush = ctypes.windll.gdi32.SelectObject(self.hdc, black_brush)
                
                # Fill the gap at the top
                ctypes.windll.gdi32.PatBlt(
                    ctypes.c_void_p(self.hdc),
                    ctypes.c_int(self.x + strip_x),
                    ctypes.c_int(self.y),
                    ctypes.c_int(actual_width),
                    ctypes.c_int(melt_offset),
                    ctypes.c_uint(win32con.PATCOPY)
                )

                
                ctypes.windll.gdi32.SelectObject(self.hdc, old_brush)
            
            # Add some dripping effect with random pixels
            if melt_offset > 10 and random.randint(1, 30) == 1:
                drip_color = RGB(random.randint(0, 255), random.randint(0, 255), random.randint(0, 255))
                drip_brush = win32gui.CreateSolidBrush(drip_color)
                old_brush = ctypes.windll.gdi32.SelectObject(self.hdc, drip_brush)
                
                # Create small drip
                drip_x = strip_x + random.randint(0, actual_width - 1)
                drip_y = melt_offset + random.randint(0, 20)
                
                ctypes.windll.gdi32.PatBlt(
                    self.hdc,
                    self.x + drip_x,
                    self.y + drip_y,
                    2,  # small drip width
                    random.randint(3, 8),  # variable drip height
                    win32con.PATCOPY
                )
                
                ctypes.windll.gdi32.SelectObject(self.hdc, old_brush)
                ctypes.windll.gdi32.DeleteObject(drip_brush)
        
        # Occasionally reset some strips to create flowing effect
        if random.randint(1, 100) == 1:
            reset_strip = random.choice(list(self.melt_positions.keys())) if self.melt_positions else 0
            self.melt_positions[reset_strip] = max(0, self.melt_positions.get(reset_strip, 0) - 10)
        

# =================== Effect Manager ===================
class EffectManager:
    def __init__(self, hdc, memdc, x, y, w, h):
        self.effects = [
            (TunnelInvertEffect(hdc, memdc, x, y, w, h), 9),
            (IconSpamEffect(hdc, memdc, x, y, w, h), 6),
            (IconTunnelInvertEffect(hdc, memdc, x, y, w, h), 7),
            (ColorEffect(hdc, memdc, x, y, w, h), 6),
            (InvertRandColors(hdc, memdc, x, y, w, h), 7),
            (MeltingScreenEffect(hdc, memdc, x, y, w, h), None)

        ]
        self.start_time = time.time()
        self.current_index = 0
        self.bytebeat8khz = None
        self.bytebeat44khz = None
        self.switched_to_color_effect = False

    def get_current_effect(self):
        # If we've switched, always return ColorEffect
        if self.switched_to_color_effect:
            return self.effects[0][0]
        if self.current_index >= len(self.effects):
            # Signal to main loop to exit
            raise StopIteration
        current_effect, duration = self.effects[self.current_index]
        if duration is not None and time.time() - self.start_time > duration:
            self.current_index += 1
            self.start_time = time.time()
            return self.get_current_effect()
        return current_effect

    def run(self):
        effect = self.get_current_effect()
        # Only one bytebeat at a time
        if isinstance(effect, ColorEffect) or isinstance(effect, InvertRandColors)  or isinstance(effect, MeltingScreenEffect):
            # Stop 8kHz if running
            if self.bytebeat8khz is not None and self.bytebeat8khz.is_alive():
                self.bytebeat8khz.stop()
                self.bytebeat8khz.join()
                self.bytebeat8khz = None
            # Start 44kHz if not running
            if self.bytebeat44khz is None or not self.bytebeat44khz.is_alive():
                self.bytebeat44khz = BytebeatPlayer44kHz()
                self.bytebeat44khz.start()
        else:
            # Stop 44kHz if running
            if self.bytebeat44khz is not None and self.bytebeat44khz.is_alive():
                self.bytebeat44khz.stop()
                self.bytebeat44khz.join()
                self.bytebeat44khz = None
            # Start 8kHz if not running
            if self.bytebeat8khz is None or not self.bytebeat8khz.is_alive():
                self.bytebeat8khz = BytebeatPlayer()
                self.bytebeat8khz.start()
        effect.run()

# =================== Main Loop ===================
def main():
    # GDI setup
    hdc = ctypes.windll.user32.GetDC(0)
    memdc = ctypes.windll.gdi32.CreateCompatibleDC(hdc)
    bitmap = ctypes.windll.gdi32.CreateCompatibleBitmap(hdc, w, h)
    ctypes.windll.gdi32.SelectObject(memdc, bitmap)

    # Effect manager
    manager = EffectManager(hdc, memdc, x, y, w, h)

    try:
        while True:
            try:
                manager.run()
            except StopIteration:
                break
            time.sleep(0.01)
    except KeyboardInterrupt:
        pass
    finally:
        # Cleanup
        if manager.bytebeat8khz is not None and manager.bytebeat8khz.is_alive():
            manager.bytebeat8khz.stop()
            manager.bytebeat8khz.join()
        if manager.bytebeat44khz is not None and manager.bytebeat44khz.is_alive():
            manager.bytebeat44khz.stop()
            manager.bytebeat44khz.join()
        ctypes.windll.gdi32.DeleteObject(bitmap)
        ctypes.windll.gdi32.DeleteDC(memdc)
        ctypes.windll.user32.ReleaseDC(0, hdc)

if __name__ == "__main__":
    main()