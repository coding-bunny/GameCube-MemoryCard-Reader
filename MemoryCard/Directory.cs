using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader.MemoryCard
{
    /// <summary>
    /// This class represents a single Directory on the Nintendo GameCube MemoryCard.
    /// The directory can be a backup, but this does not affect the structure of the Directory.
    /// 
    /// A Directory can have a maximum of 127 entries.
    /// </summary>
    internal class Directory
    {
        public ICollection<DirectoryEntry> Entries { get; internal set; } = new List<DirectoryEntry>(127);
        public ushort UpdateCounter { get; internal set; }
        public ushort ChecksumOne { get; internal set; }
        public ushort ChecksumTwo { get; internal set; }

        /// <summary>
        /// Returns a <see cref="string"/> representation of the <c>Directory</c>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the <c>Directory</c>.</returns>
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Directory Entries: {Entries.Count}");

            foreach (DirectoryEntry entry in Entries)
            {
                sb.Append(entry);
                sb.AppendLine();
            }

            sb.AppendLine($"UpdateCounter: {UpdateCounter}");
            sb.AppendLine($"CheckSum 1: {ChecksumOne}");
            sb.AppendLine($"CheckSum 2: {ChecksumTwo}");

            return sb.ToString();
        }
    }
}
