using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader.MemoryCard
{
    /// <summary>
    /// Enumeration to indicate which animation the Banner is following.
    /// </summary>
    [Flags] enum BannerIconAnimation { Forward = 0, PingPong = 1 }

    /// <summary>
    /// Enumeration to indicate whether a Banner is present or not.
    /// </summary>
    [Flags] enum BannerPresence { None = 0, Present = 1 }

    /// <summary>
    /// Enumeration to indicate the colorscheme used by a banner.
    /// </summary>
    [Flags] enum BannerColorScheme { RGB5A3 = 0, CI8 = 1 }

    /// <summary>
    /// This class represents the Banner GFX Format for a <see cref="DirectoryEntry"/>.
    /// Each <see cref="DirectoryEntry"/> contains information about the banner being displayed for a save game.
    /// All this information is stored on the card as a single byte at 0x07 in the entry.
    /// To better represent this information, we'll use a custom class to properly represent this.
    /// </summary>
    internal class BannerGfxFormat
    {
        public BannerIconAnimation Animation { get; internal set; }

        public BannerPresence Presence { get; internal set; }

        public BannerColorScheme ColorScheme { get; internal set; }

        public BannerGfxFormat(byte data) 
        {
            ColorScheme = (BannerColorScheme)((data >> 0) & 1);
            Presence = (BannerPresence)((data >> 1) & 1);
            Animation = (BannerIconAnimation)((data >> 2) & 1);
        }

        /// <summary>
        /// Returns a <see cref="string"/> representing the <c>BannerGfxFormat</c> instance.
        /// </summary>
        /// <returns>The <see cref="string"/> representing the <see cref="BannerGfxFormat"/> instance</returns>
        public override string ToString()
        {
            StringBuilder sb = new ();

            sb.AppendLine($"ByteMask: 0x{Convert.ToString((byte)this, 2).PadLeft(3, '0')}");
            sb.AppendLine($"\t\tColorScheme: {ColorScheme}");
            sb.AppendLine($"\t\tPresence: {Presence}");
            sb.AppendLine($"\t\tAnimation: {Animation}");

            return sb.ToString();
        }

        /// <summary>
        /// Implements the implicit <c>(byte)</c> cast operator, allowing a <see cref="BannerGfxFormat"/> to be cast as <see cref="byte"/>.
        /// </summary>
        /// <param name="b">The <see cref="BannerGfxFormat"/> to be cast as <see cref="byte"/>.</param>
        public static implicit operator byte(BannerGfxFormat b)
        {
            var bits = new BitArray(3, false);
            byte[] bytes = new byte[1];

            bits[0] = b.ColorScheme == BannerColorScheme.CI8;
            bits[1] = b.Presence == BannerPresence.Present;
            bits[2] = b.Animation == BannerIconAnimation.PingPong;

            bits.CopyTo(bytes, 0);

            return bytes[0];
        }

        /// <summary>
        /// Implements the explicit <c>(BannerGfxFormat)</c> cast operator, allowing a <see cref="byte"/> to be cast into a <see cref="BannerGfxFormat"/>.
        /// </summary>
        /// <param name="b">The <see cref="byte"/> to cast into a <see cref="BannerGfxFormat"/> instance.</param>
        public static explicit operator BannerGfxFormat(byte b) => new (b);
    }
}
