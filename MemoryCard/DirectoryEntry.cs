using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_MemoryCard_Reader.MemoryCard
{
    /// <summary>
    /// This class represents a single entry in the MemoryCard directory, and contains all the information
    /// relevant to reading out the stored SaveGame data from the MemoryCard.
    /// The class has simple properties to represent the information stored in the Directory.
    /// </summary>
    internal class DirectoryEntry
    {
        public uint GameCode { get; internal set; }
        public ushort MakerCode { get; internal set; }
        public byte Banner {  get; internal set; }
        public string FileName { get; internal set; } = "";
        public uint LastModification { get; internal set; }
        public uint ImageDataOffset { get;internal set; }
        public ushort IconFormat { get; internal set; }
        public ushort AnimationSpeed { get; internal set; }
        public byte FilePermissions { get; internal set; }
        public byte CopyCounter { get; internal set; }
        public ushort BlockNumber { get; internal set; }
        public ushort BlockCount { get; internal set;}
        public uint CommentAddress { get; internal set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"\tGameCode: {GameCode}");
            sb.AppendLine($"\tMakerCode: {MakerCode}");
            sb.AppendLine($"\tBanner: {Convert.ToString(Banner).PadLeft(8, '0')}");
            sb.AppendLine($"\tFileName: {FileName}");
            sb.AppendLine($"\tLastModification: {LastModification}");
            sb.AppendLine($"\tImageDataOffset: {ImageDataOffset}");
            sb.AppendLine($"\tIconFormat: {IconFormat}");
            sb.AppendLine($"\tAnimationSpeed: {AnimationSpeed}");
            sb.AppendLine($"\tFilePermissions: {Convert.ToString(FilePermissions).PadLeft(8, '0')}");
            sb.AppendLine($"\tCopyCounter: {CopyCounter}");
            sb.AppendLine($"\tBlockNumber: {BlockNumber}");
            sb.AppendLine($"\tBlockCount: {BlockCount}");
            sb.AppendLine($"\tCommentAddress: {CommentAddress}");

            return sb.ToString();
        }
    }
}
