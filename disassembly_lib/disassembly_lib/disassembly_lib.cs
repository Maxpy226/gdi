using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;


namespace disassembly_library
{
    static public class disassembly_lib
    {
        public class Beat1: IWaveProvider
        {
            public WaveFormat WaveFormat { get; }
            private int t = 0;

            public Beat1()
            {
                WaveFormat = new WaveFormat(8000, 8, 1); // 8kHz, 8-bit, mono
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    int value = (int)(t * Math.Cos(t >> 5));
                    buffer[offset + i] = (byte)(value & 255);
                    t++;
                }
                return count;
            }
        }
        public class Beat2 : IWaveProvider
        {
            public WaveFormat WaveFormat { get; }
            private int t = 0;

            public Beat2()
            {
                WaveFormat = new WaveFormat(11000, 8, 1); // 8kHz, 8-bit, mono
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    int value = t * (t >> 10 & 255 | t * 14 % 8);
                    buffer[offset + i] = (byte)(value & 255);
                    t++;
                }
                return count;
            }
        }
        public class Beat3 : IWaveProvider
        {
            public WaveFormat WaveFormat { get; }
            private int t = 0;

            public Beat3()
            {
                WaveFormat = new WaveFormat(22050, 8, 1); // 8kHz, 8-bit, mono
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    int value = t >> 6 ^ t & t >> 9 ^ t >> 12 | (t << (t >> 6) % 4 ^ -t & -t >> 13) % 128 ^ -t >> 1;
                    buffer[offset + i] = (byte)(value & 255);
                    t++;
                }
                return count;
            }
        }
        public class Beat4 : IWaveProvider
        {
            public WaveFormat WaveFormat { get; }
            private int t = 0;

            public Beat4()
            {
                WaveFormat = new WaveFormat(11025, 8, 1); // 8kHz, 8-bit, mono
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    int x, y, z, a;

                    int value = (z = (x = ((a = t + 16777216) & a >> 7) + a / 32768) / (y = -x | x * a << 8) | y / x) * z;
                    buffer[offset + i] = (byte)(value & 255);
                    t++;
                }
                return count;
            }
        }
        public class Beat5 : IWaveProvider
        {
            public WaveFormat WaveFormat { get; }
            private int t = 0;

            public Beat5()
            {
                WaveFormat = new WaveFormat(8000, 8, 1); // 8kHz, 8-bit, mono
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                for (int i = 0; i < count; i++)
                {

                    int value = (int)(t * (
                     ((t & 4096) != 0
                         ? ((t % 65536) < 59392 ? 7 : (t & 7))
                         : 16)
                     + ((1 & (t >> 14)))
                 ) >> (3 & (-(int)t >> ((t & 2048) != 0 ? 2 : 10))));
                    buffer[offset + i] = (byte)(value & 255);
                    t++;
                }
                return count;
            }
        }
    }
}
