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

class ColorFilterEffect(BaseEffect):
    def __init__(self, hdc, memdc, x, y, w, h):
        super().__init__(hdc, memdc, x, y, w, h)
        # Create a DC to store the original screen content
        self.original_dc = ctypes.windll.gdi32.CreateCompatibleDC(hdc)
        self.original_bmp = ctypes.windll.gdi32.CreateCompatibleBitmap(hdc, w, h)
        ctypes.windll.gdi32.SelectObject(self.original_dc, self.original_bmp)
        # Capture initial screen state
        ctypes.windll.gdi32.BitBlt(self.original_dc, 0, 0, w, h, hdc, x, y, win32con.SRCCOPY)
        
        # Color transition setup
        self.start_color = RGB(random.randint(0,255), random.randint(0,255), random.randint(0,255))
        self.target_color = RGB(random.randint(0,255), random.randint(0,255), random.randint(0,255))
        self.transition_start = time.time()
        self.transition_duration = 15.0  # 15 seconds per transition

    def get_current_color(self):
        t = (time.time() - self.transition_start) / self.transition_duration
        if t >= 1.0:
            # Start new transition
            self.start_color = self.target_color
            self.target_color = RGB(random.randint(0,255), random.randint(0,255), random.randint(0,255))
            self.transition_start = time.time()
            t = 0.0
            
        # Linear interpolation between colors
        r1, g1, b1 = self.start_color & 0xFF, (self.start_color >> 8) & 0xFF, (self.start_color >> 16) & 0xFF
        r2, g2, b2 = self.target_color & 0xFF, (self.target_color >> 8) & 0xFF, (self.target_color >> 16) & 0xFF
        
        r = int(r1 + (r2 - r1) * t)
        g = int(g1 + (g2 - g1) * t)
        b = int(b1 + (b2 - b1) * t)
        
        return RGB(r, g, b)

    def run(self):
        # Get interpolated color
        current_color = self.get_current_color()
        
        # Create working DC for current frame
        screen_dc = ctypes.windll.gdi32.CreateCompatibleDC(self.hdc)
        screen_bmp = ctypes.windll.gdi32.CreateCompatibleBitmap(self.hdc, self.w, self.h)
        ctypes.windll.gdi32.SelectObject(screen_dc, screen_bmp)
        
        # Use interpolated color for tinting
        brush = win32gui.CreateSolidBrush(current_color)
        tint_dc = ctypes.windll.gdi32.CreateCompatibleDC(self.hdc)
        tint_bmp = ctypes.windll.gdi32.CreateCompatibleBitmap(self.hdc, self.w, self.h)
        ctypes.windll.gdi32.SelectObject(tint_dc, tint_bmp)
        ctypes.windll.gdi32.SelectObject(tint_dc, int(brush))
        ctypes.windll.gdi32.PatBlt(tint_dc, 0, 0, self.w, self.h, win32con.PATCOPY)

        # 3. Draw the fresh screenshot to the output
        ctypes.windll.gdi32.BitBlt(self.hdc, self.x, self.y, self.w, self.h, screen_dc, 0, 0, win32con.SRCCOPY)

        # 4. Alpha blend the tint over the screenshot
        class BLENDFUNCTION(ctypes.Structure):
            _fields_ = [
                ("BlendOp", ctypes.c_byte),
                ("BlendFlags", ctypes.c_byte),
                ("SourceConstantAlpha", ctypes.c_byte),
                ("AlphaFormat", ctypes.c_byte)
            ]
        blend = BLENDFUNCTION()
        blend.BlendOp = 0  # AC_SRC_OVER
        blend.BlendFlags = 0
        blend.SourceConstantAlpha = 80  # 0-255, lower = more transparent
        blend.AlphaFormat = 0
        ctypes.windll.msimg32.AlphaBlend(
            self.hdc, self.x, self.y, self.w, self.h,
            tint_dc, 0, 0, self.w, self.h,
            blend
        )

        # 5. Cleanup all GDI objects
        ctypes.windll.gdi32.DeleteObject(int(brush))
        ctypes.windll.gdi32.DeleteObject(int(tint_bmp))
        ctypes.windll.gdi32.DeleteDC(int(tint_dc))
        ctypes.windll.gdi32.DeleteObject(int(screen_bmp))
        ctypes.windll.gdi32.DeleteDC(int(screen_dc))

    def __del__(self):
        # Cleanup the stored original content
        ctypes.windll.gdi32.DeleteObject(self.original_bmp)
        ctypes.windll.gdi32.DeleteDC(self.original_dc)
# =================== Effect Manager ===================
class EffectManager:
    def __init__(self, hdc, memdc, x, y, w, h):
        self.effects = [
            (TunnelInvertEffect(hdc, memdc, x, y, w, h), 9),
            (IconSpamEffect(hdc, memdc, x, y, w, h), 6),
            (IconTunnelInvertEffect(hdc, memdc, x, y, w, h), 7),
            (ColorEffect(hdc, memdc, x, y, w, h), 6),
            (ColorFilterEffect(hdc, memdc, x, y, w, h), 5)
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
        if isinstance(effect, ColorEffect) or isinstance(effect, ColorFilterEffect):
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