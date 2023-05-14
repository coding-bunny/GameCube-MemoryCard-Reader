using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader.MemoryCard
{
    enum IconType { None = 0b00, CI8WithSharedPalette = 0b01, RGB5A3 = 0b10, CI8WithUniquePalette = 0b11 }

    /// <summary>
    /// This class contains all the information about the Icon being displayed for a SaveGame.
    /// Because the Nintendo GameCube has some internal rules about what's considered correct,
    /// it's important we can parse this information correctly.
    /// 
    /// Since we're reading out 2 bytes at the 0x30 offset, this implies that we can have multiple
    /// icons assigned. To properly parse these, we're going add a method that allows conversion from
    /// a <see cref="ushort"/> into an array of <c>IconGfxFormat</c> objects and back.
    /// </summary>
    internal class IconGfxFormat
    {
        /// <summary>
        /// The <see cref="IconType"/> of the Icon. Set to <c>IconType.None</c> when no Icon is present.
        /// </summary>
        public IconType Type { get; internal set; } = IconType.None;

        /// <summary>
        /// Constructs an <see cref="ICollection"/> of <c>IconGfxFormat</c> entries based upon the provided data.
        /// Each <c>IconGfxFormat</c> uses 2 bits to store the information, giving us a theoretical maximum of 8
        /// icons that can be stored inside the 2 bytes of the <see cref="DirectoryEntry"/>.
        /// 
        /// Note that this method will filter out all <c>IconGfxFormat</c> entries that have their type set to <c>IconType.None</c>
        /// </summary>
        /// <param name="data">The 2 bytes holding the information as an <see cref="ushort"/>.</param>
        /// <returns>The <see cref="ICollection"/> holding all <c>IconGfxFormat</c> icons.</returns>
        public static ICollection<IconGfxFormat> FromShort(short data)
        {
            ICollection<IconGfxFormat> icons = new List<IconGfxFormat>(8);

            for (int i = 0; i < 8; i++)
            {
                icons.Add(new IconGfxFormat() { Type = (IconType)(data & 0b00000011) });
                data = (short)(data >> 2);
            }

            return icons.Where(icon => icon.Type != IconType.None).ToList();
        }
    }
}
