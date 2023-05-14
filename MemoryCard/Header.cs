using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader.MemoryCard
{
    internal class Header
    {
        public ulong TimeSpan { get; set; }
        public string CardId { get; set; } = "";
        public short Size { get; set; }
        public short Encoding { get; set; }
        public short UpdateCounter { get; set; }
        public short CheckSumOne { get; set; }
        public short CheckSumTwo { get; set; }

        private DateTime GameCubeEpoch = new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns a detailed <see cref="string"/> representation of the MemoryCard Header.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the MemoryCard Header</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"TimeSpan: {SystemTime():dd/MMM, yyyy HH:mm:ss}");
            stringBuilder.AppendLine($"CardId: {CardId}");
            stringBuilder.AppendLine($"Size (Mbits): {Size}");
            stringBuilder.AppendLine($"Encoding: {Encoding}");
            stringBuilder.AppendLine($"UpdateCounter: {UpdateCounter}");
            stringBuilder.AppendLine($"CheckSumOne: {CheckSumOne}");
            stringBuilder.AppendLine($"CheckSumTwo: {CheckSumTwo}");

            return stringBuilder.ToString();
        }

        private DateTime SystemTime()
        {
            // TODO: Determine the card type somehow.
            // A GameCube has a different CPU count than a Wii for example, resulting in different values.
            return GameCubeEpoch.AddSeconds((TimeSpan & 0xFFFF0000) / 12);
        }
    }
}
