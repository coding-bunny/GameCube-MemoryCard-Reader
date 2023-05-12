using GC_MemoryCard_Reader.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader
{
    /// <summary>
    /// This class represents a Nintendo GameCube MemoryCard.
    /// </summary>
    internal class VirtualCard
    {
        private readonly MemoryCard.BlockAllocationMap _allocationMap;
        private readonly ICollection<MemoryCard.SaveGame> saves;

        public MemoryCard.Header Header { get; internal set; }
        public MemoryCard.Directory Directory { get; internal set; }
        public MemoryCard.Directory DirectoryBackup { get; internal set; }


        public static VirtualCard FromPath(string path)
        {
            var parser = new RawParser(path);

            return new VirtualCard()
            {
                Header = parser.ExtractHeader(),
                Directory = parser.ExtractDirectory(),
                DirectoryBackup = parser.ExtractDirectory(true)
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append(Header.ToString());
            sb.Append(Directory.ToString());
            sb.AppendLine();
            sb.AppendLine("--Backup--");
            sb.Append(DirectoryBackup.ToString());
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
