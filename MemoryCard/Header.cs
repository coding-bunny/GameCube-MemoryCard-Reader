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
        public ushort Size { get; set; }
        public ushort Encoding { get; set; }
        public ushort UpdateCounter { get; set; }
        public ushort CheckSumOne { get; set; }
        public ushort CheckSumTwo { get; set; }

        /// <summary>
        /// Returns a detailed <see cref="string"/> representation of the MemoryCard Header.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the MemoryCard Header</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"TimeSpan: {TimeSpan}");
            stringBuilder.AppendLine($"CardId: {CardId}");
            stringBuilder.AppendLine($"Size (Mbits): {Size}");
            stringBuilder.AppendLine($"Encoding: {Encoding}");
            stringBuilder.AppendLine($"UpdateCounter: {UpdateCounter}");
            stringBuilder.AppendLine($"CheckSumOne: {CheckSumOne}");
            stringBuilder.AppendLine($"CheckSumTwo: {CheckSumTwo}");

            return stringBuilder.ToString();
        }
    }
}
